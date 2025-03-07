using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nicknameText;
    [SerializeField] private TMP_Text scoreText;

    public int Index { get; private set; }
    
    public void SetLeaderboard(int rank, string nickname, int score)
    {
        rankText.text = rank.ToString();
        nicknameText.text = nickname;
        scoreText.text = score.ToString();
    }
}
