using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardHoverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("��ͣЧ��")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float hoverHeight = 30f;
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    [Header("��Ϣ��ʾ")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text descriptionText;

    public Vector2 originalPosition;
    public Vector2 originalScale;
    public bool isInteractable = true;
    private int originalSiblingIndex;

    Manager manager = new Manager();

    void Awake()
    {
        infoPanel = transform.Find("Detial").gameObject;
        descriptionText = infoPanel.GetComponent<Text>();
        infoPanel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // ������ʾ�㼶�����ⱻ���������ڵ���
        originalSiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        // ����Ч��
        transform.DOScale(originalScale * hoverScale, animDuration).SetEase(easeType);
        transform.DOMoveY(originalPosition.y + hoverHeight, animDuration).SetEase(easeType);

        // ��ʾ��Ϣ���
        infoPanel.SetActive(true);
        descriptionText.text = GetCardDescription(); // �Զ��巽����ȡ��������

        // ��ѡ��������Ч
        //AudioManager.PlayHoverSound();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        // ��λ����
        transform.DOScale(originalScale, animDuration).SetEase(easeType);
        transform.DOMove(originalPosition, animDuration).SetEase(easeType);

        // �ָ��㼶
        transform.SetSiblingIndex(originalSiblingIndex);

        // ������Ϣ���
        infoPanel.SetActive(false);
    }
    string GetCardDescription()
    {
        foreach (var type in Message.Msg.instance_card)
        {
            if (type.Value == gameObject)
            {
                return manager.Get_Card_Data(type.Key).skill_message;
            }
        }
        return "No Message";
    }
}
