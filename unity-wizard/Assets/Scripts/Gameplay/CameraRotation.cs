using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject m_camera;

    [SerializeField] float speed = 3.5f;
    private float X;
    private float Y;

    private Quaternion m_origin;
    private bool m_isDragging;

    [SerializeField] float m_minAngleY = -5;
    [SerializeField] float m_maxAngleY = 5;
    [SerializeField] float m_minAngleX = -20;
    [SerializeField] float m_maxAngleX = 0;

    void Start()
    {
        m_origin = transform.rotation;
    }

    void Update()
    {
        if (m_isDragging)
            return;        
        if (m_camera.transform.rotation != m_origin)
        {
            m_camera.transform.rotation = Quaternion.RotateTowards(m_camera.transform.rotation, m_origin, speed * Time.deltaTime * 10);
        }
    }

    private void OnMouseDown()
    {
        m_isDragging = true;
    }

    private void OnMouseDrag()
    {
        X += speed * Input.GetAxis("Mouse Y");
        Y -= speed * Input.GetAxis("Mouse X");
        X = Mathf.Clamp(X, m_minAngleX, m_maxAngleX);
        Y = Mathf.Clamp(Y, m_minAngleY, m_maxAngleY);
        m_camera.transform.eulerAngles = new Vector3(X, Y, 0);        
    }

    private void OnMouseUp()
    {
        m_isDragging = false;
        X = 0;
        Y = 0;
    }
}
