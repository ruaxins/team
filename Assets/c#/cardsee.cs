using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cardsee : MonoBehaviour
{
    [Header("��������")]
    admin adminScript;
    public List<card> cards = new List<card>(); // ���������б�
    public Vector2 cardSize = new Vector2(200, 300); // ���Ƴߴ�

    [Header("��������")]
    public float radius = 200f;      // ���ΰ뾶
    public float maxAngle = 60f;     // �����ܽǶ�

    [Header("��ͣЧ��")]
    public float hoverHeight = 80f; // ��̧ͣ���߶�
    public float hoverScale = 1.2f; // ��ͣ���ű���

    [Header("��������")]
    public float positionLerpSpeed = 12f;
    public float rotationLerpSpeed = 10f;

    private List<GameObject> cardObjects = new List<GameObject>(); // ����ʵ���б�
    private Canvas canvas;

    public Transform foldc;

    void Start()
    {
        adminScript = GetComponent<admin>();
        canvas = GetComponentInParent<Canvas>();
        InitializeCards();
    }

    public void InitializeCards()
    {
        cards = adminScript.cards;
        // ��վɿ���
        foreach (var obj in cardObjects) Destroy(obj);
        cardObjects.Clear();

        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardObj = new GameObject($"Card_{i}", typeof(Image), typeof(card2));
            cardObj.transform.SetParent(transform);

            // ���ÿ���ͼƬ
            Image img = cardObj.GetComponent<Image>();
            img.sprite = cards[i].cardFront;
            img.raycastTarget = true;

            // ���ÿ��Ƴߴ�
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.sizeDelta = cardSize;
            rt.pivot = rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            cardObj.GetComponent<card2>().controller= cards[i].GetComponent<card>();

            // ��ʼ��������
            var controller = cardObj.GetComponent<card2>();
            controller.Initialize(this, i);
            cards[i].controller = controller;

            cardObjects.Add(cardObj);
        }
        for (int i = 0; i < cardObjects.Count; i++)
        {
            var controller = cards[i].controller;
            if (controller.IsDragging) continue;

            // ����Ƕȣ�����ൽ�Ҳ���ȷֲ���
            float angle = -maxAngle / 2f;
            if (cardObjects.Count > 1)
                angle += (i / (float)(cardObjects.Count - 1)) * maxAngle;

            // ����λ�ã�������ת�ѿ�����
            float radian = angle * Mathf.Deg2Rad;
            float x = radius * Mathf.Sin(radian);
            float y = radius * Mathf.Cos(radian); // Բ��λ�� (0, -radius)

            // ������ת�����Ƴ���Բ�ģ�
            float targetRot = -angle; // ��ת�Ƕ���Ƕ��෴

            controller.SetTarget(new Vector2(x, y), targetRot, Vector3.one);
        }
    }
}