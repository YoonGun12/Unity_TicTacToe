using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_TestGameManager : MonoBehaviour
{
    public void Open()
    {
        K_PopupPanelController.Instance.Show("Hello", "OK",true, () =>
        {
            Debug.Log("OK 클릭");
        });
    }
}
