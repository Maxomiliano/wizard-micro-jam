using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileConfirmMaterialButton : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _confirmButton;

    private void Start()
    {
        _confirmButton.onClick.AddListener(() => _placementSystem.ConfirmMaterialSelection());
    }
}
