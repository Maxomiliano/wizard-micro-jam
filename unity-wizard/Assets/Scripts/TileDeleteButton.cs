using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDeleteButton : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _deleteButton;

    private void Start()
    {
        _deleteButton.onClick.AddListener(() => _placementSystem.RemoveObject());
    }
}
