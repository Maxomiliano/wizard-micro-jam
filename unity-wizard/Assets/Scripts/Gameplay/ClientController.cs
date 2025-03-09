using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    [SerializeField] Sprite[] m_clients;

    //Las referencias de las faces tienen que cargarse en este orden: Neutral, Happy, Angry. De este manera 0 es Neutral, 1 es Happy y 2 es Angry.
    [SerializeField] Sprite[] m_fairyFaces;
    [SerializeField] Sprite[] m_vampireFaces;
    [SerializeField] Sprite[] m_werewolfFaces;
    [SerializeField] Sprite[] m_goblinFaces;

    [SerializeField] private Sprite[] m_faces;

    [SerializeField] SpriteRenderer m_bodyRenderer;
    [SerializeField] SpriteRenderer m_faceRenderer;

    void SetNewClient()
    {
        int i = Random.Range(0, m_clients.Length);
        m_bodyRenderer.sprite = m_clients[i];
        switch (i)
        {
            case 0:
                m_faces = m_fairyFaces;
                break;
            case 1:
                m_faces = m_vampireFaces;
                break;
            case 2:
                m_faces = m_werewolfFaces;
                break;
            case 3:
                m_faces = m_goblinFaces;
                break;
        }
        m_faceRenderer.sprite = m_faces[0];
    }

    private void SetClientMood()
    {
        
    }
}
