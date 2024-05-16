using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookViewX : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        RotateViewX();
    }
    private void RotateViewX()
    {
        if(GameManager.GamePaused)
        {return;}

        float mouseX = Input.GetAxis("Mouse X");
        Vector3 localEulerAngles = transform.eulerAngles;
        localEulerAngles.y += mouseX * _rotateSpeed;

        transform.eulerAngles = localEulerAngles;
    }
}
