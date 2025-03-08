using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileRotationButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _leftButton, _rightButton;

    private void Start()
    {
        _leftButton.onClick.AddListener(() => _placementSystem.RotateTile(-45f));
        _rightButton.onClick.AddListener(() => _placementSystem.RotateTile(45f));
    }
}
