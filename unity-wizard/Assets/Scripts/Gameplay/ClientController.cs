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

    private Dictionary<int, int> _materialAffinity = new Dictionary<int, int>();

    void SetNewClient()
    {
        int i = Random.Range(0, m_clients.Length);
        m_bodyRenderer.sprite = m_clients[i];
        switch (i)
        {
            case 0:
                m_faces = m_fairyFaces;
                _materialAffinity = new Dictionary<int, int>
                {
                    { 0, 1}, //Madera favorito +1
                    { 1, 0}, //Hueso neutro 0
                    { 2, -1} //Plata enojado -1
                };
                break;
            case 1:
                m_faces = m_vampireFaces;
                _materialAffinity = new Dictionary<int, int>
                {
                    { 0, 1},
                    { 1, 0},
                    { 2, -1}
                };
                break;
            case 2:
                m_faces = m_werewolfFaces;
                _materialAffinity = new Dictionary<int, int>
                {
                    { 0, -1},
                    { 1, 1},
                    { 2, 0}
                };
                break;
            case 3:
                m_faces = m_goblinFaces;
                _materialAffinity = new Dictionary<int, int>
                {
                    { 0, 0},
                    { 1, -1},
                    { 2, 1}
                };
                break;
        }
        m_faceRenderer.sprite = m_faces[0];
    }

    public void SetClientMood()
    {
        int totalScore = 0;
        int totalObjects = 0;

        foreach (var material in _materialAffinity)
        {
            if(_materialAffinity.ContainsKey(material.Key))
            {
                totalScore += _materialAffinity[material.Key] * material.Value;
                totalObjects += material.Value;
            }
        }

        if (totalObjects == 0)
        {
            m_faceRenderer.sprite = m_faces[0];
            return;
        }
        float moodScore = (float)totalScore / totalObjects;

        if (moodScore > 2)
        {
            m_faceRenderer.sprite = m_faces[1];
        }
        else if (moodScore < 0)
        {
            m_faceRenderer.sprite = m_faces[2];
        }
        else
        {
            m_faceRenderer.sprite = m_faces[0];
        }
    }
}
