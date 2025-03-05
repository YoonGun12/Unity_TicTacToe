using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public struct SigninData
{
    public string username;
    public string password;
}

public struct SigninResult
{
    public int result;
}

public class SigninPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    public void OnClickSigninButton()
    {
        var username = _usernameInputField.text;
        var password = _passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            //TODO: 누락된 값 입력 요청 팝업 표시
            return;
        }

        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        StartCoroutine(Signin(signinData));
    }

    IEnumerator Signin(SigninData signinData)
    {
        string jsonString = JsonUtility.ToJson(signinData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/users/signin", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                
            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);

                if (result.result == 0)
                {
                    //유저 네임이 유효하지 않음
                    GameManager.Instance.OpenConfirmPanel("해당하는 유저네임이 없습니다.", () =>
                    {
                        _usernameInputField.text = "";
                    });
                }
                else if (result.result == 1)
                {
                    //패스워드가 유효하지 않음
                    GameManager.Instance.OpenConfirmPanel("패스워드가 틀렸습니다.", () =>
                    {
                        _passwordInputField.text = "";
                    });
                }
                else if (result.result == 2)
                {
                    //성공
                    GameManager.Instance.OpenConfirmPanel("로그인 성공하였습니다.", () =>
                    {
                        Destroy(gameObject);
                    });
                }
            }
        }
    }

    public void OnClickSignupButton()
    {
        
    }
}
