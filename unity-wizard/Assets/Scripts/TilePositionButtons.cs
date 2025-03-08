using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilePositionButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _upButton, _downButton, _leftButton, _rightButton;

    private void Start()
    {
        _upButton.onClick.AddListener(() => _placementSystem.MoveTile(new Vector3Int(0, 0, 1)));
        _downButton.onClick.AddListener(() => _placementSystem.MoveTile(new Vector3Int(0, 0, -1)));
        _leftButton.onClick.AddListener(() => _placementSystem.MoveTile(new Vector3Int(-1, 0, 0)));
        _rightButton.onClick.AddListener(() => _placementSystem.MoveTile(new Vector3Int(1, 0, 0)));
    }
}
