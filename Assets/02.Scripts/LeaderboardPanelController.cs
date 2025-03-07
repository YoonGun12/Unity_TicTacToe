using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanelController : MonoBehaviour
{
    [SerializeField] private float cellHeight;
    [SerializeField] private GameObject scrollViewObject;
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;
    
    private ScoreInfo[] _leaderboardData; 
    private LinkedList<Cell> _visibleCells; // 현재 표시 중인 셀 목록
    private float _lastYValue = 1f;

    private void Awake()
    {
        _scrollRect = scrollViewObject.GetComponent<ScrollRect>();
        _rectTransform = scrollViewObject.GetComponent<RectTransform>();
        _visibleCells = new LinkedList<Cell>();

        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        StartCoroutine(NetworkManager.Instance.GetLeaderboard(OnLeaderboardReceived, OnLeaderboardFailed));
    }

    private void OnLeaderboardReceived(Scores leaderboardData)
    {
        Debug.Log($"리더보드 데이터 로드 완료 {leaderboardData.scores.Length}명.");
        
        _leaderboardData = leaderboardData.scores; // 서버에서 받은 데이터 저장
        ReloadData(); // UI 업데이트
    }

    private void OnLeaderboardFailed()
    {
        Debug.LogError("리더보드 데이터 불러오기 실패!");
    }

    private (int startIndex, int endIndex) GetVisibleIndexRange()
    {
        var visibleRect = new Rect(
            _scrollRect.content.anchoredPosition.x,
            _scrollRect.content.anchoredPosition.y,
            _rectTransform.rect.width,
            _rectTransform.rect.height);

        int startIndex = Mathf.FloorToInt(visibleRect.y / cellHeight);
        int visibleCount = Mathf.CeilToInt(_rectTransform.rect.height / cellHeight);

        startIndex = Mathf.Max(0, startIndex - 1);
        visibleCount += 2;

        return (startIndex, startIndex + visibleCount - 1);
    }

    private bool IsVisibleIndex(int index)
    {
        var (startIndex, endIndex) = GetVisibleIndexRange();
        endIndex = Mathf.Min(endIndex, _leaderboardData.Length - 1);
        return startIndex <= index && index <= endIndex;
    }

    private void ReloadData()
    {
        _visibleCells.Clear();

        var contentSizeDelta = _scrollRect.content.sizeDelta;
        contentSizeDelta.y = _leaderboardData.Length * cellHeight;
        _scrollRect.content.sizeDelta = contentSizeDelta;

        var (startIndex, endIndex) = GetVisibleIndexRange();
        var maxEndIndex = Mathf.Min(endIndex, _leaderboardData.Length - 1);

        for (int i = startIndex; i <= maxEndIndex; i++)
        {
            var cellObject = ObjectPool.Instance.GetObject();
            var cell = cellObject.GetComponent<Cell>();
            cell.SetLeaderboard(i + 1, _leaderboardData[i].nickname, _leaderboardData[i].score);
            cell.transform.localPosition = new Vector3(0, -i * cellHeight, 0);
            
            _visibleCells.AddLast(cell);
        }
    }

    public void OnValueChanged(Vector2 value)
    {
        if (_leaderboardData == null || _leaderboardData.Length == 0) return;

        if (_lastYValue < value.y) // 위로 스크롤
        {
            var firstCell = _visibleCells.First.Value;
            int newFirstIndex = firstCell.Index - 1;

            if (IsVisibleIndex(newFirstIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetLeaderboard(newFirstIndex + 1, _leaderboardData[newFirstIndex].nickname, _leaderboardData[newFirstIndex].score);
                cell.transform.localPosition = new Vector3(0, -newFirstIndex * cellHeight, 0);
                _visibleCells.AddFirst(cell);
            }

            var lastCell = _visibleCells.Last.Value;
            if (!IsVisibleIndex(lastCell.Index))
            {
                ObjectPool.Instance.ReturnObject(lastCell.gameObject);
                _visibleCells.RemoveLast();
            }
        }
        else // 아래로 스크롤
        {
            var lastCell = _visibleCells.Last.Value;
            int newLastIndex = lastCell.Index + 1;

            if (IsVisibleIndex(newLastIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetLeaderboard(newLastIndex + 1, _leaderboardData[newLastIndex].nickname, _leaderboardData[newLastIndex].score);
                cell.transform.localPosition = new Vector3(0, -newLastIndex * cellHeight, 0);
                _visibleCells.AddLast(cell);
            }

            var firstCell = _visibleCells.First.Value;
            if (!IsVisibleIndex(firstCell.Index))
            {
                ObjectPool.Instance.ReturnObject(firstCell.gameObject);
                _visibleCells.RemoveFirst();
            }
        }

        _lastYValue = value.y;
    }
    
    public void OnClickConfirmButton()
    {
        Destroy(gameObject);
    }
}
