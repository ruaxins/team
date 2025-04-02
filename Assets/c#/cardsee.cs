using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cardsee : MonoBehaviour
{
    [Header("��������")]
    admin adminScript;
    public List<card> hand_in_card_list = new List<card>(); // ���������б�
    public Vector2 hand_in_card_listize = new Vector2(200, 300); // ���Ƴߴ�

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
        Initializehand_in_card_list();
    }

    public void Initializehand_in_card_list()
    {
        hand_in_card_list = adminScript.hand_in_card_list;
        // ��վɿ���
        foreach (var obj in cardObjects) Destroy(obj);
        cardObjects.Clear();

        for (int i = 0; i < hand_in_card_list.Count; i++)
        {
            GameObject cardObj = new GameObject($"Card_{i}", typeof(Image), typeof(card2));
            cardObj.transform.SetParent(transform);

            // ���ÿ���ͼƬ
            Image img = cardObj.GetComponent<Image>();
            img.sprite = hand_in_card_list[i].cardFront;
            img.raycastTarget = true;

            // ���ÿ��Ƴߴ�
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.sizeDelta = hand_in_card_listize;
            rt.pivot = rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            cardObj.GetComponent<card2>().controller= hand_in_card_list[i].GetComponent<card>();

            // ��ʼ��������
            var controller = cardObj.GetComponent<card2>();
            controller.Initialize(this, i);
            hand_in_card_list[i].controller = controller;

            cardObjects.Add(cardObj);
        }
        for (int i = 0; i < cardObjects.Count; i++)
        {
            var controller = hand_in_card_list[i].controller;
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