using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Combat_System : MonoBehaviour
{
    #region 初始化组件
    public UnityEvent<Player, Enemy> clubEvent3;
    public UnityEvent<Player, Enemy> clubEvent4;
    public UnityEvent<Player, Enemy> clubEvent5;
    public UnityEvent<Player, Enemy> clubEvent6;
    public UnityEvent<Player, Enemy> clubEvent7;
    public UnityEvent<Player, Enemy> clubEvent8;
    public UnityEvent<Player, Enemy> clubEvent9;
    public UnityEvent<Player, Enemy> clubEvent10;
    public UnityEvent<Player, Enemy> clubEventJ;
    public UnityEvent<Player, Enemy> clubEventQ;
    public UnityEvent<Player, Enemy> clubEventK;
    public UnityEvent<Player, Enemy> clubEventA;
    public UnityEvent<Player, Enemy> curseEvent1;
    public UnityEvent<Player, Enemy> curseEvent2;
    public UnityEvent<Player, Enemy> curseEvent3;
    public UnityEvent<Player, Enemy> curseEvent4;
    public UnityEvent<Player, Enemy> curseEvent5;

    public Text equipment_bar;
    public Text hand_out_card_1;
    public Text hand_out_card_2;
    public Text hand_out_card_3;
    public Text hand_out_card_4;
    public Text hand_out_card_5;
    public Text hand_in_card_1;
    public Text hand_in_card_2;
    public Text hand_in_card_3;
    public Text hand_in_card_4;
    public Text hand_in_card_5;
    public Text hand_in_card_6;
    public Text hand_in_card_7;
    public Text hand_in_card_8;
    public Text gameround;
    public Text enemy1;
    public Text enemy2;
    public Text enemy3;
    public Text enemy_now;
    public Text detial;

    Skills skill = new Skills();
    Player player = new Player(100);
    Enemy enemy = new Enemy();
    Manager manager = new Manager();
    #endregion
    private void Start()
    {
        Game_Init();
        //发牌
        if (Message.Msg.hand_in_card_list.Count < Message.Msg.Hand_in_card_num_max)
            for (int i = Message.Msg.hand_in_card_list.Count; i < Message.Msg.Hand_in_card_num_max; i++)
            {
                manager.Get_card();
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, enemy);
                //创建实例
            }

        //默认指定第一个怪物
        enemy = Message.Msg.enemy_fight[0];

        clubEvent3.AddListener(skill.Club3);
        clubEvent4.AddListener(skill.Club4);
        clubEvent5.AddListener(skill.Club5);
        clubEvent6.AddListener(skill.Club6);
        clubEvent7.AddListener(skill.Club7);
        clubEvent8.AddListener(skill.Club8);
        clubEvent9.AddListener(skill.Club9);
        clubEvent10.AddListener(skill.Club10);
        clubEventJ.AddListener(skill.ClubJ);
        clubEventQ.AddListener(skill.ClubQ);
        clubEventK.AddListener(skill.ClubK);
        clubEventA.AddListener(skill.ClubA);
        curseEvent1.AddListener(skill.Curse1);
        curseEvent2.AddListener(skill.Curse2);
        curseEvent3.AddListener(skill.Curse3);
        curseEvent4.AddListener(skill.Curse4);
        curseEvent5.AddListener(skill.Curse5);
        //开局调用club
        if (manager.Search_equipment("club3")) clubEvent3.Invoke(player, enemy);
        if (manager.Search_equipment("club4")) clubEvent4.Invoke(player, enemy);
        if (manager.Search_equipment("club5")) clubEvent5.Invoke(player, enemy);
        if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(player, enemy);
        if (manager.Search_equipment("clubA")) clubEventA.Invoke(player, enemy);
        foreach (Enemy e in Message.Msg.enemy_fight)
        {
            if (manager.Search_equipment("curse4")) curseEvent4.Invoke(player, e);
            if (manager.Search_equipment("curse5")) curseEvent5.Invoke(player, e);
        }
    }
    //初始化游戏内容
    private void Game_Init()
    {
        #region 创建卡牌实例
        
        Card d3 = new Card("diamond", "3", 3, "null", 3);
        Card d4 = new Card("diamond", "4", 4, "null", 4);
        Card d5 = new Card("diamond", "5", 5, "null", 5);
        Card d6 = new Card("diamond", "6", 6, "null", 6);
        Card d7 = new Card("diamond", "7", 7, "null", 7);
        Card d8 = new Card("diamond", "8", 8, "null", 8);
        Card d9 = new Card("diamond", "9", 9, "null", 9);
        Card d10 = new Card("diamond", "10", 10, "null", 10);
        Card dJ = new Card("diamond", "J", 10, "null", 11);
        Card dQ = new Card("diamond", "Q", 10, "null", 12);
        Card dK = new Card("diamond", "K", 10, "null", 13);
        Card dA = new Card("diamond", "A", 10, "null", 14);

        Card c3 = new Card("club", "3", 3, "null", 3);
        Card c4 = new Card("club", "4", 4, "null", 4);
        Card c5 = new Card("club", "5", 5, "null", 5);
        Card c6 = new Card("club", "6", 6, "null", 6);
        Card c7 = new Card("club", "7", 7, "null", 7);
        Card c8 = new Card("club", "8", 8, "null", 8);
        Card c9 = new Card("club", "9", 9, "null", 9);
        Card c10 = new Card("club", "10", 10, "null", 10);
        Card cJ = new Card("club", "J", 10, "null", 11);
        Card cQ = new Card("club", "Q", 10, "null", 12);
        Card cK = new Card("club", "K", 10, "null", 13);
        Card cA = new Card("club", "A", 10, "null", 14);

        Card h3 = new Card("heart", "3", 3, "null", 3);
        Card h4 = new Card("heart", "4", 4, "null", 4);
        Card h5 = new Card("heart", "5", 5, "null", 5);
        Card h6 = new Card("heart", "6", 6, "null", 6);
        Card h7 = new Card("heart", "7", 7, "null", 7);
        Card h8 = new Card("heart", "8", 8, "null", 8);
        Card h9 = new Card("heart", "9", 9, "null", 9);
        Card h10 = new Card("heart", "10", 10, "null", 10);
        Card hJ = new Card("heart", "J", 10, "null", 11);
        Card hQ = new Card("heart", "Q", 10, "null", 12);
        Card hK = new Card("heart", "K", 10, "null", 13);
        Card hA = new Card("heart", "A", 10, "null", 14);

        Card s3 = new Card("spade", "3", 3, "null", 3);
        Card s4 = new Card("spade", "4", 4, "null", 4);
        Card s5 = new Card("spade", "5", 5, "null", 5);
        Card s6 = new Card("spade", "6", 6, "null", 6);
        Card s7 = new Card("spade", "7", 7, "null", 7);
        Card s8 = new Card("spade", "8", 8, "null", 8);
        Card s9 = new Card("spade", "9", 9, "null", 9);
        Card s10 = new Card("spade", "10", 10, "null", 10);
        Card sJ = new Card("spade", "J", 10, "null", 11);
        Card sQ = new Card("spade", "Q", 10, "null", 12);
        Card sK = new Card("spade", "K", 10, "null", 13);
        Card sA = new Card("spade", "A", 10, "null", 14);

        Card z1 = new Card("curse", "1", 0, "null", 0);
        Card z2 = new Card("curse", "2", 0, "null", 0);
        Card z3 = new Card("curse", "3", 0, "null", 0);
        Card z4 = new Card("curse", "4", 0, "null", 0);
        Card z5 = new Card("curse", "5", 0, "null", 0);
        #endregion
        #region 创建敌人
        //敌人攻击模式
        //0-造成攻击值伤害，1-两层增强，2-护甲+5，3-复制自身，4-获得扣血量的护甲，5-玩家一层自刃
        //6-失去一张手牌，7-护甲+15，8-玩家两层自刃，9-五层增强，10-四层抵挡
        List<int> mode = new List<int>
        {
            0,0,1
        };
        //初始化敌人（血量，攻击值，护甲，攻击模式）
        Enemy e1 = new Enemy(30, 10, 5, mode);
        Enemy e2 = new Enemy(50, 20, 5, mode);
        Enemy e3 = new Enemy(40, 10, 5, mode);
        Enemy fire_ghost = new Enemy();
        Enemy fire_insect = new Enemy();
        Enemy fire_slime = new Enemy();
        Enemy fire_puppet = new Enemy();
        Enemy fire_specter = new Enemy();
        Enemy fire_dog = new Enemy();
        Enemy fire_knight = new Enemy();
        Enemy fire_monster = new Enemy();
        Enemy fire_witch = new Enemy();
        Enemy fire_king = new Enemy();
        #endregion
        #region 卡牌，敌人入库
        //卡牌入库
        //攻击卡
        Message.Msg.bank_in_cards.Add(d7);
        Message.Msg.bank_in_cards.Add(d8);
        Message.Msg.bank_in_cards.Add(d9);
        Message.Msg.bank_in_cards.Add(d10);
        Message.Msg.bank_in_cards.Add(dJ);
        Message.Msg.bank_in_cards.Add(dQ);
        Message.Msg.bank_in_cards.Add(dK);
        Message.Msg.bank_in_cards.Add(dA);
        //防御卡
        Message.Msg.bank_in_cards.Add(sJ);
        Message.Msg.bank_in_cards.Add(sQ);
        Message.Msg.bank_in_cards.Add(sK);
        Message.Msg.bank_in_cards.Add(sA);
        //装备卡
        Message.Msg.equipment_bar.Add(c3);
        Message.Msg.equipment_bar.Add(c4);
        Message.Msg.equipment_bar.Add(c5);
        Message.Msg.equipment_bar.Add(c6);
        Message.Msg.equipment_bar.Add(c7);
        Message.Msg.equipment_bar.Add(c8);
        Message.Msg.equipment_bar.Add(c9);
        Message.Msg.equipment_bar.Add(c10);
        Message.Msg.equipment_bar.Add(cJ);
        Message.Msg.equipment_bar.Add(cQ);
        Message.Msg.equipment_bar.Add(cK);
        Message.Msg.equipment_bar.Add(cA);
        //诅咒卡
        Message.Msg.equipment_bar.Add(z1);
        Message.Msg.equipment_bar.Add(z2);
        Message.Msg.equipment_bar.Add(z3);
        Message.Msg.equipment_bar.Add(z4);
        Message.Msg.equipment_bar.Add(z5);
        //技能卡
        Message.Msg.bank_in_cards.Add(h3);
        Message.Msg.bank_in_cards.Add(h4);
        Message.Msg.bank_in_cards.Add(h5);
        Message.Msg.bank_in_cards.Add(h6);
        Message.Msg.bank_in_cards.Add(h7);
        Message.Msg.bank_in_cards.Add(h8);
        Message.Msg.bank_in_cards.Add(h9);
        Message.Msg.bank_in_cards.Add(h10);
        Message.Msg.bank_in_cards.Add(hJ);
        Message.Msg.bank_in_cards.Add(hQ);
        Message.Msg.bank_in_cards.Add(hK);
        Message.Msg.bank_in_cards.Add(hA);
        //敌人入库
        Message.Msg.enemy_fight.Add(e1);
        Message.Msg.enemy_fight.Add(e2);
        Message.Msg.enemy_fight.Add(e3);
        #endregion
    }
    private void Update()
    {
        gameround.text = Message.Msg.Round.ToString();

        hand_in_card_1.text = Show_card_in(0);
        hand_in_card_2.text = Show_card_in(1);
        hand_in_card_3.text = Show_card_in(2);
        hand_in_card_4.text = Show_card_in(3);
        hand_in_card_5.text = Show_card_in(4);
        hand_in_card_6.text = Show_card_in(5);
        hand_in_card_7.text = Show_card_in(6);
        hand_in_card_8.text = Show_card_in(7);

        hand_out_card_1.text = Show_card_out(0);
        hand_out_card_2.text = Show_card_out(1);
        hand_out_card_3.text = Show_card_out(2);
        hand_out_card_4.text = Show_card_out(3);
        hand_out_card_5.text = Show_card_out(4);

        equipment_bar.text = Show_equipment(0);

        enemy1.text = Show_enemy(0);
        enemy2.text = Show_enemy(1);
        enemy3.text = Show_enemy(2);

        detial.text = "玩家血量:" + player.player_health.ToString() + '\n' +
                      //"玩家攻击值:" + player.player_attack_point.ToString() + '\n' +
                      //"玩家技能攻击值:" + player.player_skill_point.ToString() + '\n' +
                      "玩家护甲值:" + player.player_armor_point_origin.ToString() + '\n' +
                      "当前敌人血量:" + enemy.enemy_health.ToString() + '\n' +
                      "当前敌人攻击值:" + enemy.enemy_attack_point.ToString() + '\n' +
                      "当前敌人护甲值:" + enemy.enemy_armor_point.ToString() + '\n' +
                      //"玩家受到伤害:" + player.player_get_hurt.ToString() + '\n' +
                      "当前敌人受到伤害:" + enemy.enemy_get_hurt.ToString();

        //检测怪物是否死亡
        if (Message.Msg.enemy_fight.Count <= 0) Debug.Log("game win");

        //检测玩家是否死亡
        manager.Death_player(player);

        //判断是否击杀怪物
        if (manager.Death_enemy(enemy) && Message.Msg.enemy_fight.Count > 0) enemy = Message.Msg.enemy_fight[0];

    }
    #region 外部调用方法
    public string Show_equipment(int n)
    {
        return Message.Msg.equipment_bar[n].type + Message.Msg.equipment_bar[n].point_show;
    }
    public string Show_enemy(int n)
    {
        if (Message.Msg.enemy_fight.Count <= n) return null;
        return Message.Msg.enemy_fight[n].enemy_health.ToString();
    }
    public string Show_card_in(int n)
    {
        if (Message.Msg.hand_in_card_list.Count <= n) return null;
        return Message.Msg.hand_in_card_list[n].type + Message.Msg.hand_in_card_list[n].point_show;
    }
    public string Show_card_out(int n)
    {
        if (Message.Msg.hand_out_card_list.Count <= n) return null;
        return Message.Msg.hand_out_card_list[n].type + Message.Msg.hand_out_card_list[n].point_show;
    }
    public void Select_enemy(int n)
    {
        if (Message.Msg.enemy_fight.Count <= n) return;
        enemy = Message.Msg.enemy_fight[n];
    }
    //更新回合_Fight按钮
    public void Next_round()
    {
        //增加回合数
        Message.Msg.Round += 1;
        //执行卡牌效果，计算牌型倍率，清空选中卡牌
        Use_card(player, enemy);
        if (manager.Search_equipment("curse3")) curseEvent3.Invoke(player, enemy);
        //回合结束清空加成
        manager.Data_clear_player(player,false);
        //从牌库补充卡牌至上限
        if (Message.Msg.hand_in_card_list.Count < Message.Msg.Hand_in_card_num_max)
            for (int i = Message.Msg.hand_in_card_list.Count; i < Message.Msg.Hand_in_card_num_max; i++)
            {
                if (Message.Msg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, enemy);
                //创建实例
            }
        //判断玩家回合是否结束
        if (Message.Msg.Round > Message.Msg.MaxRound)
        {
            //触发回合结束效果
            if (Message.Msg.round_end_action.Count > 0)
            {
                foreach (string action in Message.Msg.round_end_action)
                {
                    skill.Get_skills(action,player,enemy);
                }
            }
            //进入怪物回合
            Debug.Log("enemy_round");
            Enemy_attack();
            //回合结束清空加成
            manager.Data_clear_player(player, true);
            manager.Data_clear_enemy(enemy);
            manager.Data_clear_card();
            //重置回合数
            Message.Msg.Round = 1;
            //重置弃牌次数
            Message.Msg.Drop = Message.Msg.MaxDrop;
            //判断是否调用club
            if (manager.Search_equipment("club3")) clubEvent3.Invoke(player, enemy);
            if (manager.Search_equipment("club4")) clubEvent4.Invoke(player, enemy);
            if (manager.Search_equipment("club5")) clubEvent5.Invoke(player, enemy);
            if (manager.Search_equipment("club8")) clubEvent8.Invoke(player, enemy);
            if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(player, enemy);
            if (manager.Search_equipment("clubK")) clubEventK.Invoke(player, enemy);
            if (manager.Search_equipment("clubA")) clubEventA.Invoke(player, enemy);
            foreach (Enemy e in Message.Msg.enemy_fight)
            {
                if (manager.Search_equipment("curse4")) curseEvent4.Invoke(player, e);
                if (manager.Search_equipment("curse5")) curseEvent5.Invoke(player, e);
            }
        }
    }
    //从牌库选择卡牌的外部调用
    public void Heart8_plus(int n)
    {
        manager.Get_card(n, true);
    }
    //从弃牌堆选择卡牌的外部调用
    public void HeartJ_plus(int n)
    {
        manager.Get_card(n, false);
    }
    //从手牌选择卡牌的外部调用
    public void Heart10_plus(int n)
    {
        if (Message.Msg.hand_out_card_list[n].type == "diamond")
            player.player_attack_point++;  
        else if (Message.Msg.hand_out_card_list[n].type == "spade")
            player.player_armor_point++;
        else
            Debug.Log("该卡牌无法被强化");

    }
    //从手牌选择卡牌的外部调用
    public void HeartA_plus(int n)
    {
        if (Message.Msg.hand_out_card_list[n].type == "diamond")
        {
            player.player_attack_point -= Message.Msg.hand_out_card_list[n].point;
            player.player_armor_point += Message.Msg.hand_out_card_list[n].point;
        }
        else if (Message.Msg.hand_out_card_list[n].type == "spade")
        {
            player.player_attack_point += Message.Msg.hand_out_card_list[n].point;
            player.player_armor_point -= Message.Msg.hand_out_card_list[n].point;
        }
        else
            Debug.Log("该卡牌无法被转换");
    }
    //从手牌选择卡牌的外部调用
    public void ClubA_plus(int n)
    {
        Message.Msg.bank_in_cards.Add(Message.Msg.hand_in_card_list[n]);
        manager.Get_card();
    }
    public void Enemy_attack()
    {
        foreach (Enemy e in Message.Msg.enemy_fight)
        {
            e.Attack_mode_change(player,e);
            if (manager.Search_equipment("club6")) clubEvent6.Invoke(player, e);
        }
    }
    //单击卡牌加入准备列表，Select_card的外部调用
    public void OnCardClick(int n)
    {
        if(Message.Msg.hand_out_card_list.Count >= Message.Msg.Hand_out_card_num_max) return;
        if(Message.Msg.hand_in_card_list.Count > n)
            Select_card(Message.Msg.hand_in_card_list[n]);
    }
    //选中手牌
    public void Select_card(Card card)
    {
        if(card.type == "diamond" || card.type == "spade") 
            Message.Msg.hand_out_card_list.Insert(0, card);
        else 
            Message.Msg.hand_out_card_list.Add(card);
        Message.Msg.hand_in_card_list.Remove(card);
        //将手牌实例移至待出牌区
    }
    //单击卡牌退出准备列表，UnSelect_card的外部调用
    public void OnUnCardClick(int n)
    {
        if (Message.Msg.hand_out_card_list.Count > n)
            UnSelect_card(Message.Msg.hand_out_card_list[n]);
    }
    //取消选择手牌
    public void UnSelect_card(Card card)
    {
        Message.Msg.hand_in_card_list.Add(card);
        Message.Msg.hand_out_card_list.Remove(card);
    }
    //弃牌_Drop
    public void Drop_card()
    {
        if (Message.Msg.Drop <= 0) return;
        foreach (Card c in Message.Msg.hand_out_card_list)
        {
            c.isused = true;
            c.isshowed = false;
            Message.Msg.bank_out_cards.Add(c);
        }
        Message.Msg.hand_out_card_list.Clear();
        //从牌库补充卡牌至上限
        if (Message.Msg.hand_in_card_list.Count < Message.Msg.Hand_in_card_num_max)
            for (int i = Message.Msg.hand_in_card_list.Count; i < Message.Msg.Hand_in_card_num_max; i++)
            {
                if (Message.Msg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, enemy);
                //创建实例
            }
        Message.Msg.Drop--;
    }
    //打出手牌
    public void Use_card(Player player, Enemy enemy)
    {
        manager.Get_card_combination(player);
        foreach (Card card in Message.Msg.hand_out_card_list)
        {
            switch (card.type)
            {
                case "diamond": 
                    if (manager.Search_equipment("curse1")) player.player_attack_point += 0;
                    else if(manager.Search_equipment("curse2")) player.player_attack_point += (card.point/2);
                    else player.player_attack_point += card.point;break;
                case "spade":
                    if (manager.Search_equipment("curse1")) player.player_armor_point += 0;
                    else if (manager.Search_equipment("curse2")) player.player_armor_point += (card.point / 2);
                    else player.player_armor_point += card.point;break;
                case "heart":skill.Get_skills(card.type + card.point_show, player, enemy);break;
                default:
                    break;
            }
        }
        if (manager.Search_equipment("club7")) clubEvent7.Invoke(player, enemy);
        if (manager.Search_equipment("club9")) clubEvent9.Invoke(player, enemy);
        if (manager.Search_equipment("clubQ")) clubEventQ.Invoke(player, enemy);
        Drop_card();
        manager.Get_hurt_ennemy(player, enemy);
    }
    #endregion
}
//卡牌系统
public class Card
{
    //卡牌属性
    public string type;//diamond-方块，spade-黑桃，club-梅花，heart-红桃
    public string point_show;//展示的点数3-10,JQKA
    public float point;//计算的点数
    public string skill_message;//卡牌效果信息
    public int weight;//计算牌型的权重

    public bool isshowed = false;//进入战斗后是否被展示
    public bool isused = false;//是否被使用

    //初始化卡牌信息
    public Card() { }
    public Card(string type, string point_show, float point, string skill_message, int weight)
    {
        this.type = type;
        this.point_show = point_show;
        this.point = point;
        this.skill_message = skill_message;
        this.weight = weight;
    }
}
//技能系统
public class Skills
{
    Manager manager = new Manager();
    //初始化随机数
    System.Random random = new System.Random();
    private readonly Dictionary<string, Action<Player, Enemy>> skills;
    public Skills()
    {
        skills = new Dictionary<string, Action<Player, Enemy>>
        {
            ["heart3"] = Heart3,
            ["heart4"] = Heart4, 
            ["heart5"] = Heart5,
            ["heart6"] = Heart6,
            ["heart7"] = Heart7,
            ["heart8"] = Heart8,
            ["heart9"] = Heart9,
            ["heart10"] = Heart10,
            ["heartJ"] = HeartJ,
            ["heartQ"] = HeartQ,
            ["heartK"] = HeartK,
            ["heartA"] = HeartA,

            ["heart6_plus"] = Heart6_plus,
            ["club5_plus"] = Club5_plus
        };
    }
    public void Get_skills(string skill, Player player = null, Enemy enemy = null)
    {
        if (skills.TryGetValue(skill, out var action))
            action(player,enemy);
        else
            Debug.Log("找不到该技能");
    }
    #region 卡牌效果
    //技能卡
    void Heart3(Player player, Enemy enemy)
    {
        //玩家伤害点数翻倍
        player.player_attack_point *= 2;
    }
    void Heart4(Player player, Enemy enemy)
    {
        //无视护甲造成伤害
        enemy.enemy_have_armor = false;
    }
    void Heart5(Player player, Enemy enemy)
    {
        //玩家护甲值翻倍
        player.player_armor_point *= 2;
    }
    void Heart6(Player player, Enemy enemy)
    {
        //将回合结束行为添加到列表
        Message.Msg.round_end_action.Add("heart6_plus");
    }
    void Heart6_plus(Player player, Enemy enemy)
    {
        //玩家回合结束时对敌人造成当前护甲值的伤害
        if (enemy.enemy_have_armor)
        {
            enemy.enemy_get_hurt = player.player_armor_point_origin - enemy.enemy_armor_point;
            if (enemy.enemy_get_hurt > 0)
            {
                enemy.enemy_health -= (int)enemy.enemy_get_hurt;
                enemy.enemy_armor_point = 0;
            }
            else
            {
                enemy.enemy_armor_point = -enemy.enemy_get_hurt;
                enemy.enemy_get_hurt = 0;
            }
        }
        else
        {
            enemy.enemy_get_hurt = player.player_armor_point_origin;
            enemy.enemy_health -= (int)enemy.enemy_get_hurt;
        }
        enemy.enemy_have_armor = true;
    }
    void Heart7(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之五，本回合提升百分之五的攻击值
        player.player_health -= (int)(player.player_health * 0.05f);
        player.player_enhance++;
    }
    void Heart8(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之十
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
        //打开选择UI
    }
    void Heart9(Player player, Enemy enemy)
    {
        //回复造成伤害50%的生命值
        float player_skill_point = player.player_skill_point + player.player_attack_point * 0.05f * player.player_enhance;
        float player_attack_point = player.player_attack_point * player.scale;
        player_skill_point *= player.scale;
        int health = (int)(player_attack_point + player_skill_point)/2;
        if (player.player_health + health >= player.player_health_max)
            player.player_health = player.player_health_max;
        else
            player.player_health += health;
    }
    void Heart10(Player player, Enemy enemy)
    {
        //选择一张卡牌并使其点数加一
        //打开选择UI
    }
    void HeartJ(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之十
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
        //打开选择UI
    }
    void HeartQ(Player player, Enemy enemy)
    {
        //对敌人造成15点伤害，对自身造成5点伤害
        enemy.enemy_health -= 15;
        player.player_health -= 5;
    }
    void HeartK(Player player, Enemy enemy)
    {
        //生成5点护甲值
        player.player_armor_point += 5;
    }
    void HeartA(Player player, Enemy enemy)
    {
        //将方块转换成黑桃，或者黑桃转换成方块
        //打开选择UI
    }
    //梅花卡
    public void Club3(Player player, Enemy enemy)
    {
        player.player_enhance += 4;
    }
    public void Club4(Player player, Enemy enemy)
    {
        player.player_hold += 4;
    }
    public void Club5(Player player, Enemy enemy)
    {
        //增加一回合
        Message.Msg.MaxRound += 1;
        //将回合结束行为添加到列表
        Message.Msg.round_end_action.Add("club5_plus");
    }
    public void Club5_plus(Player player, Enemy enemy)
    {
        Message.Msg.MaxRound = 3;
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
    }
    public void Club6(Player player, Enemy enemy)
    {
        int r = random.Next(0,2);
        if (r == 0) return;
        enemy.enemy_health -= (int)player.player_get_hurt;
    }
    public void Club7(Player player, Enemy enemy)
    {
        if (player.scale == 6) player.player_armor_point *= 2;
    }
    public void Club8(Player player, Enemy enemy)
    {
        int curse = 0;
        foreach (Card card in Message.Msg.equipment_bar)
        {
            if (card.type == "curse") curse++;
        }
        if ((player.player_health + 5 * curse) > player.player_health_max) player.player_health = player.player_health_max;
        else player.player_health += (5 * curse);
    }
    public void Club9(Player player, Enemy enemy)
    {
        if (Message.Msg.hand_out_card_list.Count == 4) player.scale *= 2;
    }
    public void Club10(Player player, Enemy enemy)
    {
        int r = random.Next(0,10);
        if (r == 0) manager.Get_card();
    }
    public void ClubJ(Player player, Enemy enemy)
    {
        Message.Msg.ClubJ = true;
    }
    public void ClubQ(Player player, Enemy enemy)
    {
        if (Message.Msg.hand_out_card_list.Count <= 3) player.scale *= 3;
    }
    public void ClubK(Player player, Enemy enemy)
    {
        if (player.player_health + (int)(player.player_health_max * 0.2f) > player.player_health_max) player.player_health = player.player_health_max;
        else player.player_health += (int)(player.player_health_max * 0.2f);
    }
    public void ClubA(Player player, Enemy enemy)
    {
        //打开选择UI
    }
    //诅咒卡
    public void Curse1(Player player, Enemy enemy)
    {
        //player.player_attack_point = 0;
    }
    public void Curse2(Player player, Enemy enemy)
    {
        //player.player_attack_point /= 2;
    }
    public void Curse3(Player player, Enemy enemy)
    {
        player.player_health -= (int)(player.player_health * 0.05f);
    }
    public void Curse4(Player player, Enemy enemy)
    {
        enemy.enemy_armor_point += enemy.enemy_armor_point * 0.05f * 2;
    }
    public void Curse5(Player player, Enemy enemy)
    {
        enemy.enemy_attack_point += enemy.enemy_attack_point * 0.05f * 2;
    }
    #endregion
}
//怪物系统
public class Enemy
{
    Manager manager = new Manager();
    System.Random random = new System.Random();
    //怪物的属性
    public int enemy_health;
    public float enemy_attack_point_origin = 0;
    public float enemy_attack_point = 0;
    public float enemy_armor_point_origin = 0;
    public float enemy_armor_point = 0;
    public float enemy_get_hurt = 0;
    public bool enemy_have_armor = true;

    public int attack_mode = 0;
    public List<int> attack_mode_list;

    public Enemy() { }
    public Enemy(int enemy_health, float enemy_attack_point, float enemy_armor_point,List<int> attack_mode_list)
    {
        this.enemy_health = enemy_health;
        this.enemy_attack_point = enemy_attack_point;
        this.enemy_attack_point_origin = enemy_attack_point;
        this.enemy_armor_point = enemy_armor_point;
        this.enemy_armor_point_origin = enemy_armor_point;
        this.attack_mode_list = attack_mode_list;
    }
    #region 攻击模式
    public void Attack_mode_change(Player player, Enemy enemy)
    {
        switch (attack_mode_list[attack_mode])
        {
            case 0: Attack_mode_0(player, enemy); break;
            case 1: Attack_mode_1(player, enemy); break;
            case 2: Attack_mode_2(player, enemy); break;
            case 3: Attack_mode_3(player, enemy); break;
            case 4: Attack_mode_4(player, enemy); break;
            case 5: Attack_mode_5(player, enemy); break;
            case 6: Attack_mode_6(player, enemy); break;
            case 7: Attack_mode_7(player, enemy); break;
            case 8: Attack_mode_8(player, enemy); break;
            case 9: Attack_mode_9(player, enemy); break;
            case 10: Attack_mode_10(player, enemy); break;
            default:
                break;
        }
        attack_mode++;
        if (attack_mode >= attack_mode_list.Count) attack_mode = 0;
    }
    public void Attack_mode_0(Player player, Enemy enemy)
    {
        manager.Get_hurt_player(player, enemy);
    }
    public void Attack_mode_1(Player player, Enemy enemy)
    {
        enemy.enemy_attack_point += enemy.enemy_attack_point * 0.05f * 2;
    }
    public void Attack_mode_2(Player player, Enemy enemy)
    {
        enemy.enemy_armor_point += 5;
    }
    public void Attack_mode_3(Player player, Enemy enemy)
    {
        Message.Msg.enemy_fight.Add(enemy);
    }
    public void Attack_mode_4(Player player, Enemy enemy)
    {
        if(enemy.enemy_get_hurt > 0)
        {
            enemy.enemy_armor_point += enemy.enemy_get_hurt;
            //
        }
            
    }
    public void Attack_mode_5(Player player, Enemy enemy)
    {
        player.player_health -= (int)(player.player_health * 0.05f);
    }
    public void Attack_mode_6(Player player, Enemy enemy)
    {
        int r = random.Next(0,Message.Msg.hand_in_card_list.Count);
        Message.Msg.hand_in_card_list[r].isused = true;
        Message.Msg.hand_in_card_list[r].isshowed = false;
        Message.Msg.bank_out_cards.Add(Message.Msg.hand_in_card_list[r]);
        Message.Msg.hand_in_card_list.Remove(Message.Msg.hand_in_card_list[r]);
    }
    public void Attack_mode_7(Player player, Enemy enemy)
    {
        enemy.enemy_armor_point += 15;
    }
    public void Attack_mode_8(Player player, Enemy enemy)
    {
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
    }
    public void Attack_mode_9(Player player, Enemy enemy)
    {
        enemy.enemy_attack_point += enemy.enemy_attack_point * 0.05f * 5;
    }
    public void Attack_mode_10(Player player, Enemy enemy)
    {
        enemy.enemy_armor_point += enemy.enemy_armor_point * 0.05f * 4;
    }
    #endregion
}
//角色系统
public class Player
{
    //假设角色的属性
    public int player_health;
    public int player_health_max;
    public float player_attack_point = 0;
    public float player_skill_point = 0;
    public float player_armor_point = 0;
    public float player_armor_point_origin = 0;
    public float player_get_hurt = 0;
    public int player_enhance = 0;
    public int player_hold = 0;

    public int scale = 1;
    public Player() { }
    public Player(int player_health)
    {
        this.player_health = player_health;
        player_health_max = player_health;
    }
}
//管理系统
public class Manager
{
    //初始化随机数
    System.Random random = new System.Random();
    public void Get_card_combination(Player player)
    {
        List<int> weights = new List<int>();
        List<string> types = new List<string>();
        //排序
        foreach (Card card in Message.Msg.hand_out_card_list)
        {
            weights.Add(card.weight);
            types.Add(card.type);  
        }
        for (int i = 1; i < weights.Count; i++)
        {
            int current = weights[i];
            string curr = types[i];
            int j = i - 1;
            while (j >= 0 && weights[j] > current) 
            {
                weights[j + 1] = weights[j];
                types[j+1] = types[j];
                j--;
            }
            weights[j + 1] = current;
            types[j + 1] = curr;
        }
        //判断
        int temp = 0;
        string temp_ = null;
        int temp_num = 1;
        int temp_type = 1;
        int temp_scale = 1;
        int temp_straight = 1;
        foreach (int weight in weights)
        {
            //Debug.Log(temp_num+":"+weight);
            if (temp != 0)
            {
                if (weight == temp + 1) temp_straight++;
                if (weight == temp) temp_num++;
                else temp = weight;
            }
            else temp = weight;
        }
        foreach (string type in types)
        {
            if (temp_ != null)
            {
                if (type == temp_) temp_type++;
            }
            else temp_ = type;
        }

        if (Message.Msg.ClubJ) temp_straight++;

        if (temp_num == 1) temp_scale = 1;//单牌
        if (temp_num == 2) temp_scale = 3;//对子
        if (temp_num == 3) temp_scale = 4;//三条
        if (temp_num == 3) temp_scale = 4;//两对
        if (temp_type == 5) temp_scale = 5;//同花
        if (temp_straight == 5) temp_scale = 5;//顺子
        if (temp_num == 4) temp_scale = 6;//葫芦
        if (temp_type == 5 && temp_straight == 5) temp_scale = 8;//同花顺

        player.scale = temp_scale;
    }
    public void Get_card(int n = -1, bool place = true)
    {
        if (Message.Msg.bank_in_cards.Count <= 0) return;
        if (place)
        {
            //从牌库
            if (n == -1)
            {
                //随机卡牌
                int r = random.Next(0, Message.Msg.bank_in_cards.Count);
                Message.Msg.bank_in_cards[r].isshowed = true;
                Message.Msg.hand_in_card_list.Add(Message.Msg.bank_in_cards[r]);
                Message.Msg.bank_in_cards.Remove(Message.Msg.bank_in_cards[r]);
            }
            else
            {
                //指定卡牌
                Message.Msg.hand_in_card_list[n].isshowed = true;
                Message.Msg.hand_in_card_list.Add(Message.Msg.hand_in_card_list[n]);
                Message.Msg.bank_in_cards.Remove(Message.Msg.hand_in_card_list[n]);
            }
        }
        else
        {
            //从弃牌堆
            if (n == -1)
            {
                //随机卡牌
                int r = random.Next(0, Message.Msg.bank_out_cards.Count);
                Message.Msg.bank_out_cards[r].isused = false;
                Message.Msg.bank_in_cards[r].isshowed = true;
                Message.Msg.hand_in_card_list.Add(Message.Msg.bank_out_cards[r]);
                Message.Msg.bank_out_cards.Remove(Message.Msg.bank_out_cards[r]);
            }
            else
            {
                //指定卡牌
                Message.Msg.hand_in_card_list[n].isused = false;
                Message.Msg.hand_in_card_list[n].isshowed = true;
                Message.Msg.hand_in_card_list.Add(Message.Msg.hand_in_card_list[n]);
                Message.Msg.bank_out_cards.Remove(Message.Msg.hand_in_card_list[n]);
            }
        }

    }
    public void Get_hurt_player(Player player, Enemy enemy)
    {
        Debug.Log("玩家护甲：" + player.player_armor_point_origin + "敌人攻击：" + enemy.enemy_attack_point);
        player.player_get_hurt = enemy.enemy_attack_point - player.player_armor_point_origin;
        if(player.player_get_hurt > 0)
        {
            player.player_health -= (int)player.player_get_hurt;
            player.player_armor_point_origin = 0;
            Debug.Log("玩家受到伤害：" + player.player_get_hurt);
        }
        else
        {
            player.player_armor_point_origin = -player.player_get_hurt;
            player.player_get_hurt = 0;
            Debug.Log("玩家受到伤害：" + player.player_get_hurt);
        }
    }
    public void Get_hurt_ennemy(Player player, Enemy enemy)
    {
        //计算增强层数带来的攻击值加成
        player.player_skill_point += player.player_attack_point * 0.05f * player.player_enhance;
        //计算抵挡层数带来的护甲值加成
        player.player_armor_point += (player.player_armor_point * 0.05f * player.player_hold);
        Debug.Log("玩家攻击值:" + player.player_attack_point + "玩家技能攻击值:" + player.player_skill_point + 
                    "玩家护甲："+ player.player_armor_point_origin + "玩家本回合护甲：" + player.player_armor_point + "玩家倍率:" + player.scale);
        //计算牌型带来的结算倍数
        player.player_attack_point *= player.scale;
        player.player_skill_point *= player.scale;
        player.player_armor_point_origin += (player.player_armor_point * player.scale);
        //判断是否穿透护甲
        if (enemy.enemy_have_armor)
        {
            enemy.enemy_get_hurt = (player.player_attack_point + player.player_skill_point) - enemy.enemy_armor_point;
            if (enemy.enemy_get_hurt > 0)
            {
                enemy.enemy_health -= (int)enemy.enemy_get_hurt;
                enemy.enemy_armor_point = 0;
                Debug.Log("怪物受到伤害：" + enemy.enemy_get_hurt);
            }
            else
            {
                enemy.enemy_armor_point = -enemy.enemy_get_hurt;
                enemy.enemy_get_hurt = 0;
                Debug.Log("怪物受到伤害：" + enemy.enemy_get_hurt);
            }
        }
        else
        {
            enemy.enemy_get_hurt = (player.player_attack_point + player.player_skill_point);
            enemy.enemy_health -= (int)enemy.enemy_get_hurt;
            Debug.Log("怪物受到伤害：" + enemy.enemy_get_hurt);
        }
        enemy.enemy_have_armor = true;
    }
    public void Data_clear_player(Player player, bool endround)
    {
        player.player_get_hurt = 0;
        player.player_attack_point = 0;
        player.player_skill_point = 0;
        player.player_armor_point = 0;

        player.scale = 1;
        if (endround)
        {
            player.player_armor_point_origin = 0;
            player.player_enhance = 0;
            player.player_hold = 0;
        }
    }
    public void Data_clear_enemy(Enemy enemy)
    {
        enemy.enemy_get_hurt = 0;
        //enemy.enemy_attack_point = enemy.enemy_attack_point_origin;
        //enemy.enemy_armor_point = enemy.enemy_armor_point_origin;
    }
    public void Data_clear_card()
    {
        Message.Msg.round_end_action.Clear();
    }
    public bool Death_enemy(Enemy enemy)
    {
        if (enemy.enemy_health <= 0)
        {
            Message.Msg.enemy_fight.Remove(enemy);
            Debug.Log("enemy died");
            return true;
        }
        return false;
    }
    public void Death_player(Player player)
    {
        if (player.player_health <= 0)
            Debug.Log("game default");
    }
    public bool Search_equipment(string name)
    {
        foreach (Card card in Message.Msg.equipment_bar)
        {
            if(name == card.type + card.point_show) return true;
        }
        return false;
    }
}