using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //��Ϸ��ȫ�ֱ���
    public List<string> card_bank = new List<string>();//����ͼ��
    public List<string> select_instances = new List<string>();//ѡ����濨��
    public List<string> enemy_in_instances = new List<string>();//ս���Ĺ���
    public List<string> bank_in_instances = new List<string>();//װ��������
    public List<string> equip_instances = new List<string>();//װ����
    public List<string> equipement_instance = new List<string>();//�ѹ���Ŀ���
    public List<string> shop_instances = new List<string>();//�̵�

    public Dictionary<string,Card> data_card = new Dictionary<string,Card>();
    public Dictionary<string,Enemy> data_enemy = new Dictionary<string,Enemy>();
    public Dictionary<string, GameObject> instance_card = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> instance_enemy = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> instance_select = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> instance_shop = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> instance_equip = new Dictionary<string, GameObject>();

    bool isLock = false;
    public bool IsLock { get => isLock; set => isLock = value; }


    bool enable_x = false;
    public bool Enable_x { get => enable_x; set => enable_x = value; }

    bool enable_e = true;
    public bool Enable_e { get => enable_e; set => enable_e = value; }
    bool enable_tab = true;
    public bool Enable_Tab { get => enable_tab; set => enable_tab = value; }
    //�����ڵ���ʵ��
    public GameObject Enemy = null;
    //��������
    public AudioClip backclip = null;
    //��Ǯ
    private int money = 100;
    public int Money { get => money; set => money = value; }

    //����Ŀ���
    public string Buy_Card = null;
    //��װ����÷��������
    public int Equip_Num_Max = 3;
    //��װ����÷��������
    public int Equip_Num = 0;

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