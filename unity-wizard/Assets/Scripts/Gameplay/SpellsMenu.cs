using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsMenu : MonoBehaviour
{
    [SerializeField] GameObject m_spellCanvas;
    [SerializeField] GameObject m_wandParticles;
    [SerializeField] BoxCollider m_cameraRotation;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_spellCanvas.SetActive(true);
            m_wandParticles.SetActive(true);
            m_cameraRotation.enabled = false;
        }
    }
}
