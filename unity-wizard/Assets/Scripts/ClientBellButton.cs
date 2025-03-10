using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientBellButton : MonoBehaviour
{
    [SerializeField] ClientController _clientController;

    private void OnMouseDown()
    {
        _clientController.SubmitClient();
    }
}
