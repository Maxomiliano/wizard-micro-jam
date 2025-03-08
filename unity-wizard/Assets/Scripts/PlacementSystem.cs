using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    //[SerializeField] private GameObject _mouseIndicator;
    [SerializeField] private GameObject _cellIndicator;
    //[SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _cellSize = 1f;

    private Vector3 _currentGridPosition;

    private void Start()
    {
        //_currentGridPosition = _grid.WorldToCell(Vector3.zero);
        _currentGridPosition =  Vector3.zero;
        UpdateIndicatorPosition();
    }

    private void Update()
    {
        /*
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        _mouseIndicator.transform.position = mousePosition;
        _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        */
        if (Input.GetKeyDown(KeyCode.W)) MoveTile(new Vector3(0, 0, 1));
        if (Input.GetKeyDown(KeyCode.S)) MoveTile(new Vector3(0, 0, -1));
        if (Input.GetKeyDown(KeyCode.A)) MoveTile(new Vector3(-1, 0, 0));
        if (Input.GetKeyDown(KeyCode.D)) MoveTile(new Vector3(1, 0, 0));
    }

    public void MoveTile(Vector3 direction)
    {
        _currentGridPosition += direction * _cellSize;
        UpdateIndicatorPosition();
    }

    private void UpdateIndicatorPosition()
    {
       _cellIndicator.transform.localPosition = _currentGridPosition;
    }
}
