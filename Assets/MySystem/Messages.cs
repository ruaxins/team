using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //��Ϸ��ȫ�ֱ���
    public List<Card> temp = new List<Card>();//��ʱ�ƿ�
    public List<Card> bank = new List<Card>();//ͼ��
    public List<Card> equipment_bar = new List<Card>();//װ����
    public List<Card> bank_in_cards = new List<Card>();//�ƿ�
    public List<Card> bank_out_cards = new List<Card>();//���ƶ�
    public List<Card> hand_in_card_list = new List<Card>();//����
    public List<Card> hand_out_card_list = new List<Card>();//�����б�
    public List<string> round_end_action = new List<string>();//�����ж��б�
    public List<Enemy> enemy_bank = new List<Enemy>();//�����
    public List<Enemy> enemy_fight = new List<Enemy>();//ս���еĹ���
    //���������
    int hand_in_card_num_max = 8;
    public int Hand_in_card_num_max { get => hand_in_card_num_max; set => hand_in_card_num_max = value; }
    //��ǰ������
    int hand_in_card_num = 0;
    public int Hand_in_card_num { get => hand_in_card_num; set => hand_in_card_num = value; }
    //��������
    int hand_out_card_num_max = 5;
    public int Hand_out_card_num_max { get => hand_out_card_num_max; set => hand_out_card_num_max = value; }
    //��ǰ������
    int hand_out_card_num = 0;
    public int Hand_out_card_num { get => hand_out_card_num; set => hand_out_card_num = value; }

    //0Ϊ����غϣ�1,2,3Ϊ��һغϣ�ÿ�غϽ��������øûغϼӳɣ��غ�0�����������ܻغϼӳɣ���ս����������ȫ�ּӳ�
    int round = 1;
    public int Round { get => round; set => round = value; }
    //���غ���
    int maxround = 3;
    public int MaxRound { get => maxround; set => maxround = value; }

    //clubJ
    bool clubJ = false;
    public bool ClubJ { get => clubJ; set => clubJ = value; }

    //��ǰѡ��Ŀ���
    int card_choose = 0;
    public int Card_choose { get => card_choose; set => card_choose = value; }

    private static Message message;
    private Message() { }
    public static Message Msg
    {
        get
        {
            if (message == null)
            {
                message = new Message();
            }
            return message;
        }
    }
}
