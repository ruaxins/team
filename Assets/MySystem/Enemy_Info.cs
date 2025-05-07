using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy_Info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("信息显示")]
    [SerializeField] private GameObject infoBox;
    [SerializeField] private Text descriptionText;

    Manager manager = new Manager();

    void Awake()
    {
        infoBox.SetActive(false);
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
    }
    string GetCardDescription()
    {
        foreach (var type in Message.Msg.instance_enemy)
        {
            if (type.Value == gameObject)
            {
                switch (manager.Get_Enemy_Data(type.Key).attack_mode)
                {
                    case 0: return "下次行动：" + "对玩家造成其攻击值的伤害";
                    case 1: return "下次行动：" + "获得2层增强";
                    case 2: return "下次行动：" + "获得5点护甲";
                    case 3: return "下次行动：" + "分裂";
                    case 4: return "下次行动：" + "获得被玩家攻击失去生命点数的护甲，并反弹攻击值的5%伤害给攻击者";
                    case 5: return "下次行动：" + "玩家获得一层自刃";
                    case 6: return "下次行动：" + "灼烧随机一张手牌，无法使用";
                    case 7: return "下次行动：" + "获得15点护甲";
                    case 8: return "下次行动：" + "玩家获得2层自刃";
                    case 9: return "下次行动：" + "获得5层增强";
                    case 10: return "下次行动：" + "获得4层抵挡";
                    default: return "No Message";
                }
            }
        }
        return "No Message";
    }
}
