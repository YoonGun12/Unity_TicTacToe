using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{

    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpr;
    
    public enum MarkerType {None, O, X}

    public delegate void OnBlockClicked(int index);
    private OnBlockClicked _onBlockClicked;
    private int _blockIndex;
    private SpriteRenderer _spriteRenderer;
    private Color _defalutColor;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defalutColor = _spriteRenderer.color;
    }

    /// <summary>
    /// 블럭의 색상을 변경하는 함수
    /// </summary>
    /// <param name="color">색상</param>
    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    /// <summary>
    /// Block 초기화 함수
    /// </summary>
    /// <param name="blockIndex">인덱스</param>
    /// <param name="onBlockClicked">터치 이벤트</param>
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        this._onBlockClicked = onBlockClicked;
        SetColor(_defalutColor);
    }
    
    /// <summary>
    /// 어떤 마커를 표시할지 전달하는 함수
    /// </summary>
    /// <param name="markerType">마커타입</param>
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.O:
                markerSpr.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSpr.sprite = xSprite;
                break;
            case MarkerType.None:
                markerSpr.sprite = null;
                break;
        }
    }

    //마우스가 눌렸다가 같은 오브젝트에서 뗐을때 UI가 아닌 게임 오브젝트에 적용, collider가 필수
    //스스로 판단 x, 상위객체를 통해 관리
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        _onBlockClicked?.Invoke(_blockIndex);
    }
}
