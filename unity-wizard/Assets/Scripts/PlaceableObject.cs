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

    public void SetPreviewMaterial(int materialIndex)
    {
        if (materialIndex >= 0 && materialIndex < _materialVariants.Length)
        {
            foreach(var variant in _materialVariants)
            {
                variant.gameObject.SetActive(false);
            }
            _materialVariants[materialIndex].gameObject.SetActive(true);
        }
    }

    public int GetCurrentMaterialIndex()
    {
        return _currentMaterialIndex;
    }
}
