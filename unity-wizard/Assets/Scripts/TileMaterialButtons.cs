using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterialButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;

    public void SelectFirstMaterial()
    {
        _placementSystem.ChangeObjectMaterial(1);
    }
}
