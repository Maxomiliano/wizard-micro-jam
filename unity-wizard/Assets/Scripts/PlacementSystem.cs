using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _placeableObjects;
    [SerializeField] private GameObject _cellIndicator;
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _cellSize = 1f;

    [SerializeField] private Color _freeColor = Color.green;
    [SerializeField] private Color _occupiedColor = Color.red;
    private Renderer _cellRenderer;

    private Dictionary<Vector3Int, GameObject> _placedObjects = new Dictionary<Vector3Int, GameObject>();

    private Vector3 _currentGridPosition;
    private float _currentRotation;
    private int _gridSize = 5;

    private void Start()
    {
        _cellRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
        _currentGridPosition = Vector3.zero;
        UpdateIndicatorPosition();
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
            }
        }
    }

    public void RemoveObject()
    {
        Vector3Int gridPosition = _grid.WorldToCell(_currentGridPosition);
        if (_placedObjects.ContainsKey(gridPosition))
        {
            Destroy(_placedObjects[gridPosition]);
            _placedObjects.Remove(gridPosition);
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
}
