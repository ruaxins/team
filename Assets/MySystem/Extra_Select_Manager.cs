using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Select_Manager : MonoBehaviour
{
    Manager manager = new Manager();
    //卡牌类型
    string types;
    //卡牌大类
    string type;
    private void Start()
    {
        foreach (var s in Message.Msg.instance_card)
        {
            if (s.Value == gameObject)
            {
                types = s.Key;
                break;
            }
        }
        type = manager.Get_Card_Data(types).type;
    }
    //从牌库选择指定卡牌
    public void Heart8_plus()
    {
        manager.Get_card(true, types);
    }
    //从弃牌堆选择指定卡牌
    public void HeartJ_plus()
    {
        manager.Get_card(false, types);
    }
    //从手牌选择卡牌并强化
    public void Heart10_plus()
    {
        if (type == "diamond")
            Round_Message.RMsg.Player.player_attack_point++;
        else if (type == "spade")
            Round_Message.RMsg.Player.player_armor_point++;
        else
            Debug.Log("该卡牌无法被强化");

    }
    //从手牌选择卡牌并转换
    public void HeartA_plus()
    {
        if (type == "diamond")
        {
            Round_Message.RMsg.Player.player_attack_point -= manager.Get_Card_Data(types).point;
            Round_Message.RMsg.Player.player_armor_point += manager.Get_Card_Data(types).point;
        }
        else if (type == "spade")
        {
            Round_Message.RMsg.Player.player_attack_point += manager.Get_Card_Data(types).point;
            Round_Message.RMsg.Player.player_armor_point -= manager.Get_Card_Data(types).point;
        }
        else
            Debug.Log("该卡牌无法被转换");
    }
    //从手牌选择卡牌复制并加入牌库
    public void ClubA_plus()
    {
        Round_Message.RMsg.bank_in_instances.Add(types);
        manager.Get_card();
    }
}
