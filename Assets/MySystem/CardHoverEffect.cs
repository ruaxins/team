using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardHoverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("悬停效果")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float hoverHeight = 30f;
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    public Vector2 originalPosition;
    public Vector2 originalScale;
    public bool isInteractable = true;
    private int originalSiblingIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // 提升显示层级（避免被其他卡牌遮挡）
        originalSiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        // 动画效果
        transform.DOScale(originalScale * hoverScale, animDuration).SetEase(easeType);
        transform.DOMoveY(originalPosition.y + hoverHeight, animDuration).SetEase(easeType);

        // 可选：播放音效
        //AudioManager.PlayHoverSound();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // 复位动画
        transform.DOScale(originalScale, animDuration).SetEase(easeType);
        transform.DOMove(originalPosition, animDuration).SetEase(easeType);

        // 恢复层级
        transform.SetSiblingIndex(originalSiblingIndex);
    }
}
