using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Combat_System : MonoBehaviour
{
    GameObject hand_in_list;
    GameObject equipement_list;
    GameObject enemy_list;
    Text gameround;
    Text detial;

    #region 初始化组件
    List<Vector2> potision = new List<Vector2>
    {
        new Vector2(-600, 0),
        new Vector2(-400, 0),
        new Vector2(-200, 0),
        new Vector2(0, 0),
        new Vector2(200, 0),
        new Vector2(400, 0),
        new Vector2(600, 0),
        new Vector2(800, 0),
        new Vector2(1000, 0),
        new Vector2(1200, 0),
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
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
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
        if (manager.Search_equipment("club3")) clubEvent3.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club4")) clubEvent4.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("club5")) clubEvent5.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("clubA")) clubEventA.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        foreach (Enemy e in Round_Message.RMsg.enemy_fight)
        {
            if (manager.Search_equipment("curse4")) curseEvent4.Invoke(Round_Message.RMsg.Player, e);
            if (manager.Search_equipment("curse5")) curseEvent5.Invoke(Round_Message.RMsg.Player, e);
        }
        Flash_pos();
    }
    //初始化游戏内容
    private void Game_Init()
    {
        hand_in_list = GameObject.Find("hand_in_list");
        equipement_list = GameObject.Find("equipement_list");
        enemy_list = GameObject.Find("enemy_list");
        gameround = GameObject.Find("gameround").GetComponent<Text>();
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
        Round_Message.RMsg.bank_in_cards.Add(d7);
        Round_Message.RMsg.bank_in_cards.Add(d8);
        Round_Message.RMsg.bank_in_cards.Add(d9);
        Round_Message.RMsg.bank_in_cards.Add(d10);
        Round_Message.RMsg.bank_in_cards.Add(dJ);
        //Round_Message.RMsg.bank_in_cards.Add(dQ);
        //Round_Message.RMsg.bank_in_cards.Add(dK);
        //Round_Message.RMsg.bank_in_cards.Add(dA);
        //防御卡
        Round_Message.RMsg.bank_in_cards.Add(sJ);
        Round_Message.RMsg.bank_in_cards.Add(sQ);
        Round_Message.RMsg.bank_in_cards.Add(sK);
        Round_Message.RMsg.bank_in_cards.Add(sA);
        ////装备卡
        //Round_Message.RMsg.equipment_bar.Add(c3);
        //Round_Message.RMsg.equipment_bar.Add(c4);
        //Round_Message.RMsg.equipment_bar.Add(c5);
        //Round_Message.RMsg.equipment_bar.Add(c6);
        //Round_Message.RMsg.equipment_bar.Add(c7);
        //Round_Message.RMsg.equipment_bar.Add(c8);
        //Round_Message.RMsg.equipment_bar.Add(c9);
        //Round_Message.RMsg.equipment_bar.Add(c10);
        //Round_Message.RMsg.equipment_bar.Add(cJ);
        //Round_Message.RMsg.equipment_bar.Add(cQ);
        //Round_Message.RMsg.equipment_bar.Add(cK);
        //Round_Message.RMsg.equipment_bar.Add(cA);
        ////诅咒卡
        //Round_Message.RMsg.equipment_bar.Insert(0, z1);
        //Round_Message.RMsg.equipment_bar.Insert(0, z2);
        //Round_Message.RMsg.equipment_bar.Insert(0, z3);
        //Round_Message.RMsg.equipment_bar.Insert(0, z4);
        //Round_Message.RMsg.equipment_bar.Insert(0, z5);
        //技能卡
        //Round_Message.RMsg.bank_in_cards.Add(h3);
        //Round_Message.RMsg.bank_in_cards.Add(h4);
        //Round_Message.RMsg.bank_in_cards.Add(h5);
        //Round_Message.RMsg.bank_in_cards.Add(h6);
        //Round_Message.RMsg.bank_in_cards.Add(h7);
        //Round_Message.RMsg.bank_in_cards.Add(h8);
        //Round_Message.RMsg.bank_in_cards.Add(h9);
        //Round_Message.RMsg.bank_in_cards.Add(h10);
        //Round_Message.RMsg.bank_in_cards.Add(hJ);
        //Round_Message.RMsg.bank_in_cards.Add(hQ);
        //Round_Message.RMsg.bank_in_cards.Add(hK);
        //Round_Message.RMsg.bank_in_cards.Add(hA);
        //敌人入库
        Round_Message.RMsg.enemy_fight.Add(e1);
        Round_Message.RMsg.enemy_fight.Add(e2);
        Round_Message.RMsg.enemy_fight.Add(e3);
        //玩家
        Round_Message.RMsg.Player = player;
        #endregion
    }
    public void Flash_pos()
    {
        for (int i = 0; i < Round_Message.RMsg.hand_in_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.hand_in_instances[i];
            // 设置父对象
            instance.transform.SetParent(hand_in_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            rt.anchoredPosition = potision[i]; // 向右100像素，向下50像素
            //Debug.Log("pos"+instance.transform.position);  
            instance.transform.localScale = new Vector2(1, 1);
            instance.GetComponent<CardHoverEffect>().originalPosition = instance.transform.position;
            instance.GetComponent<CardHoverEffect>().originalScale = new Vector2(1, 1);
            //Debug.Log("update" + instance.GetComponent<CardHoverEffect>().originalPosition + "," + instance.GetComponent<CardHoverEffect>().originalScale);
            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.hand_in_card_list[i].type + '\n' + Round_Message.RMsg.hand_in_card_list[i].point_show;
            }
        }
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
        gameround.text = Round_Message.RMsg.Round.ToString();
        //Flash_pos();
        detial.text = "玩家血量:" + Round_Message.RMsg.Player.player_health.ToString() + '\n' +
                      //"玩家攻击值:" + player.player_attack_point.ToString() + '\n' +
                      //"玩家技能攻击值:" + player.player_skill_point.ToString() + '\n' +
                      "玩家护甲值:" + Round_Message.RMsg.Player.player_armor_point_origin.ToString() + '\n' +
                      "当前敌人血量:" + Round_Message.RMsg.Enemy_Now.enemy_health.ToString() + '\n' +
                      "当前敌人攻击值:" + Round_Message.RMsg.Enemy_Now.enemy_attack_point.ToString() + '\n' +
                      "当前敌人护甲值:" + Round_Message.RMsg.Enemy_Now.enemy_armor_point.ToString() + '\n' +
                      //"玩家受到伤害:" + player.player_get_hurt.ToString() + '\n' +
                      "当前敌人受到伤害:" + Round_Message.RMsg.Enemy_Now.enemy_get_hurt.ToString();

        //检测怪物是否死亡
        if (Round_Message.RMsg.enemy_fight.Count <= 0) Debug.Log("game win");

        //检测玩家是否死亡
        manager.Death_player(Round_Message.RMsg.Player);

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
        Use_card(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("curse3")) curseEvent3.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        //判断玩家回合是否结束
        if (Round_Message.RMsg.Round > Round_Message.RMsg.MaxRound)
        {
            //触发回合结束效果
            if (Round_Message.RMsg.round_end_action.Count > 0)
            {
                foreach (string action in Round_Message.RMsg.round_end_action)
                {
                    skill.Get_skills(action, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
                }
            }
            //进入怪物回合
            Debug.Log("enemy_round");
            Enemy_attack();
            //回合结束清空加成
            manager.Data_clear_enemy(Round_Message.RMsg.Enemy_Now);
            manager.Data_clear_card();
            //重置回合数
            Round_Message.RMsg.Round = 1;
            //判断是否调用club
            if (manager.Search_equipment("club3")) clubEvent3.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club4")) clubEvent4.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club5")) clubEvent5.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("club8")) clubEvent8.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubJ")) clubEventJ.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubK")) clubEventK.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            if (manager.Search_equipment("clubA")) clubEventA.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            foreach (Enemy e in Round_Message.RMsg.enemy_fight)
            {
                if (manager.Search_equipment("curse4")) curseEvent4.Invoke(Round_Message.RMsg.Player, e);
                if (manager.Search_equipment("curse5")) curseEvent5.Invoke(Round_Message.RMsg.Player, e);
            }
        }
        //回合结束清空加成
        manager.Data_clear_player(Round_Message.RMsg.Player, false);
        //从牌库补充卡牌至上限
        if (Round_Message.RMsg.hand_in_card_list.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_card_list.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Round_Message.RMsg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            }

        Flash_pos();
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
            Round_Message.RMsg.Player.player_attack_point++;
        else if (Round_Message.RMsg.hand_out_card_list[n].type == "spade")
            Round_Message.RMsg.Player.player_armor_point++;
        else
            Debug.Log("该卡牌无法被强化");

    }
    //从手牌选择卡牌的外部调用
    public void HeartA_plus(int n)
    {
        if (Round_Message.RMsg.hand_out_card_list[n].type == "diamond")
        {
            Round_Message.RMsg.Player.player_attack_point -= Round_Message.RMsg.hand_out_card_list[n].point;
            Round_Message.RMsg.Player.player_armor_point += Round_Message.RMsg.hand_out_card_list[n].point;
        }
        else if (Round_Message.RMsg.hand_out_card_list[n].type == "spade")
        {
            Round_Message.RMsg.Player.player_attack_point += Round_Message.RMsg.hand_out_card_list[n].point;
            Round_Message.RMsg.Player.player_armor_point -= Round_Message.RMsg.hand_out_card_list[n].point;
        }
        else
            Debug.Log("该卡牌无法被转换");
    }
    //从手牌选择卡牌的外部调用
    public void ClubA_plus(int n)
    {
        Round_Message.RMsg.bank_in_cards.Add(Round_Message.RMsg.hand_in_card_list[n]);
        manager.Get_card();
    }
    public void Enemy_attack()
    {
        foreach (Enemy e in Round_Message.RMsg.enemy_fight)
        {
            e.Attack_mode_change(Round_Message.RMsg.Player, e);
            if (manager.Search_equipment("club6")) clubEvent6.Invoke(Round_Message.RMsg.Player, e);
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
                if (Round_Message.RMsg.bank_in_cards.Count <= 0) break;
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            }
        Flash_pos();
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
