using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Messages : MonoBehaviour
{

}
public class Round_Message
{
    //��Ϸ��ȫ�ֱ���
    public List<Card> equipment_bar = new List<Card>();//װ����
    public List<Card> bank_in_cards = new List<Card>();//�ƿ�
    public List<Card> bank_out_cards = new List<Card>();//���ƶ�
    public List<Card> hand_in_card_list = new List<Card>();//����
    public List<Card> hand_out_card_list = new List<Card>();//�����б�
    public List<string> round_end_action = new List<string>();//�����ж��б�
    public List<Enemy> enemy_fight = new List<Enemy>();//ս���еĹ���
    public List<GameObject> hand_in_instances = new List<GameObject>();//����ʵ��
    public List<GameObject> hand_out_instances = new List<GameObject>();//�����б�ʵ��
    public List<GameObject> enemy_instances = new List<GameObject>();//����ʵ��
    public List<GameObject> equipment_instances = new List<GameObject>();//װ����ʵ��
    public Queue<GameObject> pool = new Queue<GameObject>();//����ʵ����
    public Queue<GameObject> equipement_pool = new Queue<GameObject>();//װ��ʵ����
    public Queue<GameObject> enemy_pool = new Queue<GameObject>();//�����
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

    int dropround = 0;
    public int DropRound { get => dropround; set => dropround = value; }
    //���غ���
    int maxdropround = 3;
    public int MaxDropRound { get => maxdropround; set => maxdropround = value; }

    //clubJ
    bool clubJ = false;
    public bool ClubJ { get => clubJ; set => clubJ = value; }

    //��ǰѡ��Ŀ���
    int card_choose = 0;
    public int Card_choose { get => card_choose; set => card_choose = value; }
    //��ǰѡ�����
    Enemy enemy_now = null;
    public Enemy Enemy_Now { get => enemy_now; set => enemy_now = value; }
    //���
    Player player = null;
    public Player Player { get => player; set => player = value; }

    private static Round_Message round_message;
    private Round_Message() { }
    public static Round_Message RMsg
    {
        get
        {
            if (round_message == null)
            {
                round_message = new Round_Message();
            }
            return round_message;
        }
    }
}
