using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private CharacterController _rigB;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private Vector3 _direction;

    public void SetAxis(Vector2 vec2)
    {
        _axis = new Vector3(vec2.x, 0f, vec2.y).normalized;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if(GameManager.GamePaused)
        {return;}

        _direction = _axis * _moveSpeed;
        _direction.y = _gravity;
        _direction = transform.TransformDirection(_direction);
        _rigB.Move(_direction * Time.deltaTime);
    }
}
