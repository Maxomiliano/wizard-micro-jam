using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawnButtons : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private Button[] _objectsButtons;
    [SerializeField] private GameObject[] _objectsPrefabs;

    void Start()
    {
        for (int i = 0; i < _objectsButtons.Length; i++)
        {
            int index = i;
            _objectsButtons[i].onClick.AddListener(() => _placementSystem.PlaceObject(_objectsPrefabs[index]));
        }
    }
}
