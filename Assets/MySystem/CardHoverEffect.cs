using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardHoverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [Header("��ͣЧ��")]
    [SerializeField] private float hoverScale = 1.5f;
    [SerializeField] private float hoverHeight = 50f;
    [SerializeField] private float animDuration = 1f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    [Header("��Ϣ��ʾ")]
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

        // ������ʾ�㼶�����ⱻ���������ڵ���
        //originalSiblingIndex = transform.GetSiblingIndex();
        //transform.SetAsLastSibling();
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
        //transform.SetSiblingIndex(originalSiblingIndex);

        // ������Ϣ���
        infoPanel.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(Round_Message.RMsg.Hand_out_card_num);
        //if (Round_Message.RMsg.Hand_out_card_num >= Round_Message.RMsg.Hand_out_card_num_max) return;
        //isInteractable = !isInteractable;

        //ʾ����ѡ�п���Ч��
        //GetComponent<Image>().color = Color.yellow;

        // ��������ʹ���߼�
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
