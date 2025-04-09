using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cardsee : MonoBehaviour
{
    [Header("卡牌设置")]
    admin adminScript;
    public List<card> hand_in_card_list = new List<card>(); // 卡牌数据列表
    public Vector2 hand_in_card_listize = new Vector2(200, 300); // 卡牌尺寸

    [Header("布局设置")]
    public float radius = 200f;      // 扇形半径
    public float maxAngle = 60f;     // 扇形总角度

    [Header("悬停效果")]
    public float hoverHeight = 80f; // 悬停抬升高度
    public float hoverScale = 1.2f; // 悬停缩放比例

    [Header("动画设置")]
    public float positionLerpSpeed = 12f;
    public float rotationLerpSpeed = 10f;

    private List<GameObject> cardObjects = new List<GameObject>(); // 卡牌实例列表
    private Canvas canvas;

    public Transform foldc;

    void Start()
    {
        adminScript = GetComponent<admin>();
        canvas = GetComponentInParent<Canvas>();
        Initializehand_in_card_list();
    }

    public void Initializehand_in_card_list()
    {
        hand_in_card_list = adminScript.hand_in_card_list;
        // 清空旧卡牌
        foreach (var obj in cardObjects) Destroy(obj);
        cardObjects.Clear();

        for (int i = 0; i < hand_in_card_list.Count; i++)
        {
            GameObject cardObj = new GameObject($"Card_{i}", typeof(Image), typeof(card2));
            cardObj.transform.SetParent(transform);

            // 设置卡牌图片
            Image img = cardObj.GetComponent<Image>();
            img.sprite = hand_in_card_list[i].cardFront;
            img.raycastTarget = true;

            // 设置卡牌尺寸
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.sizeDelta = hand_in_card_listize;
            rt.pivot = rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            cardObj.GetComponent<card2>().controller= hand_in_card_list[i].GetComponent<card>();

            // 初始化控制器
            var controller = cardObj.GetComponent<card2>();
            controller.Initialize(this, i);
            hand_in_card_list[i].controller = controller;

            cardObjects.Add(cardObj);
        }
        for (int i = 0; i < cardObjects.Count; i++)
        {
            var controller = hand_in_card_list[i].controller;
            if (controller.IsDragging) continue;

            // 计算角度（从左侧到右侧均匀分布）
            float angle = -maxAngle / 2f;
            if (cardObjects.Count > 1)
                angle += (i / (float)(cardObjects.Count - 1)) * maxAngle;

            // 计算位置（极坐标转笛卡尔）
            float radian = angle * Mathf.Deg2Rad;
            float x = radius * Mathf.Sin(radian);
            float y = radius * Mathf.Cos(radian); // 圆心位于 (0, -radius)

            // 计算旋转（卡牌朝向圆心）
            float targetRot = -angle; // 旋转角度与角度相反

            controller.SetTarget(new Vector2(x, y), targetRot, Vector3.one);
        }
    }
}