using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMaterialButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button _woodMaterialButton, _boneMaterialButton, _silverMaterialButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _exitButton.gameObject.SetActive(false);
        _woodMaterialButton.onClick.AddListener(() => SelectWoodMaterial());
        _boneMaterialButton.onClick.AddListener(() => SelectBoneMaterial());
        _silverMaterialButton.onClick.AddListener(() => SelectSilverMaterial());
    }


    public void SelectWoodMaterial()
    {
        _placementSystem.ChangeObjectMaterial(1);
        if (!_exitButton.isActiveAndEnabled)
        { 
            _exitButton.gameObject.SetActive(true);
        }
    }

    public void SelectBoneMaterial()
    {
        _placementSystem.ChangeObjectMaterial(2);
        if (!_exitButton.isActiveAndEnabled)
        {
            _exitButton.gameObject.SetActive(true);
        }
    }

    public void SelectSilverMaterial()
    {
        _placementSystem.ChangeObjectMaterial(3);
        if (!_exitButton.isActiveAndEnabled)
        {
            _exitButton.gameObject.SetActive(true);
        }
    }
}
