using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    public delegate void OnConfirmButtonClick();
    private OnConfirmButtonClick onConfirmButtonClick;

    public void Show(string message, OnConfirmButtonClick onConfirmButtonClick)
    {
        messageText.text = message;
        this.onConfirmButtonClick = onConfirmButtonClick;
    }

    /// <summary>
    /// Confirm 버튼 클릭시 호출되는 함수
    /// </summary>
    public void OnClickConfirmButton()
    {
        onConfirmButtonClick?.Invoke();
        Hide();
    }

    /// <summary>
    /// X버튼 클릭시 호출되는 함수
    /// </summary>
    public void OnClickCloseButton()
    {
        Hide();
    }
}
