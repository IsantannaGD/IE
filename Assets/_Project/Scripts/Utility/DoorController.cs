using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private string _messageToShow;

    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;

    [SerializeField] private Vector3 _originalLeftDoorRot;
    [SerializeField] private Vector3 _originalRightDoorRot;

    [SerializeField] private Vector3 _targetLeftDoorRot;
    [SerializeField] private Vector3 _targetRightDoorRot;

    [SerializeField] private float _animSpeed;
    [SerializeField] private bool _animationRunning;
    [SerializeField] private bool _isOpened;

    public void HighLightInteractableObject(bool status)
    {
        GameManager.OnShowMessage?.Invoke(_messageToShow, status);

        // if (status)
        // {
        //     foreach (PersonalOutline outline in _itemOutlines)
        //     {
        //         outline.OutlineWidth = 10f;
        //     }
        //
        //     return;
        // }
        //
        // foreach (PersonalOutline outline in _itemOutlines)
        // {
        //     outline.OutlineWidth = 0f;
        // }
    }

    public void InteractionCallback(Player player)
    {
        if(_animationRunning)
        {return;}

        _animationRunning = true;

        Sequence anim = DOTween.Sequence();

        anim.AppendCallback(OpenDoorCallback);

        anim.Play().OnComplete(() =>
        {
            _isOpened = !_isOpened;
            _animationRunning = false;
        });
    }

    private void OpenDoorCallback()
    {
        if (!_isOpened)
        {
            _leftDoor.transform.DOLocalRotate(_targetLeftDoorRot, _animSpeed);
            _rightDoor.transform.DOLocalRotate(_targetRightDoorRot, _animSpeed);
            return;
        }

        _leftDoor.transform.DOLocalRotate(_originalLeftDoorRot, _animSpeed);
        _rightDoor.transform.DOLocalRotate(_originalRightDoorRot, _animSpeed);
    }
}
