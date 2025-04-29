using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Messages : MonoBehaviour
{

}
public class Round_Message
{
    //��Ϸ�ڱ���
    public List<string> round_end_action = new List<string>();//�����ж��б�
    public List<string> skill_action = new List<string>();//�����ж��б�

    public List<string> bank_in_instances = new List<string>();//�ƿ�
    public List<string> bank_out_instances = new List<string>();//���ƶ�
    public List<string> hand_in_instances = new List<string>();//����ʵ��
    public List<string> hand_out_instances = new List<string>();//����ʵ��
    public List<string> enemy_instances = new List<string>();//����ʵ��
    public List<string> equipment_instances = new List<string>();//װ��ʵ��

    public List<string> select_action = new List<string>();

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

    //���ƴ���
    int round = 0;
    public int Round { get => round; set => round = value; }
    //�����ƴ���
    int maxround = 3;
    public int MaxRound { get => maxround; set => maxround = value; }
    //���ƴ���
    int dropround = 0;
    public int DropRound { get => dropround; set => dropround = value; }
    //������ƴ���
    int maxdropround = 3;
    public int MaxDropRound { get => maxdropround; set => maxdropround = value; }

    //��ǰѡ�����
    Enemy enemy_now = null;
    public Enemy Enemy_Now { get => enemy_now; set => enemy_now = value; }
    //��ǰѡ����
    string card_now = null;
    public string Card_Now { get => card_now; set => card_now = value; }
    //���
    Player player = null;
    public Player Player { get => player; set => player = value; }

    //�Ƿ�����
    bool iscomplete = false;
    public bool IsComplete { get => iscomplete; set => iscomplete = value; }

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
