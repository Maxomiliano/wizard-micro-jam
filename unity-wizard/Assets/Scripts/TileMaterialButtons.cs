using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMaterialButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _woodMaterialButton, _boneMaterialButton, _silverMaterialButton;

    private void Start()
    {
        _woodMaterialButton.onClick.AddListener(() => SelectWoodMaterial());
        _boneMaterialButton.onClick.AddListener(() => SelectBoneMaterial());
        _silverMaterialButton.onClick.AddListener(() => SelectSilverMaterial());
    }


    public void SelectWoodMaterial()
    {
        _placementSystem.ChangeObjectMaterial(1);
    }

    public void SelectBoneMaterial()
    {
        _placementSystem.ChangeObjectMaterial(2);
    }

    public void SelectSilverMaterial()
    {
        _placementSystem.ChangeObjectMaterial(3);
    }
}
