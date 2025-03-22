using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cardsee : MonoBehaviour
{
    [Header("卡牌设置")]
    public List<card> cards = new List<card>(); // 卡牌数据列表
    public Vector2 cardSize = new Vector2(200, 300); // 卡牌尺寸

    [Header("布局设置")]
    public float cardSpacing = 150f; // 卡牌间距
    public float maxRotation = 15f; // 最大旋转角度

    [Header("悬停效果")]
    public float hoverHeight = 80f; // 悬停抬升高度
    public float hoverScale = 1.2f; // 悬停缩放比例

    private Canvas canvas;

    [SerializeField]
    private List<GameObject> cardObjects = new List<GameObject>(); // 卡牌实例列表

    private int hoverIndex = -1;

    void Start()
    {
        // 获取Canvas引用
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("未找到Canvas！请确保cardsee挂载在Canvas的子物体上。");
            return;
        }

        // 初始化卡牌
        InitializeCards();
    }

    void Update()
    {
        UpdateHoverState();
        UpdateLayout();
    }

    // 初始化卡牌
    void InitializeCards()
    {
        // 清空旧卡牌
        foreach (var cardObj in cardObjects)
        {
            if (cardObj != null)
                Destroy(cardObj);
        }
        cardObjects.Clear();

        // 创建新卡牌
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = new GameObject($"Card_{i}");
            cardObj.transform.SetParent(transform);

            // 添加UI组件
            Image img = cardObj.AddComponent<Image>();
            img.sprite = cards[i].cardFront; // 仅设置卡面图片

            // 设置RectTransform
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.sizeDelta = cardSize;
            rt.pivot = new Vector2(0.5f, 0f); // 轴心点底部中心
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0f); // 锚点屏幕底部中心

            // 添加到列表
            cardObjects.Add(cardObj);
        }
    }

    // 更新悬停状态
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

    // 更新卡牌布局
    void UpdateLayout()
    {
        if (cardObjects == null || cardObjects.Count == 0) return;

        // 计算布局参数
        float totalWidth = (cardObjects.Count - 1) * cardSpacing;
        Vector2 startPos = new Vector2(-totalWidth / 2, 50f); // 底部居中基准点

        for (int i = 0; i < cardObjects.Count; i++)
        {
            RectTransform rt = cardObjects[i].GetComponent<RectTransform>();
            if (rt == null) continue;

            bool isHovered = i == hoverIndex;

            // 计算水平偏移
            float xPos = startPos.x + i * cardSpacing;

            // 计算旋转角度
            float rotation = -Mathf.Lerp(
                -maxRotation,
                maxRotation,
                cardObjects.Count > 1 ? (float)i / (cardObjects.Count - 1) : 0.5f
            );

            // 悬停效果
            float yOffset = isHovered ? hoverHeight : 0;
            float scale = isHovered ? hoverScale : 1f;

            // 计算目标位置
            Vector2 targetPos = new Vector2(xPos, startPos.y + yOffset);

            // 应用变换
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * 12f);
            rt.localRotation = Quaternion.Slerp(rt.localRotation, Quaternion.Euler(0, 0, rotation), Time.deltaTime * 10f);
            rt.localScale = Vector3.Lerp(rt.localScale, Vector3.one * scale, Time.deltaTime * 10f);
        }
    }
}