using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    public delegate void PanelControllerHideDelegate();
    
    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel 표시 함수
    /// </summary>
    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.5f).SetEase(Ease.InOutBack);
    }

    /// <summary>
    /// Panel 숨기기 함수
    /// </summary>
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;
        
        _backgroundCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(()=>
        {
            hideDelegate?.Invoke();
            Destroy(gameObject);
        });
    }
}
