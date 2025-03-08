using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsMenu : MonoBehaviour
{
    [SerializeField] GameObject m_spellCanvas;
    [SerializeField] GameObject m_wandParticles;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_spellCanvas.SetActive(true);
            m_wandParticles.SetActive(true);
        }
    }
}
