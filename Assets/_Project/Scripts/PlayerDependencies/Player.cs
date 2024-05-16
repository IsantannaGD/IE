using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnInteracting;

    [SerializeField] private PlayerPhysics _playerPhysics;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private PlayerInventoryManager _playerInventoryManager;

    public PlayerPhysics PlayerPhysics => _playerPhysics;
    public PlayerInventoryManager PlayerInventoryManager => _playerInventoryManager;

    private void Start()
    {
        Initializations();
    }

    private void Initializations()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
