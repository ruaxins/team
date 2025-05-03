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
    public List<string> bank_in_instances = new List<string>();//装备的卡牌
    public List<string> equipement_instance = new List<string>();//

    public Dictionary<string,Card> data_card = new Dictionary<string,Card>();
    public Dictionary<string,Enemy> data_enemy = new Dictionary<string,Enemy>();
    public Dictionary<string, GameObject> instance_card = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> instance_enemy = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> instance_select = new Dictionary<string, GameObject>();

    bool isLock = false;
    public bool IsLock { get => isLock; set => isLock = value; }


    bool enable_x = false;
    public bool Enable_x { get => enable_x; set => enable_x = value; }

    bool enable_e = false;
    public bool Enable_e { get => enable_e; set => enable_e = value; }

    public GameObject Enemy = null;

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