using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Image handleImage;
    [SerializeField] private AudioClip clickSound;
    
    private Image _backgroundImage;
    private AudioSource _audioSource;
    private RectTransform _handleRectTransform;
    private bool _isOn;

    public delegate void OnSwitchChangedDelegate(bool isOn);
    public OnSwitchChangedDelegate OnSwitchChanged;

    private static readonly Color32 OnColor = new Color32(150, 150, 150, 255);
    private static readonly Color32 OffColor = new Color32(90, 90, 90, 255);
    
    private void Awake()
    {
        _handleRectTransform = handleImage.GetComponent<RectTransform>();
        _backgroundImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _handleRectTransform.anchoredPosition = new Vector2(-15, 0);
        _backgroundImage.color = OffColor;
        _isOn = false;
    }

    private void SetOn(bool isOn)
    {
        if (isOn)
        {
            _handleRectTransform.DOAnchorPosX(15, 0.2f);
            _backgroundImage.DOBlendableColor(OnColor, 0.2f);
        }
        else
        {
            _handleRectTransform.DOAnchorPosX(-15, 0.2f);
            _backgroundImage.DOBlendableColor(OffColor, 0.2f);
        }
        //효과음 재생
        if(clickSound != null) _audioSource.PlayOneShot(clickSound);
        
        
        OnSwitchChanged?.Invoke(isOn);
        _isOn = isOn;
    }

    public void OnClickSwitch()
    {
        SetOn(!_isOn);
    }
}
