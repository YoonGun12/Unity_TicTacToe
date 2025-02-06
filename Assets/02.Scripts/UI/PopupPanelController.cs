using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanelController : MonoBehaviour
{
   /*public delegate void PopupDelegate();
   private PopupDelegate afterPopup;

   private GameObject popupPanel;
   private TextMeshProUGUI popupText;
   private Button confirmBtn;
   private Button closeBtn;
   private TextMeshProUGUI confirmBtnText;

   public PopupPanelController(PopupDelegate popupDelegate, GameObject popup, TextMeshProUGUI text, TextMeshProUGUI confirmText, Button confirm, Button close)
   {
      afterPopup = popupDelegate;
      popupPanel = popup;
      popupText = text;
      confirmBtnText = confirmText;
      confirmBtn = confirm;
      closeBtn = close;
   }

   public void ShowPopup(string message, string buttonMessage)
   {
      popupPanel.SetActive(true);
      popupText.text = message;
      confirmBtnText.text = buttonMessage;
      
      confirmBtn.onClick.RemoveAllListeners();
      confirmBtn.onClick.AddListener(() => {afterPopup.Invoke(); popupPanel.SetActive(false);});
      
      closeBtn.onClick.RemoveAllListeners();
      closeBtn.onClick.AddListener(()=>{popupPanel.SetActive(false);});
   }*/
}
