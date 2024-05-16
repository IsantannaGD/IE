using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action OnStartGame;
    public static Action OnGamePause;
    public static Action OnCallSettings;
    public static Action<string, bool> OnShowMessage;

    public static GameManager Instance;

    public static bool GamePaused = false;

    public static void LoadSceneCallback(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void QuitGameCallback()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Instance = this;

        OnGamePause += GamePauseCallback;

        
    }

    private void Start()
    {
        LoadSceneCallback(1);
    }

    private void GamePauseCallback()
    {
        GamePaused = !GamePaused;

        if (GamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
