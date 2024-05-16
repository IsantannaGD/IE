using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelMovementController : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private bool _animationRunning = false;
    [SerializeField] private bool _isClosed;

    public void MovementManagerCallback(Action callback = null)
    {
        if(_animationRunning)
        {return;}

        _animationRunning = true;
        _isClosed = !_isClosed;

        if (!_isClosed)
        {
            callback?.Invoke();
            _rect.DOAnchorPosX(0f, _animationSpeed).SetUpdate(true).OnComplete(() =>
            {
                _animationRunning = false;
            });
            return;
        }

        _rect.DOAnchorPosX(500f, _animationSpeed).SetUpdate(true).OnComplete(() =>
        {
            callback?.Invoke();
            _animationRunning = false;
        });
    }
}
