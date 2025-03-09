using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _materialVariants;
    private int _currentMaterialIndex = 0;

    private void Start()
    {
        SetMaterial(_currentMaterialIndex);
    }

    public void SetMaterial(int index)
    {
        if(index < 0 || index >= _materialVariants.Length) return;

        foreach(GameObject material in _materialVariants)
        {
            material.SetActive(false);
        }
        _materialVariants[index].SetActive(true);
        _currentMaterialIndex = index;
    }

    public int GetCurrentMaterialIndex()
    {
        return _currentMaterialIndex;
    }
}
