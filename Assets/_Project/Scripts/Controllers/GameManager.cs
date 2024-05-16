using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnStartGame;
    public static Action OnGamePause;
    public static Action<string, bool> OnShowMessage;

    public static GameManager Instance;

    public static bool GamePaused = false;

    private void Awake()
    {
        Instance = this;

        OnGamePause += GamePauseCallback;
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
