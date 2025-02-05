using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
  [SerializeField] private PanelController confirmPanelController;
  [SerializeField] private PanelController settingsPanelController;
  
  
  public enum PanelType {ConfirmPanel, SettingsPanel}
  private PanelController _currentPanelController;

  /// <summary>
  /// 표시할 패널 정보 전달하는 함수
  /// </summary>
  /// <param name="panelType">표시할 패널</param>
  public void ShowPanel(PanelType panelType)
  {
    switch (panelType)
    {
      case PanelType.ConfirmPanel:
        ShowPanelController(confirmPanelController);
        break;
      case PanelType.SettingsPanel:
        ShowPanelController(settingsPanelController);
        break;
    }
  }

  /// <summary>
  /// 패널을 표시하는 함수
  /// 기존 패널이 있으면 Hide하고 새로운 패널을 Show함
  /// </summary>
  /// <param name="panelController">표시할 패널</param>
  private void ShowPanelController(PanelController panelController)
  {
    if (_currentPanelController != null)
    {
      _currentPanelController.Hide();
    }
    panelController.Show(() =>
    {
      _currentPanelController = null;
    });
    _currentPanelController = panelController;
  }
}
