using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Select_Manager : MonoBehaviour
{
    Manager manager = new Manager();
    //��������
    string types;
    //���ƴ���
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
    //���ƿ�ѡ��ָ������
    public void Heart8_plus()
    {
        manager.Get_card(true, types);
    }
    //�����ƶ�ѡ��ָ������
    public void HeartJ_plus()
    {
        manager.Get_card(false, types);
    }
    //������ѡ���Ʋ�ǿ��
    public void Heart10_plus()
    {
        if (type == "diamond")
            Round_Message.RMsg.Player.player_attack_point++;
        else if (type == "spade")
            Round_Message.RMsg.Player.player_armor_point++;
        else
            Debug.Log("�ÿ����޷���ǿ��");

    }
    //������ѡ���Ʋ�ת��
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
            Debug.Log("�ÿ����޷���ת��");
    }
    //������ѡ���Ƹ��Ʋ������ƿ�
    public void ClubA_plus()
    {
        Round_Message.RMsg.bank_in_instances.Add(types);
        manager.Get_card();
    }
}
