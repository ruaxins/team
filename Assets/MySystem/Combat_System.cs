using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Combat_System : MonoBehaviour
{
    GameObject hand_in_list;
    GameObject hand_out_list;
    GameObject equipement_list;
    GameObject enemy_list;
    Text gameround;
    Text enemy_now;
    Text detial;

    #region 初始化组件
    List<Vector2> potision = new List<Vector2>
    {
        new Vector2(-600, 0),
        new Vector2(-500, 0),
        new Vector2(-400, 0),
        new Vector2(-300, 0),
        new Vector2(-200, 0),
        new Vector2(-100, 0),
        new Vector2(0, 0),
        new Vector2(100, 0),
        new Vector2(200, 0),
        new Vector2(300, 0),
        new Vector2(400, 0),
        new Vector2(500, 0),
        new Vector2(600, 0),
        new Vector2(700, 0),
        new Vector2(800, 0),
        new Vector2(900, 0),
    };
    List<Vector2> potisions = new List<Vector2>
    {
        new Vector2(0, 300),
        new Vector2(0, 200),
        new Vector2(0, 100),
        new Vector2(0, 0),
        new Vector2(0, -100),
        new Vector2(0, -200),
        new Vector2(0, -300),
        new Vector2(0, -400),
        new Vector2(0, -500),
        new Vector2(0, -600),
        new Vector2(0, -700),
        new Vector2(0, -800),
        new Vector2(0, -900),
    };
    List<Vector2> potision_e = new List<Vector2>
    {
        new Vector2(-500, 0),
        new Vector2(-300, 0),
        new Vector2(-100, 0),
        new Vector2(100, 0),
        new Vector2(300, 0),
    };

    UnityEvent<Player, Enemy> clubEvent3 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent4 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent5 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent6 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent7 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent8 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent9 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEvent10 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEventJ = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEventQ = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEventK = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> clubEventA = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> curseEvent1 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> curseEvent2 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> curseEvent3 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> curseEvent4 = new UnityEvent<Player, Enemy>();
    UnityEvent<Player, Enemy> curseEvent5 = new UnityEvent<Player, Enemy>();



    Skills skill = new Skills();
    Player player = new Player(100);
    Manager manager = new Manager();
    #endregion
    private void Start()
    {
        Game_Init();
        //发牌
        if (Round_Message.RMsg.hand_in_card_list.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_card_list.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, Round_Message.RMsg.Enemy_Now);
            }
        //装备实例
        for (int i = 0; i < Round_Message.RMsg.equipment_bar.Count; i++)
        {
            manager.LoadCard(i, "equip");
        }
        for (int i = 0; i < Round_Message.RMsg.enemy_fight.Count; i++)
        {
            manager.LoadEnemy(i, "enemy");
        }

        //默认指定第一个怪物
        Round_Message.RMsg.Enemy_Now = Round_Message.RMsg.enemy_fight[0];

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
        if (manager.Search_equipment("club3")) clubEvent3.Invoke(player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club4")) clubEvent4.Invoke(player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club5")) clubEvent5.Invoke(player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("clubA")) clubEventA.Invoke(player, Round_Message.RMsg.Enemy_Now);
        foreach (Enemy e in Round_Message.RMsg.enemy_fight)
        {
            if (manager.Search_equipment("curse4")) curseEvent4.Invoke(player, e);
            if (manager.Search_equipment("curse5")) curseEvent5.Invoke(player, e);
        }
    }
    //初始化游戏内容
    private void Game_Init()
    {
        hand_in_list = GameObject.Find("hand_in_list");
        hand_out_list = GameObject.Find("hand_out_list");
        equipement_list = GameObject.Find("equipement_list");
        enemy_list = GameObject.Find("enemy_list");
        gameround = GameObject.Find("gameround").GetComponent<Text>();
        enemy_now = GameObject.Find("enemy_now").GetComponent<Text>();
        detial = GameObject.Find("detial").GetComponent<Text>();
        for (int i = 0; i < 10; i++)
        {
            Replenish();
            Replenish_equipement();
            Replenish_enemy();
        }
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
        ////装备卡
        //Message.Msg.equipment_bar.Add(c3);
        //Message.Msg.equipment_bar.Add(c4);
        //Message.Msg.equipment_bar.Add(c5);
        //Message.Msg.equipment_bar.Add(c6);
        //Message.Msg.equipment_bar.Add(c7);
        //Message.Msg.equipment_bar.Add(c8);
        //Message.Msg.equipment_bar.Add(c9);
        //Message.Msg.equipment_bar.Add(c10);
        //Message.Msg.equipment_bar.Add(cJ);
        //Message.Msg.equipment_bar.Add(cQ);
        //Message.Msg.equipment_bar.Add(cK);
        //Message.Msg.equipment_bar.Add(cA);
        ////诅咒卡
        //Message.Msg.equipment_bar.Insert(0, z1);
        //Message.Msg.equipment_bar.Insert(0, z2);
        //Message.Msg.equipment_bar.Insert(0, z3);
        //Message.Msg.equipment_bar.Insert(0, z4);
        //Message.Msg.equipment_bar.Insert(0, z5);
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
        Round_Message.RMsg.enemy_fight.Add(e1);
        Round_Message.RMsg.enemy_fight.Add(e2);
        Round_Message.RMsg.enemy_fight.Add(e3);
        #endregion
    }
    void Replenish()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/card");
        GameObject newObj = Instantiate(prefab, transform);
        Button btn = newObj.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            manager.OnCardClick(newObj);
        });
        newObj.SetActive(true);
        Round_Message.RMsg.pool.Enqueue(newObj);
    }
    void Replenish_equipement()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/card");
        GameObject newObj = Instantiate(prefab, transform);
        Button btn = newObj.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            
        });
        newObj.SetActive(true);
        Round_Message.RMsg.equipement_pool.Enqueue(newObj);
    }
    void Replenish_enemy()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/enemy");
        GameObject newObj = Instantiate(prefab, transform);
        Button btn = newObj.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            manager.OnEnemyClick(newObj);
        });
        newObj.SetActive(true);
        Round_Message.RMsg.enemy_pool.Enqueue(newObj);
    }
    private void Update()
    {
        //Debug.Log("cardInstances:" + Round_Message.RMsg.pool.Count);
        //if (Message.Msg.pool.Count == 0) Replenish();
        gameround.text = Round_Message.RMsg.Round.ToString();
        //创建手牌实例
        //Debug.Log("in:" + Round_Message.RMsg.hand_in_card_list.Count + ":" + Round_Message.RMsg.hand_in_instances.Count);
        //Debug.Log("out:" + Round_Message.RMsg.hand_out_card_list.Count + ":" + Round_Message.RMsg.hand_out_instances.Count);
        //Debug.Log("e:" + Round_Message.RMsg.enemy_fight.Count + ":" + Round_Message.RMsg.enemy_instances.Count);
        for (int i = 0; i < Round_Message.RMsg.hand_in_card_list.Count; i++)
        {

            GameObject instance = Round_Message.RMsg.hand_in_instances[i];
            // 设置父对象
            instance.transform.SetParent(hand_in_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();

            // 设置位置（中心点坐标）
            rt.anchoredPosition = potision[i]; // 向右100像素，向下50像素

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.hand_in_card_list[i].type + '\n' + Round_Message.RMsg.hand_in_card_list[i].point_show;
            }
        }
        //创建出牌列表实例
        for (int i = 0; i < Round_Message.RMsg.hand_out_card_list.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.hand_out_instances[i];
            // 设置父对象
            instance.transform.SetParent(hand_out_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();

            // 设置位置（中心点坐标）
            rt.anchoredPosition = potision[i]; // 向右100像素，向下50像素


            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.hand_out_card_list[i].type + '\n' + Round_Message.RMsg.hand_out_card_list[i].point_show;
            }
        }
        //创建装备列表实例
        for (int i = 0; i < Round_Message.RMsg.equipment_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.equipment_instances[i];
            // 设置父对象
            instance.transform.SetParent(equipement_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();

            // 设置位置（中心点坐标）
            rt.anchoredPosition = potisions[i]; // 向右100像素，向下50像素

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.equipment_bar[i].type + '\n' + Round_Message.RMsg.equipment_bar[i].point_show;
            }
        }
        //创建敌人列表实例
        for (int i = 0; i < Round_Message.RMsg.enemy_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.enemy_instances[i];
            // 设置父对象
            instance.transform.SetParent(enemy_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();

            // 设置位置（中心点坐标）
            rt.anchoredPosition = potision_e[i]; // 向右100像素，向下50像素

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.enemy_fight[i].enemy_health.ToString();
            }
        }

        detial.text = "玩家血量:" + player.player_health.ToString() + '\n' +
                      //"玩家攻击值:" + player.player_attack_point.ToString() + '\n' +
                      //"玩家技能攻击值:" + player.player_skill_point.ToString() + '\n' +
                      "玩家护甲值:" + player.player_armor_point_origin.ToString() + '\n' +
                      "当前敌人血量:" + Round_Message.RMsg.Enemy_Now.enemy_health.ToString() + '\n' +
                      "当前敌人攻击值:" + Round_Message.RMsg.Enemy_Now.enemy_attack_point.ToString() + '\n' +
                      "当前敌人护甲值:" + Round_Message.RMsg.Enemy_Now.enemy_armor_point.ToString() + '\n' +
                      //"玩家受到伤害:" + player.player_get_hurt.ToString() + '\n' +
                      "当前敌人受到伤害:" + Round_Message.RMsg.Enemy_Now.enemy_get_hurt.ToString();

        //检测怪物是否死亡
        if (Round_Message.RMsg.enemy_fight.Count <= 0) Debug.Log("game win");

        //检测玩家是否死亡
        manager.Death_player(player);

        //判断是否击杀怪物
        if (Round_Message.RMsg.enemy_fight.Count > 0 && manager.Death_enemy(Round_Message.RMsg.Enemy_Now)) Round_Message.RMsg.Enemy_Now = Round_Message.RMsg.enemy_fight[0];

    }
    #region 外部调用方法
    public string Show_enemy(int n)
    {
        if (Round_Message.RMsg.enemy_fight.Count <= n) return null;
        return Round_Message.RMsg.enemy_fight[n].enemy_health.ToString();
    }

    //更新回合_Fight按钮
    public void Next_round()
    {
        //增加回合数
        Round_Message.RMsg.Round += 1;
        //执行卡牌效果，计算牌型倍率，清空选中卡牌
        Use_card(player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("curse3")) curseEvent3.Invoke(player, Round_Message.RMsg.Enemy_Now);
        //回合结束清空加成
        manager.Data_clear_player(player, false);
        //从牌库补充卡牌至上限
        if (Round_Message.RMsg.hand_in_card_list.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_card_list.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Message.Msg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, Round_Message.RMsg.Enemy_Now);
            }
        //判断玩家回合是否结束
        if (Round_Message.RMsg.Round > Round_Message.RMsg.MaxRound)
        {
            //触发回合结束效果
            if (Round_Message.RMsg.round_end_action.Count > 0)
            {
                foreach (string action in Round_Message.RMsg.round_end_action)
                {
                    skill.Get_skills(action, player, Round_Message.RMsg.Enemy_Now);
                }
            }
            //进入怪物回合
            Debug.Log("enemy_round");
            Enemy_attack();
            //回合结束清空加成
            manager.Data_clear_player(player, true);
            manager.Data_clear_enemy(Round_Message.RMsg.Enemy_Now);
            manager.Data_clear_card();
            //重置回合数
            Round_Message.RMsg.Round = 1;
            //判断是否调用club
            if (manager.Search_equipment("club3")) clubEvent3.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club4")) clubEvent4.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club5")) clubEvent5.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club8")) clubEvent8.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubK")) clubEventK.Invoke(player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubA")) clubEventA.Invoke(player, Round_Message.RMsg.Enemy_Now);
            foreach (Enemy e in Round_Message.RMsg.enemy_fight)
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
        if (Round_Message.RMsg.hand_out_card_list[n].type == "diamond")
            player.player_attack_point++;
        else if (Round_Message.RMsg.hand_out_card_list[n].type == "spade")
            player.player_armor_point++;
        else
            Debug.Log("该卡牌无法被强化");

    }
    //从手牌选择卡牌的外部调用
    public void HeartA_plus(int n)
    {
        if (Round_Message.RMsg.hand_out_card_list[n].type == "diamond")
        {
            player.player_attack_point -= Round_Message.RMsg.hand_out_card_list[n].point;
            player.player_armor_point += Round_Message.RMsg.hand_out_card_list[n].point;
        }
        else if (Round_Message.RMsg.hand_out_card_list[n].type == "spade")
        {
            player.player_attack_point += Round_Message.RMsg.hand_out_card_list[n].point;
            player.player_armor_point -= Round_Message.RMsg.hand_out_card_list[n].point;
        }
        else
            Debug.Log("该卡牌无法被转换");
    }
    //从手牌选择卡牌的外部调用
    public void ClubA_plus(int n)
    {
        Message.Msg.bank_in_cards.Add(Round_Message.RMsg.hand_in_card_list[n]);
        manager.Get_card();
    }
    public void Enemy_attack()
    {
        foreach (Enemy e in Round_Message.RMsg.enemy_fight)
        {
            e.Attack_mode_change(player, e);
            if (manager.Search_equipment("club6")) clubEvent6.Invoke(player, e);
        }
    }
    //弃牌_Drop
    public void Drop()
    {
        if (Round_Message.RMsg.DropRound >= Round_Message.RMsg.MaxDropRound) return;
        Drop_card();
        Round_Message.RMsg.DropRound++;
        //从牌库补充卡牌至上限
        if (Round_Message.RMsg.hand_in_card_list.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_card_list.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Message.Msg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(player, Round_Message.RMsg.Enemy_Now);
            }
    }
    public void Drop_card()
    {
        for (int i = 0; i < Round_Message.RMsg.hand_out_card_list.Count;i++)
        {
            Round_Message.RMsg.hand_out_card_list[i].isused = true;
            Round_Message.RMsg.hand_out_card_list[i].isshowed = false;
            Round_Message.RMsg.bank_out_cards.Add(Round_Message.RMsg.hand_out_card_list[i]);
            manager.ReturnObject(Round_Message.RMsg.hand_out_instances[i]);
        }
        Round_Message.RMsg.hand_out_instances.Clear();
        Round_Message.RMsg.hand_out_card_list.Clear();
        Round_Message.RMsg.Hand_out_card_num = 0;

    }
    //打出手牌
    public void Use_card(Player player, Enemy enemy)
    {
        manager.Get_card_combination(player);
        foreach (Card card in Round_Message.RMsg.hand_out_card_list)
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
        Round_Message.RMsg.round_end_action.Add("heart6_plus");
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
        Round_Message.RMsg.MaxRound += 1;
        //将回合结束行为添加到列表
        Round_Message.RMsg.round_end_action.Add("club5_plus");
    }
    public void Club5_plus(Player player, Enemy enemy)
    {
        Round_Message.RMsg.MaxRound = 3;
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
        foreach (Card card in Round_Message.RMsg.equipment_bar)
        {
            if (card.type == "curse") curse++;
        }
        if ((player.player_health + 5 * curse) > player.player_health_max) player.player_health = player.player_health_max;
        else player.player_health += (5 * curse);
    }
    public void Club9(Player player, Enemy enemy)
    {
        if (Round_Message.RMsg.hand_out_card_list.Count == 4) player.scale *= 2;
    }
    public void Club10(Player player, Enemy enemy)
    {
        int r = random.Next(0,10);
        if (r == 0) manager.Get_card();
    }
    public void ClubJ(Player player, Enemy enemy)
    {
        Round_Message.RMsg.ClubJ = true;
    }
    public void ClubQ(Player player, Enemy enemy)
    {
        if (Round_Message.RMsg.hand_out_card_list.Count <= 3) player.scale *= 3;
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
        Round_Message.RMsg.enemy_fight.Add(enemy);
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
        int r = random.Next(0,Round_Message.RMsg.hand_in_card_list.Count);
        Round_Message.RMsg.hand_in_card_list[r].isused = true;
        Round_Message.RMsg.hand_in_card_list[r].isshowed = false;
        Round_Message.RMsg.bank_out_cards.Add(Round_Message.RMsg.hand_in_card_list[r]);
        Round_Message.RMsg.hand_in_card_list.Remove(Round_Message.RMsg.hand_in_card_list[r]);
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
        foreach (Card card in Round_Message.RMsg.hand_out_card_list)
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

        if (Round_Message.RMsg.ClubJ) temp_straight++;

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
                Round_Message.RMsg.hand_in_card_list.Add(Message.Msg.bank_in_cards[r]);
                Message.Msg.bank_in_cards.Remove(Message.Msg.bank_in_cards[r]);
            }
            else
            {
                //指定卡牌
                Round_Message.RMsg.hand_in_card_list[n].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_in_card_list[n]);
                Message.Msg.bank_in_cards.Remove(Round_Message.RMsg.hand_in_card_list[n]);
            }
        }
        else
        {
            //从弃牌堆
            if (n == -1)
            {
                //随机卡牌
                int r = random.Next(0, Round_Message.RMsg.bank_out_cards.Count);
                Round_Message.RMsg.bank_out_cards[r].isused = false;
                Message.Msg.bank_in_cards[r].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.bank_out_cards[r]);
                Round_Message.RMsg.bank_out_cards.Remove(Round_Message.RMsg.bank_out_cards[r]);
            }
            else
            {
                //指定卡牌
                Round_Message.RMsg.hand_in_card_list[n].isused = false;
                Round_Message.RMsg.hand_in_card_list[n].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_in_card_list[n]);
                Round_Message.RMsg.bank_out_cards.Remove(Round_Message.RMsg.hand_in_card_list[n]);
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
        Round_Message.RMsg.round_end_action.Clear();
        Round_Message.RMsg.DropRound = 0;
    }
    public bool Death_enemy(Enemy enemy)
    {
        if (enemy.enemy_health <= 0)
        {
            int pos = Round_Message.RMsg.enemy_fight.IndexOf(enemy);
            ReturnEnemy(Round_Message.RMsg.enemy_instances[pos]);
            Round_Message.RMsg.enemy_fight.Remove(enemy);
            Round_Message.RMsg.enemy_instances.Remove(Round_Message.RMsg.enemy_instances[pos]);
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
        foreach (Card card in Round_Message.RMsg.equipment_bar)
        {
            if(name == card.type + card.point_show) return true;
        }
        return false;
    }
    // 从池中获取对象
    public GameObject GetObject()
    {
        GameObject obj = Round_Message.RMsg.pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    // 将对象返回池中
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        Round_Message.RMsg.pool.Enqueue(obj);
    }
    // 从池中获取对象
    public GameObject GetEnemy()
    {
        GameObject obj = Round_Message.RMsg.enemy_pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    // 将对象返回池中
    public void ReturnEnemy(GameObject obj)
    {
        obj.SetActive(false);
        Round_Message.RMsg.enemy_pool.Enqueue(obj);
    }
    public void LoadCard(int n, string type)
    {
        // 不需要文件扩展名，路径相对于Resources文件夹
        GameObject prefab = GetObject();
        if (prefab == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        if (type == "card")
        {
            // 实例化到场景中
            Round_Message.RMsg.hand_in_instances.Add(prefab);
        }
        else if (type == "equip")
        {
            // 实例化到场景中
            Round_Message.RMsg.equipment_instances.Add(prefab);
        }
        else
        {
            Debug.Log("can not found this prefab type");
        }
    }
    public void LoadEnemy(int n, string type)
    {
        // 不需要文件扩展名，路径相对于Resources文件夹
        GameObject prefab = GetEnemy();
        if (prefab == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        if (type == "enemy")
        {
            // 实例化到场景中
            Round_Message.RMsg.enemy_instances.Add(prefab);
        }
        else
        {
            Debug.Log("can not found this prefab type");
        }
    }
    //单击卡牌
    public void OnCardClick(GameObject gameObject)
    {
        if (Round_Message.RMsg.hand_out_instances.Contains(gameObject))
        {
            Debug.Log("contains in out");
            Round_Message.RMsg.Hand_out_card_num--;
            int pos = Round_Message.RMsg.hand_out_instances.IndexOf(gameObject);
            UnSelect_card(pos);
            return;
        }
        if (Round_Message.RMsg.hand_in_instances.Contains(gameObject))
        {
            Debug.Log("contains in in");
            if (Round_Message.RMsg.Hand_out_card_num >= Round_Message.RMsg.Hand_out_card_num_max) return;
            Round_Message.RMsg.Hand_out_card_num++;
            int pos = Round_Message.RMsg.hand_in_instances.IndexOf(gameObject);
            Select_card(pos);
            return;
        }
        Debug.Log("can not found card");
    }
    public void OnEnemyClick(GameObject gameObject)
    {   
        if (Round_Message.RMsg.enemy_instances.Contains(gameObject))
        {
            Debug.Log("enemy change");
            int pos = Round_Message.RMsg.enemy_instances.IndexOf(gameObject);
            Round_Message.RMsg.Enemy_Now = Round_Message.RMsg.enemy_fight[pos];
        }
        else
        {
            Debug.Log("can not found enemy");
        }
        
    }
    //选中手牌
    public void Select_card(int n)
    {
        if (Round_Message.RMsg.hand_in_card_list[n].type == "diamond" || Round_Message.RMsg.hand_in_card_list[n].type == "spade")
        {
            Round_Message.RMsg.hand_out_card_list.Insert(0, Round_Message.RMsg.hand_in_card_list[n]);
            Round_Message.RMsg.hand_out_instances.Insert(0, Round_Message.RMsg.hand_in_instances[n]);
        }
        else
        {
            Round_Message.RMsg.hand_out_card_list.Add(Round_Message.RMsg.hand_in_card_list[n]);
            Round_Message.RMsg.hand_out_instances.Add(Round_Message.RMsg.hand_in_instances[n]);
        }
        Round_Message.RMsg.hand_in_card_list.Remove(Round_Message.RMsg.hand_in_card_list[n]);
        Round_Message.RMsg.hand_in_instances.Remove(Round_Message.RMsg.hand_in_instances[n]);
    }
    //取消选择手牌
    public void UnSelect_card(int n)
    {
        Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_out_card_list[n]);
        Round_Message.RMsg.hand_in_instances.Add(Round_Message.RMsg.hand_out_instances[n]);
        Round_Message.RMsg.hand_out_card_list.Remove(Round_Message.RMsg.hand_out_card_list[n]);
        Round_Message.RMsg.hand_out_instances.Remove(Round_Message.RMsg.hand_out_instances[n]);
    }
}