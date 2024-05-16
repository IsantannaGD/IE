using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitGameButton;

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        _playButton.onClick.AddListener(() => GameManager.LoadSceneCallback(2));
        _exitGameButton.onClick.AddListener(GameManager.QuitGameCallback);
    }
}
