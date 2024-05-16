using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineVirtualCamera _eyesCam;
    [SerializeField] private Color _rayColor = Color.yellow;

    [SerializeField] private float _interactionRange = 2f;
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private IInteractable _interactableObject;

    public IInteractable InteractableObject => _interactableObject;

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.PlayerPhysics.SetAxis(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            _player.PlayerPhysics.SetAxis(Vector2.zero);
        }
    }

    public void Interactive(InputAction.CallbackContext context)
    {
        if(GameManager.GamePaused)
        {return;}

        if (context.performed)
        {
            if (_interactableObject != null)
            {
                _interactableObject.InteractionCallback(_player);
            }
        }
    }

    public void OpenSettings(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.OnCallSettings?.Invoke();
        }
    }

    private void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if(GameManager.GamePaused)
        {return;}

        var eyesTransform = _eyesCam.transform;
        Ray ray = new Ray( eyesTransform.position, eyesTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _interactionRange, _rayColor);

        RaycastHit impact;

        if (Physics.Raycast(ray, out impact, _interactionRange, _interactionLayer))
        {
            if (impact.transform.parent.TryGetComponent(out IInteractable obj))
            {
                _rayColor = Color.green;
                _interactableObject = obj;
                _interactableObject.HighLightInteractableObject(true);
                return;
            }
        }

        if (_interactableObject != null)
        {
            _interactableObject.HighLightInteractableObject(false);
            _interactableObject = null;
        }

        _rayColor = Color.yellow;
    }
}
