using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : PanelController
{
    [SerializeField] private CanvasGroup canvasGroupA;
    [SerializeField] private CanvasGroup canvasGroupB;
    [SerializeField] private Button gameOverButton;

    public enum GameUIMode
    {
        Init,
        TurnA,
        TurnB,
        GameOver
    }

    private const float DisableAlpha = 0.5f;
    private const float EnableAlpha = 1f;
    
    public void SetGameUIMode(GameUIMode mode)
    {
        switch (mode)
        {
            case GameUIMode.Init:
                canvasGroupA.gameObject.SetActive(true);
                canvasGroupB.gameObject.SetActive(true);
                gameOverButton.gameObject.SetActive(false);

                canvasGroupA.alpha = DisableAlpha;
                canvasGroupB.alpha = DisableAlpha;
                break;
            case GameUIMode.TurnA:
                canvasGroupA.gameObject.SetActive(true);
                canvasGroupB.gameObject.SetActive(true);
                gameOverButton.gameObject.SetActive(false);

                canvasGroupA.alpha = EnableAlpha;
                canvasGroupB.alpha = DisableAlpha;
                break;
            case GameUIMode.TurnB:
                canvasGroupA.gameObject.SetActive(true);
                canvasGroupB.gameObject.SetActive(true);
                gameOverButton.gameObject.SetActive(false);

                canvasGroupA.alpha = DisableAlpha;
                canvasGroupB.alpha = EnableAlpha;
                break;
            case GameUIMode.GameOver:
                canvasGroupA.gameObject.SetActive(false);
                canvasGroupB.gameObject.SetActive(false);
                gameOverButton.gameObject.SetActive(true);
                break;
        }
    }

    public void OnClickGameOverButton()
    {
        //TODO: 게임 오버 구현
    }
    
    /*public void SetTurnIconColor(GameManager.PlayerType playerType)
    {
        if (playerType == GameManager.PlayerType.PlayerA)
        {
            playerAIcon.color = new Color32(0, 166, 255, 255);
            playerBIcon.color = Color.white;
        }
        else if (playerType == GameManager.PlayerType.PlayerB)
        {
            playerAIcon.color = Color.white;
            playerBIcon.color = new Color32(255, 0, 94, 255);
        }
        턴 표시 방법2
    }*/
}
