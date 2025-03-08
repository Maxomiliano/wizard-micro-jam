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

    private Vector3 _currentGridPosition;
    private float _currentRotation;
    private int _gridSize = 5;

    private void Start()
    {
        _currentGridPosition =  Vector3.zero;
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
    }


    public void MoveTile(Vector3 direction)
    {
        //_currentGridPosition += direction * _cellSize;

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
    }

    public void PlaceObject(GameObject objectPrefab)
    {
        if(objectPrefab != null)
        {
            Vector3 spawnPosition = _cellIndicator.transform.position;
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.Euler(0, _currentRotation, 0), _spawnParent);
        }
    }


    private void UpdateIndicatorPosition()
    {
       _cellIndicator.transform.localPosition = _currentGridPosition;
    }
}
