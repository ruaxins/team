using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardHoverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [Header("悬停效果")]
    [SerializeField] private float hoverScale = 1.5f;
    [SerializeField] private float hoverHeight = 50f;
    [SerializeField] private float animDuration = 1f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    [Header("信息显示")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text descriptionText;

    public Vector2 originalPosition;
    public Vector2 originalScale;
    public bool isInteractable = true;
    private int originalSiblingIndex;

    void Awake()
    {
        infoPanel = transform.Find("Detial").gameObject;
        descriptionText = infoPanel.GetComponent<Text>();
        infoPanel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // 提升显示层级（避免被其他卡牌遮挡）
        //originalSiblingIndex = transform.GetSiblingIndex();
        //transform.SetAsLastSibling();
        // 动画效果
        transform.DOScale(originalScale * hoverScale, animDuration).SetEase(easeType);
        transform.DOMoveY(originalPosition.y + hoverHeight, animDuration).SetEase(easeType);

        // 显示信息面板
        infoPanel.SetActive(true);
        descriptionText.text = GetCardDescription(); // 自定义方法获取卡牌描述

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
        //transform.SetSiblingIndex(originalSiblingIndex);

        // 隐藏信息面板
        infoPanel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(Round_Message.RMsg.Hand_out_card_num);
        //if (Round_Message.RMsg.Hand_out_card_num >= Round_Message.RMsg.Hand_out_card_num_max) return;
        //isInteractable = !isInteractable;

        //示例：选中卡牌效果
        //GetComponent<Image>().color = Color.yellow;

        // 触发卡牌使用逻辑
        //CardManager.Instance.SelectCard(this);
    }
    string GetCardDescription()
    {
        return "yes";
    }
    private void OnDestroy()
    {
        originalScale = new Vector2(1,1);
        gameObject.transform.localScale = originalScale;
    }
}
