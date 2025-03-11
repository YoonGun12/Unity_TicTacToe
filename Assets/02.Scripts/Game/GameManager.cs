using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : K_Singleton<GameManager>
{
   [SerializeField] private GameObject settingsPanel;
   [SerializeField] private GameObject confirmPanel;
   [SerializeField] private GameObject signinPanel;
   [SerializeField] private GameObject signupPanel;
   [SerializeField] private GameObject leaderboardPanel;

   private InGameUIController _inGameUIController;
   private Canvas _canvas;
   
   private Constants.GameType _gameType;
   private GameLogic _gameLogic;

   private void Start()
   {
      StartCoroutine(NetworkManager.Instance.GetScore((userInfo) =>
      {
         Debug.Log("자동 로그인 성공" + userInfo);
      }, () =>
      {
         Debug.Log("자동 로그인 실패");
         OpenSigninPanel();
      }));
   }

   public void ChangeToGameScene(Constants.GameType gameType)
   {
      _gameType = gameType;
      SceneManager.LoadScene("Game");
   }

   public void ChangeToMainScene()
   {
      _gameLogic?.Dispose();
      _gameLogic = null;
      SceneManager.LoadScene("Main");
   }

   public void OpenSettingPanel()
   {
      if (_canvas != null)
      {
         var settingsPanelObject = Instantiate(settingsPanel, _canvas.transform);
         settingsPanelObject.GetComponent<PanelController>().Show();
      }
   }
   
   public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClick onConfirmButtonClick)
   {
      if (_canvas != null)
      {
         var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
         confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClick);
      }
   }

   public void OpenSigninPanel()
   {
      if (_canvas != null)
      {
         var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
      }
   }

   public void OpenSignupPanel()
   {
      if (_canvas != null)
      {
         var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
      }
   }

   public void OpenLeaderboardPanel()
   {
      if (_canvas != null)
      {
         var leaderboardPanelObject = Instantiate(leaderboardPanel, _canvas.transform);
      }
   }
   

   public void OpenGameOverPanel()
   {
      _inGameUIController.SetGameUIMode(InGameUIController.GameUIMode.GameOver);
   }

   protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      if (scene.name == "Game")
      {
         //씬에 배치된 오브젝트 찾기 
         var blockController = FindObjectOfType<BlockController>();
         _inGameUIController = FindObjectOfType<InGameUIController>();

         //blockcontroller 초기화
         blockController.InitBlocks();
         
         //InGameUI 초기화
         _inGameUIController.SetGameUIMode(InGameUIController.GameUIMode.Init);
         
         //GameLogic 객체 생성
         if(_gameLogic != null) _gameLogic.Dispose();
         _gameLogic = new GameLogic(blockController, _gameType);
      }

      _canvas = FindObjectOfType<Canvas>();
   }

   private void OnApplicationQuit()
   {
      _gameLogic?.Dispose();
      _gameLogic = null;
   }
}
