using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    None = 0,
    Wood = 1,
    Bone = 2,
    Silver = 3
}

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _placeableObjects;
    [SerializeField] private GameObject _cellIndicator;
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _cellSize = 1f;

    [SerializeField] private Color _freeColor = Color.green;
    [SerializeField] private Color _occupiedColor = Color.red;
    [SerializeField] private ClientController _clientController;

    private Dictionary<Vector3Int, GameObject> _placedObjects = new Dictionary<Vector3Int, GameObject>();
    private Dictionary<int, int> _materialCounters = new Dictionary<int, int>();

    private PlaceableObject _lastPlaceableObject;
    private Renderer _cellRenderer;
    private Vector3 _currentGridPosition;
    private float _currentRotation;
    private int _gridSize = 5;
    private int _selectedMaterial = -1;


    private void Start()
    {
        _cellRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
        _currentGridPosition = Vector3.zero;
        UpdateIndicatorPosition();

        for (int i = 0; i < 3; i++)
        {
            _materialCounters[i] = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MoveTile(new Vector3(0, 0, 1));
        if (Input.GetKeyDown(KeyCode.S)) MoveTile(new Vector3(0, 0, -1));
        if (Input.GetKeyDown(KeyCode.A)) MoveTile(new Vector3(-1, 0, 0));
        if (Input.GetKeyDown(KeyCode.D)) MoveTile(new Vector3(1, 0, 0));

        if (Input.GetKeyDown(KeyCode.E)) RotateTile(45f);
        if (Input.GetKeyDown(KeyCode.Q)) RotateTile(-45f);

        if (Input.GetKeyDown(KeyCode.R)) RemoveObject();
    }


    public void MoveTile(Vector3 direction)
    {
        Vector3 newPosition = _currentGridPosition + (direction * _cellSize);

        float minLimit = -(_gridSize / 2) * _cellSize;
        float maxLimit = (_gridSize / 2) * _cellSize;

        newPosition.x = Mathf.Clamp(newPosition.x, minLimit, maxLimit);
        newPosition.z = Mathf.Clamp(newPosition.z, minLimit, maxLimit);

        _currentGridPosition = newPosition;
        UpdateIndicatorPosition();
    }

    public void RotateTile(float angle)
    {
        _currentRotation += angle;
        _cellIndicator.transform.localRotation = Quaternion.Euler(0, _currentRotation, 0);

        Vector3Int gridPosition = _grid.WorldToCell(_currentGridPosition);

        if (_placedObjects.ContainsKey(gridPosition))
        {
            _placedObjects[gridPosition].transform.localRotation = Quaternion.Euler(0, _currentRotation, 0);
        }
    }

    public void PlaceObject(GameObject objectPrefab)
    {
        Vector3Int gridPosition = _grid.WorldToCell(_currentGridPosition);
        if (_placedObjects.ContainsKey(gridPosition))
        {
            Debug.Log("No puedes colocar un objeto aquí. Celda ocupada.");
            return;
        }
        if (!_placedObjects.ContainsKey(gridPosition))
        {
            if (objectPrefab != null)
            {
                Vector3 spawnPosition = _cellIndicator.transform.position;
                GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.Euler(0, _currentRotation, 0), _spawnParent);
                _placedObjects.Add(gridPosition, newObject);
                _lastPlaceableObject = newObject.GetComponent<PlaceableObject>();

                int materialIndex = _lastPlaceableObject.GetCurrentMaterialIndex();
                AddMaterial((MaterialType)materialIndex);
                if (_clientController != null)
                {
                    _clientController.UpdateMaterialCounters(_materialCounters);
                }
            }
        }
    }

    public void RemoveObject()
    {
        Vector3Int gridPosition = _grid.WorldToCell(_currentGridPosition);
        if (_placedObjects.ContainsKey(gridPosition))
        {
            PlaceableObject objectToRemove = _placedObjects[gridPosition].GetComponent<PlaceableObject>();
            int materialIndex = objectToRemove.GetCurrentMaterialIndex();
            if (_materialCounters.ContainsKey(materialIndex))
            { 
                _materialCounters[materialIndex]--;
            }

            Destroy(_placedObjects[gridPosition]);
            _placedObjects.Remove(gridPosition);
            Debug.Log($"Objeto eliminado. Material {materialIndex} actualizado.");
            foreach (var pair in _materialCounters)
            {
                Debug.Log($"Material {pair.Key}: {pair.Value} objetos");
            }

            if (_clientController != null)
            {
                _clientController.UpdateMaterialCounters(_materialCounters);
            }
        }
    }

    private void UpdateIndicatorPosition()
    {
        _cellIndicator.transform.localPosition = _currentGridPosition;

        Vector3Int gridPosition = _grid.WorldToCell(_currentGridPosition);
        if (_placedObjects.ContainsKey(gridPosition))
        {
            _cellRenderer.material.color = _occupiedColor;
        }
        else
        {
            _cellRenderer.material.color = _freeColor;
        }
    }

    public void ChangeObjectMaterial(int materialIndex)
    {
        if (_lastPlaceableObject != null)
        {
            _selectedMaterial = materialIndex ;
            _lastPlaceableObject.SetPreviewMaterial(_selectedMaterial);
            Debug.Log($"Material cambiado visualmente a {_selectedMaterial}, pero aún no confirmado.");
        }
    }

    public void ConfirmMaterialSelection()
    {
        if (_lastPlaceableObject != null && _selectedMaterial != -1)
        {
            int previousMaterial = _lastPlaceableObject.GetCurrentMaterialIndex();

            if (previousMaterial != _selectedMaterial)
            {
                if (_materialCounters.ContainsKey(previousMaterial))
                {
                    _materialCounters[previousMaterial] = Mathf.Max(0, _materialCounters[previousMaterial] - 1);
                }

                if (_materialCounters.ContainsKey(_selectedMaterial))
                {
                    _materialCounters[_selectedMaterial]++;
                }
                else
                {
                    _materialCounters[_selectedMaterial] = 1;
                }
            }

            if (_clientController != null)
            {
                _clientController.UpdateMaterialCounters(_materialCounters);
            }


            _lastPlaceableObject.SetMaterial(_selectedMaterial);
            Debug.Log($"Material confirmado: {_selectedMaterial}");
            foreach (var pair in _materialCounters)
            {
                Debug.Log($"Material {pair.Key}: {pair.Value} objetos");
            }
            _selectedMaterial = -1;
        }
    }

    private void AddMaterial(MaterialType material)
    {
        int materialID = (int)material;
        if (!_materialCounters.ContainsKey(materialID))
        {
            _materialCounters[materialID] = 0;
        }
        _materialCounters[materialID]++;
        Debug.Log($"Material {materialID}: {_materialCounters[materialID]} objetos");
    }
}
