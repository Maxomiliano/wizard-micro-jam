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

    //[SerializeField] private Sprite[] m_faces;


    [SerializeField] SpriteRenderer m_bodyRenderer;
    [SerializeField] SpriteRenderer m_faceRenderer;

    private Dictionary<int, int> _materialAffinity = new Dictionary<int, int>();


    private void Start()
    {
        SetNewClient();
    }

    void SetNewClient()
    {
        Debug.Log("SetNewClient() ha sido llamado!");
        int i = Random.Range(0, m_clients.Length);
        m_bodyRenderer.sprite = m_clients[i];
        switch (i)
        {
            case 0:
                _materialAffinity = new Dictionary<int, int>
                {
                    { (int)MaterialType.Wood, 0 },
                    { (int)MaterialType.Bone, 1 },
                    { (int)MaterialType.Silver, -1 }
                };
                m_faceRenderer.sprite = m_fairyFaces[0];
                break;
            case 1:
                _materialAffinity = new Dictionary<int, int>
                {
                    { (int)MaterialType.Wood, 1 },
                    { (int)MaterialType.Bone, 0 },
                    { (int)MaterialType.Silver, -1 }
                };
                m_faceRenderer.sprite = m_vampireFaces[0];
                break;
            case 2:
                _materialAffinity = new Dictionary<int, int>
                {
                    { (int)MaterialType.Wood, -1 },
                    { (int)MaterialType.Bone, 1 },
                    { (int)MaterialType.Silver, 0 }
                };
                m_faceRenderer.sprite = m_werewolfFaces[0];
                break;
            case 3:
                _materialAffinity = new Dictionary<int, int>
                {
                    { (int)MaterialType.Wood, 0 },
                    { (int)MaterialType.Bone, -1 },
                    { (int)MaterialType.Silver, 1 }
                };
                m_faceRenderer.sprite = m_goblinFaces[0];
                break;
        }
    }

    public void SetClientMood(Dictionary<int, int> materialCounters)
    {
        int totalScore = 0;
        int totalObjects = 0;

        foreach (var material in materialCounters)
        {
            if (_materialAffinity.ContainsKey(material.Key))
            {
                totalScore += _materialAffinity[material.Key] * materialCounters[material.Key];
                totalObjects += materialCounters[material.Key];
            }
        }

        if (totalObjects == 0)
        {
            if (m_bodyRenderer.sprite == m_clients[0]) // Fairy
                m_faceRenderer.sprite = m_fairyFaces[0];
            else if (m_bodyRenderer.sprite == m_clients[1]) // Vampiro
                m_faceRenderer.sprite = m_vampireFaces[0];
            else if (m_bodyRenderer.sprite == m_clients[2]) // Hombre lobo
                m_faceRenderer.sprite = m_werewolfFaces[0];
            else if (m_bodyRenderer.sprite == m_clients[3]) // Goblin
                m_faceRenderer.sprite = m_goblinFaces[0];

            return;
        }

        float moodScore = (float)totalScore / totalObjects;
        Sprite newFace = null;

        if (m_bodyRenderer.sprite == m_clients[0]) // Fairy
            newFace = moodScore > 0 ? m_fairyFaces[1] : (moodScore < 0 ? m_fairyFaces[2] : m_fairyFaces[0]);
        else if (m_bodyRenderer.sprite == m_clients[1]) // Vampiro
            newFace = moodScore > 0 ? m_vampireFaces[1] : (moodScore < 0 ? m_vampireFaces[2] : m_vampireFaces[0]);
        else if (m_bodyRenderer.sprite == m_clients[2]) // Hombre lobo
            newFace = moodScore > 0 ? m_werewolfFaces[1] : (moodScore < 0 ? m_werewolfFaces[2] : m_werewolfFaces[0]);
        else if (m_bodyRenderer.sprite == m_clients[3]) // Goblin
            newFace = moodScore > 0 ? m_goblinFaces[1] : (moodScore < 0 ? m_goblinFaces[2] : m_goblinFaces[0]);

        if (newFace != null)
        {
            m_faceRenderer.sprite = newFace;
        }
    }

    public void UpdateMaterialCounters(Dictionary<int, int> materialCounters)
    {
        SetClientMood(materialCounters);
    }
}
