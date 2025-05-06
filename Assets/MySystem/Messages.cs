using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //游戏内全局变量
    public List<string> card_bank = new List<string>();//卡牌图鉴
    public List<string> select_instances = new List<string>();//选择界面卡牌
    public List<string> enemy_in_instances = new List<string>();//战斗的怪物
    public List<string> bank_in_instances = new List<string>();//装备的手牌
    public List<string> equip_instances = new List<string>();//装备牌
    public List<string> equipement_instance = new List<string>();//已购买的卡牌
    public List<string> shop_instances = new List<string>();//商店

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
    //场景内敌人实例
    public GameObject Enemy = null;
    //背景音乐
    public AudioClip backclip = null;
    //金钱
    private int money = 100;
    public int Money { get => money; set => money = value; }

    //购买的卡牌
    public string Buy_Card = null;
    //可装备的梅花牌数量
    public int Equip_Num_Max = 3;
    //已装备的梅花牌数量
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