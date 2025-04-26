using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Messages : MonoBehaviour
{

}
public class Round_Message
{
    //游戏内变量
    public List<string> round_end_action = new List<string>();//结束行动列表
    public List<string> skill_action = new List<string>();//结束行动列表

    public List<string> bank_in_instances = new List<string>();//牌库
    public List<string> bank_out_instances = new List<string>();//弃牌堆
    public List<string> hand_in_instances = new List<string>();//手牌实例
    public List<string> hand_out_instances = new List<string>();//出牌实例
    public List<string> enemy_instances = new List<string>();//敌人实例
    public List<string> equipment_instances = new List<string>();//装备实例

    public List<string> select_action = new List<string>();

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

    //出牌次数
    int round = 0;
    public int Round { get => round; set => round = value; }
    //最大出牌次数
    int maxround = 3;
    public int MaxRound { get => maxround; set => maxround = value; }
    //弃牌次数
    int dropround = 0;
    public int DropRound { get => dropround; set => dropround = value; }
    //最大弃牌次数
    int maxdropround = 3;
    public int MaxDropRound { get => maxdropround; set => maxdropround = value; }

    //当前选择怪物
    Enemy enemy_now = null;
    public Enemy Enemy_Now { get => enemy_now; set => enemy_now = value; }
    //当前选择卡牌
    string card_now = null;
    public string Card_Now { get => card_now; set => card_now = value; }
    //玩家
    Player player = null;
    public Player Player { get => player; set => player = value; }

    //是否阻塞
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
