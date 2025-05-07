using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy_Info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("��Ϣ��ʾ")]
    [SerializeField] private GameObject infoBox;
    [SerializeField] private Text descriptionText;

    Manager manager = new Manager();

    void Awake()
    {
        infoBox.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ʾ��Ϣ���
        infoBox.SetActive(true);
        descriptionText.text = GetCardDescription(); // �Զ��巽����ȡ��������
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // ������Ϣ���
        infoBox.SetActive(false);
    }
    string GetCardDescription()
    {
        foreach (var type in Message.Msg.instance_enemy)
        {
            if (type.Value == gameObject)
            {
                switch (manager.Get_Enemy_Data(type.Key).attack_mode)
                {
                    case 0: return "�´��ж���" + "���������乥��ֵ���˺�";
                    case 1: return "�´��ж���" + "���2����ǿ";
                    case 2: return "�´��ж���" + "���5�㻤��";
                    case 3: return "�´��ж���" + "����";
                    case 4: return "�´��ж���" + "��ñ���ҹ���ʧȥ���������Ļ��ף�����������ֵ��5%�˺���������";
                    case 5: return "�´��ж���" + "��һ��һ������";
                    case 6: return "�´��ж���" + "�������һ�����ƣ��޷�ʹ��";
                    case 7: return "�´��ж���" + "���15�㻤��";
                    case 8: return "�´��ж���" + "��һ��2������";
                    case 9: return "�´��ж���" + "���5����ǿ";
                    case 10: return "�´��ж���" + "���4��ֵ�";
                    default: return "No Message";
                }
            }
        }
        return "No Message";
    }
}
