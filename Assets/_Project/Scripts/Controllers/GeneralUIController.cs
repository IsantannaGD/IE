using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUIController : MonoBehaviour
{
    [SerializeField] private GameObject _messageBackground;
    [SerializeField] private TextMeshProUGUI _messageDisplay;
    [SerializeField] private Image _pointer;

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        GameManager.OnShowMessage += PlayerMessageControllerCallback;
    }

    private void PlayerMessageControllerCallback(string message, bool status)
    {
        _pointer.color = status ? Color.green : Color.yellow;

        _messageBackground.SetActive(status);
        _messageDisplay.text = message;
    }

    private void OnDestroy()
    {
        GameManager.OnShowMessage -= PlayerMessageControllerCallback;
    }
}
