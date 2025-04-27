using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Extra_Select_Manager;
using static UnityEngine.EventSystems.EventTrigger;

public class Basic : MonoBehaviour
{ 
}
//卡牌系统
public class Card
{
    //卡牌属性
    public string type;//diamond-方块，spade-黑桃，club-梅花，heart-红桃
    public string point_show;//展示的点数3-10,JQKA
    public float point;//计算的点数
    public float point_temp = 0;//临时点数
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

            ["club3"] = Club3,
            ["club4"] = Club4,
            ["club5"] = Club5,
            ["club6"] = Club6,
            ["club7"] = Club7,
            ["club8"] = Club8,
            ["club9"] = Club9,
            ["club10"] = Club10,
            ["clubJ"] = ClubJ,
            ["clubQ"] = ClubQ,
            ["clubK"] = ClubK,
            ["clubA"] = ClubA,

            ["curse1"] = Curse1,
            ["curse2"] = Curse2,
            ["curse3"] = Curse3,
            ["curse4"] = Curse4,
            ["curse5"] = Curse5,

            ["heart6_plus"] = Heart6_plus,
            ["heart8_plus"] = Heart8_plus,
            ["heartJ_plus"] = HeartJ_plus,
            ["heart10_plus"] = Heart10_plus,
            ["heartA_plus"] = HeartA_plus,
            ["clubA_plus"] = ClubA_plus,
        };
    }
    public void Get_skills(string skill, Player player = null, Enemy enemy = null)
    {
        if (skills.TryGetValue(skill, out var action))
            action(player, enemy);
        else
            Debug.Log("找不到该技能:"+skill);
    }
    #region 卡牌效果
    //技能卡
    void Heart3(Player player, Enemy enemy)
    {
        //玩家伤害点数翻倍
        player.scale *= 2;
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
    void Heart7(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之五，本回合提升百分之五的攻击值
        player.player_enhance++;
        player.player_hurt++;
    }
    void Heart8(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之十
        player.player_hurt += 2;
    }
    void Heart9(Player player, Enemy enemy)
    {
        //回复造成伤害50%的生命值
        if(player.player_health + (int)(enemy.enemy_get_hurt * 0.5f) <= player.player_health_max)
            player.player_health += (int)(enemy.enemy_get_hurt * 0.5f);
        else
            player.player_health = player.player_health_max;
    }
    void Heart10(Player player, Enemy enemy)
    {
        //选择一张卡牌并使其点数加一
        //打开选择UI
    }
    void HeartJ(Player player, Enemy enemy)
    {
        //扣除自身当前血量值的百分之十
        player.player_hurt += 2;
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
        //回合开始触发
        player.player_enhance += 4;
    }
    public void Club4(Player player, Enemy enemy)
    {
        //回合开始触发
        player.player_hold += 4;
    }
    public void Club5(Player player, Enemy enemy)
    {
        //回合开始触发
        Round_Message.RMsg.MaxRound = 4;
        player.player_hurt += 2;
    }
    public void Club6(Player player, Enemy enemy)
    {
        //受击触发
        int r = random.Next(0, 2);
        if (r == 0) return;
        enemy.enemy_health -= (int)player.player_get_hurt;
    }
    public void Club7(Player player, Enemy enemy)
    {
        //出牌触发
        if (player.scale == 6) player.player_armor_point *= 2;
    }
    public void Club8(Player player, Enemy enemy)
    {
        //回合开始触发
        int curse = 0;
        foreach (string card in Round_Message.RMsg.enemy_instances)
        {
            if (manager.Get_Card_Data(card).type == "curse") curse++;
        }
        if ((player.player_health + 5 * curse) > player.player_health_max) 
            player.player_health = player.player_health_max;
        else 
            player.player_health += (5 * curse);
    }
    public void Club9(Player player, Enemy enemy)
    {
        //出牌触发
        if (Round_Message.RMsg.hand_out_instances.Count == 4) player.scale *= 2;
    }
    public void Club10(Player player, Enemy enemy)
    {
        //抽牌触发
        int r = random.Next(0, 10);
        if (r == 0) manager.Get_card();
    }
    public void ClubJ(Player player, Enemy enemy)
    {
        //出牌触发
        Round_Message.RMsg.ClubJ = true;
    }
    public void ClubQ(Player player, Enemy enemy)
    {
        //出牌触发
        if (Round_Message.RMsg.hand_out_instances.Count <= 3) player.scale *= 3;
    }
    public void ClubK(Player player, Enemy enemy)
    {
        //回合结束触发
        if (player.player_health + (int)(player.player_health_max * 0.2f) > player.player_health_max) 
            player.player_health = player.player_health_max;
        else 
            player.player_health += (int)(player.player_health_max * 0.2f);
    }
    public void ClubA(Player player, Enemy enemy)
    {
        //回合开始触发
    }
    //诅咒卡
    public void Curse1(Player player, Enemy enemy)
    {
        //计算点数触发
        
    }
    public void Curse2(Player player, Enemy enemy)
    {
        //计算点数触发
        player.player_attack_point /= 2;
        player.player_armor_point /= 2;
    }
    public void Curse3(Player player, Enemy enemy)
    {
        //敌人受击触发
        player.player_hurt ++;
    }
    public void Curse4(Player player, Enemy enemy)
    {
        //回合开始触发
        foreach (string type in Round_Message.RMsg.enemy_instances)
        {
            manager.Get_Enemy_Data(type).enemy_hold += 2;
        }
    }
    public void Curse5(Player player, Enemy enemy)
    {
        //回合开始触发
        foreach (string type in Round_Message.RMsg.enemy_instances)
        {
            manager.Get_Enemy_Data(type).enemy_enhance += 2;
        }
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

    //从牌库选择指定卡牌
    public void Heart8_plus(Player player, Enemy enemy)
    {
        //出牌调用
        manager.Get_card(true, Round_Message.RMsg.Card_Now);
    }
    //从弃牌堆选择指定卡牌
    public void HeartJ_plus(Player player, Enemy enemy)
    {
        //出牌调用
        manager.Get_card(false, Round_Message.RMsg.Card_Now);
    }
    //从手牌选择卡牌并强化
    public void Heart10_plus(Player player, Enemy enemy)
    {
        //出牌调用
        string type = manager.Get_Card_Data(Round_Message.RMsg.Card_Now).type;
        if (type == "diamond" || type == "spade")
            manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point_temp++;
        else
            Debug.Log("该卡牌无法被强化");

    }
    //从手牌选择卡牌并转换
    public void HeartA_plus(Player player, Enemy enemy)
    {
        //出牌调用
        string type = manager.Get_Card_Data(Round_Message.RMsg.Card_Now).type;
        if (type == "diamond")
        {
            Round_Message.RMsg.Player.player_attack_point -= (manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point + manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point_temp);
            Round_Message.RMsg.Player.player_armor_point += (manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point + manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point_temp);
        }
        else if (type == "spade")
        {
            Round_Message.RMsg.Player.player_attack_point += (manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point + manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point_temp);
            Round_Message.RMsg.Player.player_armor_point -= (manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point + manager.Get_Card_Data(Round_Message.RMsg.Card_Now).point_temp);
        }
        else
            Debug.Log("该卡牌无法被转换");
    }
    //从手牌选择卡牌复制并加入牌库
    public void ClubA_plus(Player player, Enemy enemy)
    {
        //回合开始调用
        Round_Message.RMsg.bank_in_instances.Add(Round_Message.RMsg.Card_Now);
        manager.Get_card();
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
    public float enemy_attack_point = 0;
    public float enemy_armor_point = 0;
    public float enemy_get_hurt = 0;
    public bool enemy_have_armor = true;

    //增强
    public int enemy_enhance = 0;
    //抵挡
    public int enemy_hold = 0;
    //自刃
    public int enemy_hurt = 0;

    public int attack_mode = 0;
    public List<int> attack_mode_list;

    public Enemy() { }
    public Enemy(int enemy_health, float enemy_attack_point, float enemy_armor_point, List<int> attack_mode_list)
    {
        this.enemy_health = enemy_health;
        this.enemy_attack_point = enemy_attack_point;
        this.enemy_armor_point = enemy_armor_point;
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
        if(!Round_Message.RMsg.enemy_instances.Contains("fire_ghost"))
            Round_Message.RMsg.enemy_instances.Add("fire_ghost");
    }
    public void Attack_mode_4(Player player, Enemy enemy)
    {
        if (enemy.enemy_get_hurt > 0)
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
        int r = random.Next(0, Round_Message.RMsg.hand_in_instances.Count);
        Round_Message.RMsg.bank_out_instances.Add(Round_Message.RMsg.hand_in_instances[r]);
        Round_Message.RMsg.hand_in_instances.Remove(Round_Message.RMsg.hand_in_instances[r]);
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
    //增强
    public int player_enhance = 0;
    //抵挡
    public int player_hold = 0;
    //自刃
    public int player_hurt = 0;

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
    public bool Search_equipment(string name)
    {
        string type = Get_Card_Data(name).type;
        foreach (string card in Round_Message.RMsg.equipment_instances)
        {
            if (type == card) return true;
        }
        return false;
    }
    //单击卡牌
    public void OnCardClick(string type)
    {
        GameObject card = Get_Card_Instances(type);
        if (Round_Message.RMsg.hand_out_instances.Contains(type))
        {
            card.GetComponent<CardHoverEffect>().isInteractable = true;
            Round_Message.RMsg.Hand_out_card_num--;
            UnSelect_card(type);
            return;
        }
        if (Round_Message.RMsg.hand_in_instances.Contains(type))
        {
            if (Round_Message.RMsg.Hand_out_card_num >= Round_Message.RMsg.Hand_out_card_num_max) return;
            card.GetComponent<CardHoverEffect>().isInteractable = false;
            Round_Message.RMsg.Hand_out_card_num++;
            Select_card(type);
            return;
        }
        Debug.Log("can not found card");
    }
    public void OnEnemyClick(string type)
    {
        if (Round_Message.RMsg.enemy_instances.Contains(type))
        {
            Debug.Log("enemy change");
            Round_Message.RMsg.Enemy_Now = Get_Enemy_Data(type);
        }
        else
        {
            Debug.Log("can not found enemy");
        }

    }
    //选中手牌
    public void Select_card(string type)
    {
        if (Get_Card_Data(type).type == "diamond" || Get_Card_Data(type).type == "spade")
        {
            Round_Message.RMsg.hand_out_instances.Insert(0, type);
        }
        else
        {
            Round_Message.RMsg.hand_out_instances.Add(type);
        }
        Round_Message.RMsg.hand_in_instances.Remove(type);
    }
    //取消选择手牌
    public void UnSelect_card(string type)
    {
        Round_Message.RMsg.hand_in_instances.Add(type);
        Round_Message.RMsg.hand_out_instances.Remove(type);
    }
    //获取选择实例
    public GameObject Get_Select_Instance(string type)
    {
        if (Message.Msg.instance_select.TryGetValue(type, out GameObject card))
        {
            return card;
        }
        return null;
    }
    //获取卡牌实例
    public GameObject Get_Card_Instances(string type)
    {
        if (Message.Msg.instance_card.TryGetValue(type, out GameObject card))
        {
            return card;
        }
        return null;
    }
    //获取敌人实例
    public GameObject Get_Enemy_Instances(string type)
    {
        if (Message.Msg.instance_enemy.TryGetValue(type, out GameObject enemy))
        {
            return enemy;
        }
        return null;
    }
    //获取卡牌类
    public Card Get_Card_Data(string type)
    {
        if (Message.Msg.data_card.TryGetValue(type, out Card card))
        {
            return card;
        }
        return null;
    }
    //获取敌人类
    public Enemy Get_Enemy_Data(string type)
    {
        if (Message.Msg.data_enemy.TryGetValue(type, out Enemy enemy))
        {
            return enemy;
        }
        return null;
    }
    //计算出牌列表的数值
    public void Use_card(Player player, Enemy enemy)
    {
        float player_attack_point = 0;
        float player_armor_point = 0;
        foreach (string type in Round_Message.RMsg.hand_out_instances)
        {
            Card card = Get_Card_Data(type);
            switch (card.type)
            {
                case "diamond": player_attack_point += (card.point + card.point_temp); break;
                case "spade": player_armor_point += (card.point + card.point_temp); break;
                case "heart":if (!Round_Message.RMsg.skill_action.Contains(type)) Round_Message.RMsg.skill_action.Add(type);break;
                default:
                    break;
            }
        }
        player.player_attack_point = player_attack_point;
        player.player_armor_point = player_armor_point;
    }
    //获取卡牌组合
    public void Get_card_combination(Player player)
    {
        List<int> weights = new List<int>();
        List<string> types = new List<string>();
        //排序
        foreach (string type in Round_Message.RMsg.hand_out_instances)
        {
            Card card = Get_Card_Data(type);
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
                types[j + 1] = types[j];
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
    //计算敌人受伤
    public void Get_hurt_ennemy(Player player, Enemy enemy)
    {
        //计算增强层数带来的攻击值加成
        player.player_skill_point += player.player_attack_point * 0.05f * player.player_enhance;
        //计算抵挡层数带来的护甲值加成
        player.player_armor_point += (player.player_armor_point * 0.05f * player.player_hold);
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
            }
            else
            {
                enemy.enemy_armor_point = -enemy.enemy_get_hurt;
                enemy.enemy_get_hurt = 0;
            }
        }
        else
        {
            enemy.enemy_get_hurt = (player.player_attack_point + player.player_skill_point);
            enemy.enemy_health -= (int)enemy.enemy_get_hurt;
        }
        enemy.enemy_have_armor = true;
    }
    //计算玩家受伤
    public void Get_hurt_player(Player player, Enemy enemy)
    {
        //计算增强层数带来的攻击值加成
        enemy.enemy_attack_point += enemy.enemy_attack_point * 0.05f * enemy.enemy_enhance;
        //计算抵挡层数带来的护甲值加成
        enemy.enemy_armor_point += enemy.enemy_armor_point * 0.05f * enemy.enemy_hold;
        player.player_get_hurt = enemy.enemy_attack_point - player.player_armor_point_origin;
        if (player.player_get_hurt > 0)
        {
            player.player_health -= (int)player.player_get_hurt;
            player.player_armor_point_origin = 0;
        }
        else
        {
            player.player_armor_point_origin = -player.player_get_hurt;
            player.player_get_hurt = 0;
        }
    }
    //在场景中遭遇敌人
    public void Get_Enemy_Outside(string type)
    {
        Message.Msg.enemy_in_instances.Add(type);
    }
    //在场景中获得卡牌
    public void Get_Card_Outside(string type)
    {
        if (Message.Msg.card_bank.Count <= 0) return;
        if (Message.Msg.bank_in_instances.Contains(type)) return;
        Message.Msg.bank_in_instances.Add(type);
    }
    //在场景中获得装备牌
    public void Get_Equipement_Outside(string type)
    {
        if (Message.Msg.card_bank.Count <= 0) return;
        if (Message.Msg.equipement_instance.Contains(type)) return;
        if (Get_Card_Data(type).type == "club") Message.Msg.equipement_instance.Add(type);
        if (Get_Card_Data(type).type == "curse") Message.Msg.equipement_instance.Insert(0, type);
    }
    //从牌库（弃牌堆）获得随机（指定）卡牌
    public void Get_card(bool place = true, string type = null)
    {
        if (place)
        {
            if (Round_Message.RMsg.bank_in_instances.Count <= 0) return;
            //从牌库
            if (type == null)
            {
                //随机卡牌
                int r = random.Next(0, Round_Message.RMsg.bank_in_instances.Count);
                Round_Message.RMsg.hand_in_instances.Add(Round_Message.RMsg.bank_in_instances[r]);
                Round_Message.RMsg.bank_in_instances.Remove(Round_Message.RMsg.bank_in_instances[r]);
            }
            else
            {
                //指定卡牌
                Round_Message.RMsg.hand_in_instances.Add(type);
                Round_Message.RMsg.bank_in_instances.Remove(type);
            }
        }
        else
        {
            if (Round_Message.RMsg.bank_out_instances.Count <= 0) return;
            //从弃牌堆
            if (type == null)
            {
                //随机卡牌
                int r = random.Next(0, Round_Message.RMsg.bank_out_instances.Count);
                Round_Message.RMsg.hand_out_instances.Add(Round_Message.RMsg.bank_out_instances[r]);
                Round_Message.RMsg.bank_out_instances.Remove(Round_Message.RMsg.bank_out_instances[r]);
            }
            else
            {
                //指定卡牌
                Round_Message.RMsg.hand_in_instances.Add(type);
                Round_Message.RMsg.bank_out_instances.Remove(type);
            }
        }
    }
    //弃牌
    public void Drop_card()
    {
        foreach (string card in Round_Message.RMsg.hand_out_instances)
        {
            Round_Message.RMsg.bank_out_instances.Add(card);
            Get_Card_Instances(card).GetComponent<CardHoverEffect>().isInteractable = true;
            Get_Card_Instances(card).transform.Find("Detial").gameObject.SetActive(false);
            Get_Card_Instances(card).SetActive(false);
        }
        Round_Message.RMsg.hand_out_instances.Clear();
        Round_Message.RMsg.Hand_out_card_num = 0;

    }
    //判断当前敌人是否死亡
    public bool Death_enemy(Enemy enemy)
    {
        if (enemy.enemy_health <= 0)
        {
            string s = null;
            foreach (var type in Message.Msg.data_enemy)
            {
                if (type.Value == enemy)
                {
                    s = type.Key;
                    break;
                }
            }
            Round_Message.RMsg.enemy_instances.Remove(s);
            Debug.Log("enemy died");
            return true;
        }
        return false;
    }
    //清空玩家数据
    public void Data_clear_round()
    {
        Round_Message.RMsg.Player.player_get_hurt = 0;
        Round_Message.RMsg.Player.player_attack_point = 0;
        Round_Message.RMsg.Player.player_skill_point = 0;
        Round_Message.RMsg.Player.player_armor_point = 0;
        Round_Message.RMsg.Player.scale = 1;

        Round_Message.RMsg.skill_action.Clear();

        Round_Message.RMsg.select_action.Clear();
    }
    //清空敌人数据
    public void Data_clear_endround()
    {
        Round_Message.RMsg.Player.player_get_hurt = 0;
        Round_Message.RMsg.Player.player_attack_point = 0;
        Round_Message.RMsg.Player.player_skill_point = 0;
        Round_Message.RMsg.Player.player_armor_point = 0;
        Round_Message.RMsg.Player.scale = 1;

        Round_Message.RMsg.skill_action.Clear();

        Round_Message.RMsg.Player.player_armor_point_origin = 0;
        Round_Message.RMsg.Player.player_enhance = 0;
        Round_Message.RMsg.Player.player_hold = 0;

        Round_Message.RMsg.Enemy_Now.enemy_get_hurt = 0;

        foreach (string card in Round_Message.RMsg.bank_out_instances)
        {
            Round_Message.RMsg.bank_in_instances.Add(card);
        }
        Round_Message.RMsg.bank_out_instances.Clear();
        Round_Message.RMsg.round_end_action.Clear();
        Round_Message.RMsg.DropRound = 0;
        Round_Message.RMsg.Round = 0;
        Round_Message.RMsg.MaxRound = 3;
    }
    //退出战斗重置数据
    public void Data_clear_combat()
    {
        foreach (string type in Message.Msg.bank_in_instances)
        {
            Get_Card_Data(type).point_temp = 0;
        }
    }
    //获得当前选择卡牌
    public void Get_Now_Card(GameObject card)
    {
        foreach (var type in Message.Msg.instance_select)
        {
            if (type.Value == card)
            {
                Round_Message.RMsg.Card_Now = type.Key;
                break;
            }
        } 
    }
}