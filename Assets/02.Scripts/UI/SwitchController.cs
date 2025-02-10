using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Image handleImage;

    private RectTransform _handleRectTransform;
    private bool _isOn;
    
    private void Awake()
    {
        _handleRectTransform = handleImage.GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetOn(false);
    }

    private void SetOn(bool isOn)
    {
        if (isOn)
        {
            _handleRectTransform.DOAnchorPosX(15, 0.2f);
        }
        else
        {
            _handleRectTransform.DOAnchorPosX(-15, 0.2f);
        }

        _isOn = isOn;
    }

    public void OnClickSwitch()
    {
        SetOn(!_isOn);
    }
}
