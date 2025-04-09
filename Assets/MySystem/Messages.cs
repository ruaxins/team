using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

}
public class Message
{
    //游戏内全局变量
    public List<Card> temp = new List<Card>();//临时牌库
    public List<Card> bank = new List<Card>();//图鉴
    public List<Card> equipment_bar = new List<Card>();//装备栏
    public List<Card> bank_in_cards = new List<Card>();//牌库
    public List<Card> bank_out_cards = new List<Card>();//弃牌堆
    public List<Card> hand_in_card_list = new List<Card>();//手牌
    public List<Card> hand_out_card_list = new List<Card>();//出牌列表
    public List<string> round_end_action = new List<string>();//结束行动列表
    public List<Enemy> enemy_bank = new List<Enemy>();//怪物池
    public List<Enemy> enemy_fight = new List<Enemy>();//战斗中的怪物
    //最大手牌数
    int hand_in_card_num_max = 8;
    public int Hand_in_card_num_max { get => hand_in_card_num_max; set => hand_in_card_num_max = value; }
    //当前手牌数
    int hand_in_card_num = 0;
    public int Hand_in_card_num { get => hand_in_card_num; set => hand_in_card_num = value; }
    //最大出牌数
    int hand_out_card_num_max = 5;
    public int Hand_out_card_num_max { get => hand_out_card_num_max; set => hand_out_card_num_max = value; }
    //当前出牌数
    int hand_out_card_num = 0;
    public int Hand_out_card_num { get => hand_out_card_num; set => hand_out_card_num = value; }

    //0为怪物回合，1,2,3为玩家回合，每回合结束后重置该回合加成，回合0结束后重置总回合加成，对战结束后重置全局加成
    int round = 1;
    public int Round { get => round; set => round = value; }
    //最大回合数
    int maxround = 3;
    public int MaxRound { get => maxround; set => maxround = value; }

    //clubJ
    bool clubJ = false;
    public bool ClubJ { get => clubJ; set => clubJ = value; }

    //当前选择的卡牌
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
