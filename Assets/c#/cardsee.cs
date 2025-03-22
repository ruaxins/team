using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cardsee : MonoBehaviour
{
    [Header("��������")]
    public List<card> cards = new List<card>(); // ���������б�
    public Vector2 cardSize = new Vector2(200, 300); // ���Ƴߴ�

    [Header("��������")]
    public float cardSpacing = 150f; // ���Ƽ��
    public float maxRotation = 15f; // �����ת�Ƕ�

    [Header("��ͣЧ��")]
    public float hoverHeight = 80f; // ��̧ͣ���߶�
    public float hoverScale = 1.2f; // ��ͣ���ű���

    private Canvas canvas;

    [SerializeField]
    private List<GameObject> cardObjects = new List<GameObject>(); // ����ʵ���б�

    private int hoverIndex = -1;

    void Start()
    {
        // ��ȡCanvas����
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("δ�ҵ�Canvas����ȷ��cardsee������Canvas���������ϡ�");
            return;
        }

        // ��ʼ������
        InitializeCards();
    }

    void Update()
    {
        UpdateHoverState();
        UpdateLayout();
    }

    // ��ʼ������
    void InitializeCards()
    {
        // ��վɿ���
        foreach (var cardObj in cardObjects)
        {
            if (cardObj != null)
                Destroy(cardObj);
        }
        cardObjects.Clear();

        // �����¿���
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = new GameObject($"Card_{i}");
            cardObj.transform.SetParent(transform);

            // ���UI���
            Image img = cardObj.AddComponent<Image>();
            img.sprite = cards[i].cardFront; // �����ÿ���ͼƬ

            // ����RectTransform
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.sizeDelta = cardSize;
            rt.pivot = new Vector2(0.5f, 0f); // ���ĵ�ײ�����
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0f); // ê����Ļ�ײ�����

            // ��ӵ��б�
            cardObjects.Add(cardObj);
        }
    }

    // ������ͣ״̬
    void UpdateHoverState()
    {
        if (cardObjects == null || cardObjects.Count == 0) return;

        Vector2 mousePos = Input.mousePosition;
        hoverIndex = -1;

        for (int i = 0; i < cardObjects.Count; i++)
        {
            if (cardObjects[i] == null) continue;

            RectTransform rt = cardObjects[i].GetComponent<RectTransform>();
            if (rt != null &&
                RectTransformUtility.RectangleContainsScreenPoint(rt, mousePos))
            {
                hoverIndex = i;
                break;
            }
        }
    }

    // ���¿��Ʋ���
    void UpdateLayout()
    {
        if (cardObjects == null || cardObjects.Count == 0) return;

        // ���㲼�ֲ���
        float totalWidth = (cardObjects.Count - 1) * cardSpacing;
        Vector2 startPos = new Vector2(-totalWidth / 2, 50f); // �ײ����л�׼��

        for (int i = 0; i < cardObjects.Count; i++)
        {
            RectTransform rt = cardObjects[i].GetComponent<RectTransform>();
            if (rt == null) continue;

            bool isHovered = i == hoverIndex;

            // ����ˮƽƫ��
            float xPos = startPos.x + i * cardSpacing;

            // ������ת�Ƕ�
            float rotation = -Mathf.Lerp(
                -maxRotation,
                maxRotation,
                cardObjects.Count > 1 ? (float)i / (cardObjects.Count - 1) : 0.5f
            );

            // ��ͣЧ��
            float yOffset = isHovered ? hoverHeight : 0;
            float scale = isHovered ? hoverScale : 1f;

            // ����Ŀ��λ��
            Vector2 targetPos = new Vector2(xPos, startPos.y + yOffset);

            // Ӧ�ñ任
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * 12f);
            rt.localRotation = Quaternion.Slerp(rt.localRotation, Quaternion.Euler(0, 0, rotation), Time.deltaTime * 10f);
            rt.localScale = Vector3.Lerp(rt.localScale, Vector3.one * scale, Time.deltaTime * 10f);
        }
    }
}