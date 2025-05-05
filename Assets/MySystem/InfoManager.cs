using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("信息显示")]
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameObject infoBox_plus;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject infoPanel_plus;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text descriptionText_plus;

    Manager manager = new Manager();

    void Awake()
    {
        infoBox = transform.Find("Box").gameObject;
        infoBox_plus = transform.Find("Box_plus").gameObject;
        infoPanel = infoBox.transform.Find("Detial").gameObject;
        infoPanel_plus = infoBox_plus.transform.Find("Detial_plus").gameObject;
        descriptionText = infoPanel.GetComponent<Text>();
        descriptionText_plus = infoPanel_plus.GetComponent<Text>();
        infoBox.SetActive(false);
        infoBox_plus.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 显示信息面板
        infoBox.SetActive(true);
        descriptionText.text = GetCardDescription(); // 自定义方法获取卡牌描述

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // 隐藏信息面板
        infoBox.SetActive(false);
        infoBox_plus.SetActive(false);
    }
    string GetCardDescription()
    {
        infoBox_plus.SetActive(true);
        foreach (var type in Message.Msg.instance_card)
        {
            if (type.Value == gameObject)
            {
                switch (type.Key)
                {
                    case "heart7": descriptionText_plus.text = "自刃：原生命值 - 5 % 生命值（n层 = 叠加）。\n" + "增强：原攻击 *（1 + 5 %）（n层 = +5 %×n）。"; break;
                    case "heart8": descriptionText_plus.text = "自刃：原生命值 - 5 % 生命值（n层 = 叠加）。"; break;
                    case "heartJ": descriptionText_plus.text = "自刃：原生命值 - 5 % 生命值（n层 = 叠加）。"; break;
                    case "club3": descriptionText_plus.text = "增强：原攻击 *（1 + 5 %）（n层 = +5 %×n）。"; break;
                    case "club4": descriptionText_plus.text = "抵挡：原防御 *（1 + 5 %）（n层 = +5 %×n）。"; break;
                    case "club5": descriptionText_plus.text = "自刃：原生命值 - 5 % 生命值（n层 = 叠加）。"; break;
                    case "curse5": descriptionText_plus.text = "增强：原攻击 *（1 + 5 %）（n层 = +5 %×n）。"; break;
                    case "curse4": descriptionText_plus.text = "抵挡：原防御 *（1 + 5 %）（n层 = +5 %×n）。"; break;
                    case "curse3": descriptionText_plus.text = "自刃：原生命值 - 5 % 生命值（n层 = 叠加）。"; break;
                    default:
                        infoBox_plus.SetActive(false);
                        break;
                }
                return manager.Get_Card_Data(type.Key).skill_message;
            }
        }
        return "No Message";
    }
}
