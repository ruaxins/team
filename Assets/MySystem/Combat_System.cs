using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Combat_System : MonoBehaviour
{
    GameObject hand_in_list;
    GameObject equipement_list;
    GameObject enemy_list;
    GameObject enemy_select;
    Text gameround;
    Text card_combination;
    Text card_attack;
    Text card_defend;

    #region 初始化组件
    List<Vector2> potision = new List<Vector2>
    {
        new Vector2(0, 0),
        new Vector2(150, 0),
        new Vector2(-150, 0),
        new Vector2(300, 0),
        new Vector2(-300, 0),
        new Vector2(450, 0),
        new Vector2(-450, 0),
        new Vector2(600, 0),
        new Vector2(-600, 0),
        new Vector2(750, 0),
        new Vector2(-750, 0),
        new Vector2(900, 0),
        new Vector2(-900, 0),
    };
    List<Vector2> potision_enemy = new List<Vector2>
    {
        new Vector2(0, 0),
        new Vector2(450, 0),
        new Vector2(-450, 0),
        new Vector2(900, 0),
        new Vector2(-900, 0),
    };
    Skills skill = new Skills();
    Manager manager = new Manager();

    #endregion
    private void Start()
    {
        Game_Init();

        //默认指定第一个怪物
        Round_Message.RMsg.Enemy_Now = manager.Get_Enemy_Data(Round_Message.RMsg.enemy_instances[0]);
    }
    //初始化游戏内容
    private void Game_Init()
    {
        hand_in_list = GameObject.Find("hand_in_list");
        equipement_list = GameObject.Find("equipement_list");
        enemy_list = GameObject.Find("enemy_list");
        enemy_select = GameObject.Find("Select_Enemy");
        gameround = GameObject.Find("gameround").GetComponent<Text>();
        card_combination = GameObject.Find("combination").GetComponent<Text>();
        card_attack = GameObject.Find("attack").GetComponent<Text>();
        card_defend = GameObject.Find("defend").GetComponent<Text>();


        #region 卡牌，敌人入库
        foreach (string card in Message.Msg.bank_in_instances)
        {
            Round_Message.RMsg.bank_in_instances.Add(card);
        }
        foreach (string card in Message.Msg.equipement_instance)
        {
            Round_Message.RMsg.equipment_instances.Add(card);
        }
        foreach (string enemy in Message.Msg.enemy_in_instances)
        {
            Round_Message.RMsg.enemy_instances.Add(enemy);
        }

        //玩家
        Round_Message.RMsg.Player = new Player(100);
        #endregion
    }
    public void Flash_pos()
    {
        foreach (string type in Round_Message.RMsg.hand_in_instances)
        {
            GameObject instance = manager.Get_Card_Instances(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(hand_in_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Round_Message.RMsg.hand_in_instances.IndexOf(type);
            rt.anchoredPosition = potision[pos]; 

            instance.transform.localScale = new Vector2(1, 1);
            instance.GetComponent<CardHoverEffect>().originalPosition = instance.transform.position;
            instance.GetComponent<CardHoverEffect>().originalScale = new Vector2(1, 1);
            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = manager.Get_Card_Data(type).type + '\n' + manager.Get_Card_Data(type).point_show;
            }
        }
        foreach (string type in Round_Message.RMsg.equipment_instances)
        {
            GameObject instance = manager.Get_Card_Instances(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(equipement_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Round_Message.RMsg.equipment_instances.IndexOf(type);
            rt.anchoredPosition = potision[pos];

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = manager.Get_Card_Data(type).type + '\n' + manager.Get_Card_Data(type).point_show;
            }
        }
        foreach (string type in Round_Message.RMsg.enemy_instances)
        {
            GameObject instance = manager.Get_Enemy_Instances(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(enemy_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Round_Message.RMsg.enemy_instances.IndexOf(type);
            rt.anchoredPosition = potision_enemy[pos];

            Transform name = instance.transform.Find("Name");
            Transform health = instance.transform.Find("Health");
            Transform attack = instance.transform.Find("Attack");
            Transform defend = instance.transform.Find("Defend");
            if (name != null)
            {
                GameObject childObject = name.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = "名称：" + type;
            }
            if (health != null)
            {
                GameObject childObject = health.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = "血量：" + manager.Get_Enemy_Data(type).enemy_health.ToString();
            }
            if (attack != null)
            {
                GameObject childObject = attack.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = "攻击值：" + manager.Get_Enemy_Data(type).enemy_attack_point.ToString();
            }
            if (defend != null)
            {
                GameObject childObject = defend.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = "护甲值：" + manager.Get_Enemy_Data(type).enemy_armor_point.ToString();
            }
        }
    }
    private void Update()
    {
        foreach (var type in Message.Msg.data_enemy)
        {
            if (type.Value == Round_Message.RMsg.Enemy_Now)
            {
                string enemy = type.Key;
                enemy_select.transform.position = manager.Get_Enemy_Instances(enemy).transform.position;
                break;
            }
        }
        
        //计算牌型组合和玩家数值并显示
        if (Round_Message.RMsg.hand_in_instances.Count > 0)
        {
            manager.Get_card_combination(Round_Message.RMsg.Player);
            manager.Use_card(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
        card_combination.text = "当前牌型倍率：" + Round_Message.RMsg.Player.scale.ToString();
        card_attack.text = "玩家攻击值：" + Round_Message.RMsg.Player.player_attack_point.ToString();
        card_defend.text = "玩家护甲值：" + Round_Message.RMsg.Player.player_armor_point_origin.ToString();

        gameround.text = Round_Message.RMsg.Round.ToString();
        // 判断当前怪物是否死亡
        if (Round_Message.RMsg.enemy_instances.Count > 0 && manager.Death_enemy(Round_Message.RMsg.Enemy_Now))
        {
            Round_Message.RMsg.Enemy_Now = manager.Get_Enemy_Data(Round_Message.RMsg.enemy_instances[0]);
            Flash_pos();
        }
        //判断游戏是否结束
        if (Round_Message.RMsg.enemy_instances.Count <= 0)
        {
            Debug.Log("Win");
            //清空战斗数据
            manager.Data_clear_combat();
            //退出战斗
            GameObject.Find("Manager").GetComponent<Btn_Controller>().Combat_Exit();
        }
        if (Round_Message.RMsg.Player.player_health <= 0)
        {
            Debug.Log("Loss");
            //清空战斗数据
            manager.Data_clear_combat();
            //退出战斗
            GameObject.Find("Manager").GetComponent<Btn_Controller>().Combat_Exit();
        }
    }
    #region 外部调用方法
    //Fight按钮
    public void Select_Open()
    {
        foreach (string skills in Round_Message.RMsg.skill_action)
        {
            if (skills == "heart8" || skills == "heart10" || skills == "heartJ" || skills == "heartA" || skills == "clubA")
                Round_Message.RMsg.select_action.Add(skills);
        }
        if (Round_Message.RMsg.select_action.Count > 0)
            GameObject.Find("RoundManager").GetComponent<Extra_Select_Manager>().StartSelectTurn();
        else
            Next_round();
    }
    //更新回合
    public void Next_round()
    {
        //使用卡牌
        foreach (string skills in Round_Message.RMsg.skill_action)
        {
            skill.Get_skills(skills, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
        //对当前敌人造成伤害
        manager.Get_hurt_ennemy(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        //弃牌
        manager.Drop_card();
        //减少出牌次数
        Round_Message.RMsg.Round ++;
        Flash_pos();
        if (Round_Message.RMsg.Round >= Round_Message.RMsg.MaxRound) GameObject.Find("RoundManager").GetComponent<TurnManager>().EndPlayerTurn();
        else GameObject.Find("RoundManager").GetComponent<TurnManager>().StartNewTurn();
    }
    //弃牌_Drop
    public void Drop()
    {
        if (Round_Message.RMsg.DropRound >= Round_Message.RMsg.MaxDropRound) return;
        manager.Drop_card();
        Round_Message.RMsg.DropRound++;
        //从牌库补充卡牌至上限
        if (Round_Message.RMsg.hand_in_instances.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_instances.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                if (Round_Message.RMsg.hand_in_instances.Count <= 0) break;
                manager.Get_card();
            }
        Flash_pos();
    }
    #endregion
}
