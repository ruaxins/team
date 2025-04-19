using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Basic : MonoBehaviour
{ 
}
//����ϵͳ
public class Card
{
    //��������
    public string type;//diamond-���飬spade-���ң�club-÷����heart-����
    public string point_show;//չʾ�ĵ���3-10,JQKA
    public float point;//����ĵ���
    public string skill_message;//����Ч����Ϣ
    public int weight;//�������͵�Ȩ��

    public bool isshowed = false;//����ս�����Ƿ�չʾ
    public bool isused = false;//�Ƿ�ʹ��

    //��ʼ��������Ϣ
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
//����ϵͳ
public class Skills
{
    Manager manager = new Manager();
    //��ʼ�������
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
            action(player, enemy);
        else
            Debug.Log("�Ҳ����ü���");
    }
    #region ����Ч��
    //���ܿ�
    void Heart3(Player player, Enemy enemy)
    {
        //����˺���������
        player.player_attack_point *= 2;
    }
    void Heart4(Player player, Enemy enemy)
    {
        //���ӻ�������˺�
        enemy.enemy_have_armor = false;
    }
    void Heart5(Player player, Enemy enemy)
    {
        //��һ���ֵ����
        player.player_armor_point *= 2;
    }
    void Heart6(Player player, Enemy enemy)
    {
        //���غϽ�����Ϊ��ӵ��б�
        Round_Message.RMsg.round_end_action.Add("heart6_plus");
    }
    void Heart6_plus(Player player, Enemy enemy)
    {
        //��һغϽ���ʱ�Ե�����ɵ�ǰ����ֵ���˺�
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
        //�۳�����ǰѪ��ֵ�İٷ�֮�壬���غ������ٷ�֮��Ĺ���ֵ
        player.player_health -= (int)(player.player_health * 0.05f);
        player.player_enhance++;
    }
    void Heart8(Player player, Enemy enemy)
    {
        //�۳�����ǰѪ��ֵ�İٷ�֮ʮ
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
        //��ѡ��UI
    }
    void Heart9(Player player, Enemy enemy)
    {
        //�ظ�����˺�50%������ֵ
        float player_skill_point = player.player_skill_point + player.player_attack_point * 0.05f * player.player_enhance;
        float player_attack_point = player.player_attack_point * player.scale;
        player_skill_point *= player.scale;
        int health = (int)(player_attack_point + player_skill_point) / 2;
        if (player.player_health + health >= player.player_health_max)
            player.player_health = player.player_health_max;
        else
            player.player_health += health;
    }
    void Heart10(Player player, Enemy enemy)
    {
        //ѡ��һ�ſ��Ʋ�ʹ�������һ
        //��ѡ��UI
    }
    void HeartJ(Player player, Enemy enemy)
    {
        //�۳�����ǰѪ��ֵ�İٷ�֮ʮ
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
        //��ѡ��UI
    }
    void HeartQ(Player player, Enemy enemy)
    {
        //�Ե������15���˺������������5���˺�
        enemy.enemy_health -= 15;
        player.player_health -= 5;
    }
    void HeartK(Player player, Enemy enemy)
    {
        //����5�㻤��ֵ
        player.player_armor_point += 5;
    }
    void HeartA(Player player, Enemy enemy)
    {
        //������ת���ɺ��ң����ߺ���ת���ɷ���
        //��ѡ��UI
    }
    //÷����
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
        //����һ�غ�
        Round_Message.RMsg.MaxRound += 1;
        //���غϽ�����Ϊ��ӵ��б�
        Round_Message.RMsg.round_end_action.Add("club5_plus");
    }
    public void Club5_plus(Player player, Enemy enemy)
    {
        Round_Message.RMsg.MaxRound = 3;
        player.player_health -= (int)(player.player_health * 0.05f) * 2;
    }
    public void Club6(Player player, Enemy enemy)
    {
        int r = random.Next(0, 2);
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
        int r = random.Next(0, 10);
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
        //��ѡ��UI
    }
    //���俨
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
//����ϵͳ
public class Enemy
{
    Manager manager = new Manager();
    System.Random random = new System.Random();
    //���������
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
    public Enemy(int enemy_health, float enemy_attack_point, float enemy_armor_point, List<int> attack_mode_list)
    {
        this.enemy_health = enemy_health;
        this.enemy_attack_point = enemy_attack_point;
        this.enemy_attack_point_origin = enemy_attack_point;
        this.enemy_armor_point = enemy_armor_point;
        this.enemy_armor_point_origin = enemy_armor_point;
        this.attack_mode_list = attack_mode_list;
    }
    #region ����ģʽ
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
        int r = random.Next(0, Round_Message.RMsg.hand_in_card_list.Count);
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
//��ɫϵͳ
public class Player
{
    //�����ɫ������
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
//����ϵͳ
public class Manager
{
    //��ʼ�������
    System.Random random = new System.Random();
    public void Get_card_combination(Player player)
    {
        List<int> weights = new List<int>();
        List<string> types = new List<string>();
        //����
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
                types[j + 1] = types[j];
                j--;
            }
            weights[j + 1] = current;
            types[j + 1] = curr;
        }
        //�ж�
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

        if (temp_num == 1) temp_scale = 1;//����
        if (temp_num == 2) temp_scale = 3;//����
        if (temp_num == 3) temp_scale = 4;//����
        if (temp_num == 3) temp_scale = 4;//����
        if (temp_type == 5) temp_scale = 5;//ͬ��
        if (temp_straight == 5) temp_scale = 5;//˳��
        if (temp_num == 4) temp_scale = 6;//��«
        if (temp_type == 5 && temp_straight == 5) temp_scale = 8;//ͬ��˳

        player.scale = temp_scale;
    }
    public void Get_card(int n = -1, bool place = true)
    {
        if (Round_Message.RMsg.bank_in_cards.Count <= 0) return;
        if (place)
        {
            //���ƿ�
            if (n == -1)
            {
                //�������
                int r = random.Next(0, Round_Message.RMsg.bank_in_cards.Count);
                Round_Message.RMsg.bank_in_cards[r].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.bank_in_cards[r]);
                Round_Message.RMsg.bank_in_cards.Remove(Round_Message.RMsg.bank_in_cards[r]);
            }
            else
            {
                //ָ������
                Round_Message.RMsg.hand_in_card_list[n].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_in_card_list[n]);
                Round_Message.RMsg.bank_in_cards.Remove(Round_Message.RMsg.hand_in_card_list[n]);
            }
        }
        else
        {
            //�����ƶ�
            if (n == -1)
            {
                //�������
                int r = random.Next(0, Round_Message.RMsg.bank_out_cards.Count);
                Round_Message.RMsg.bank_out_cards[r].isused = false;
                Round_Message.RMsg.bank_in_cards[r].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.bank_out_cards[r]);
                Round_Message.RMsg.bank_out_cards.Remove(Round_Message.RMsg.bank_out_cards[r]);
            }
            else
            {
                //ָ������
                Round_Message.RMsg.hand_in_card_list[n].isused = false;
                Round_Message.RMsg.hand_in_card_list[n].isshowed = true;
                Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_in_card_list[n]);
                Round_Message.RMsg.bank_out_cards.Remove(Round_Message.RMsg.hand_in_card_list[n]);
            }
        }
    }
    public void Get_hurt_player(Player player, Enemy enemy)
    {
        Debug.Log("��һ��ף�" + player.player_armor_point_origin + "���˹�����" + enemy.enemy_attack_point);
        player.player_get_hurt = enemy.enemy_attack_point - player.player_armor_point_origin;
        if (player.player_get_hurt > 0)
        {
            player.player_health -= (int)player.player_get_hurt;
            player.player_armor_point_origin = 0;
            Debug.Log("����ܵ��˺���" + player.player_get_hurt);
        }
        else
        {
            player.player_armor_point_origin = -player.player_get_hurt;
            player.player_get_hurt = 0;
            Debug.Log("����ܵ��˺���" + player.player_get_hurt);
        }
    }
    public void Get_hurt_ennemy(Player player, Enemy enemy)
    {
        //������ǿ���������Ĺ���ֵ�ӳ�
        player.player_skill_point += player.player_attack_point * 0.05f * player.player_enhance;
        //����ֵ����������Ļ���ֵ�ӳ�
        player.player_armor_point += (player.player_armor_point * 0.05f * player.player_hold);
        Debug.Log("��ҹ���ֵ:" + player.player_attack_point + "��Ҽ��ܹ���ֵ:" + player.player_skill_point +
                    "��һ��ף�" + player.player_armor_point_origin + "��ұ��غϻ��ף�" + player.player_armor_point + "��ұ���:" + player.scale);
        //�������ʹ����Ľ��㱶��
        player.player_attack_point *= player.scale;
        player.player_skill_point *= player.scale;
        player.player_armor_point_origin += (player.player_armor_point * player.scale);
        //�ж��Ƿ�͸����
        if (enemy.enemy_have_armor)
        {
            enemy.enemy_get_hurt = (player.player_attack_point + player.player_skill_point) - enemy.enemy_armor_point;
            if (enemy.enemy_get_hurt > 0)
            {
                enemy.enemy_health -= (int)enemy.enemy_get_hurt;
                enemy.enemy_armor_point = 0;
                Debug.Log("�����ܵ��˺���" + enemy.enemy_get_hurt);
            }
            else
            {
                enemy.enemy_armor_point = -enemy.enemy_get_hurt;
                enemy.enemy_get_hurt = 0;
                Debug.Log("�����ܵ��˺���" + enemy.enemy_get_hurt);
            }
        }
        else
        {
            enemy.enemy_get_hurt = (player.player_attack_point + player.player_skill_point);
            enemy.enemy_health -= (int)enemy.enemy_get_hurt;
            Debug.Log("�����ܵ��˺���" + enemy.enemy_get_hurt);
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
        foreach (Card card in Round_Message.RMsg.bank_out_cards)
        {
            Round_Message.RMsg.bank_in_cards.Add(card);
        }
        Round_Message.RMsg.bank_out_cards.Clear();
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
            if (name == card.type + card.point_show) return true;
        }
        return false;
    }
    // �ӳ��л�ȡ����
    public GameObject GetObject()
    {
        GameObject obj = Round_Message.RMsg.pool.Dequeue();
        obj.SetActive(true);
        obj.AddComponent<CardHoverEffect>();
        return obj;
    }
    // �����󷵻س���
    public void ReturnObject(GameObject obj)
    {
        GameObject.Destroy(obj.GetComponent<CardHoverEffect>());
        obj.SetActive(false);
        Round_Message.RMsg.pool.Enqueue(obj);
    }
    // �ӳ��л�ȡ����
    public GameObject GetEnemy()
    {
        GameObject obj = Round_Message.RMsg.enemy_pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    // �����󷵻س���
    public void ReturnEnemy(GameObject obj)
    {
        obj.SetActive(false);
        Round_Message.RMsg.enemy_pool.Enqueue(obj);
    }
    public void LoadCard(int n, string type)
    {
        // ����Ҫ�ļ���չ����·�������Resources�ļ���
        GameObject prefab = GetObject();
        if (prefab == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        if (type == "card")
        {
            // ʵ������������
            Round_Message.RMsg.hand_in_instances.Add(prefab);
        }
        else if (type == "equip")
        {
            // ʵ������������
            Round_Message.RMsg.equipment_instances.Add(prefab);
        }
        else
        {
            Debug.Log("can not found this prefab type");
        }
    }
    public void LoadEnemy(int n, string type)
    {
        // ����Ҫ�ļ���չ����·�������Resources�ļ���
        GameObject prefab = GetEnemy();
        if (prefab == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        if (type == "enemy")
        {
            // ʵ������������
            Round_Message.RMsg.enemy_instances.Add(prefab);
        }
        else
        {
            Debug.Log("can not found this prefab type");
        }
    }
    //��������
    public void OnCardClick(GameObject gameObject)
    {
        if (Round_Message.RMsg.hand_out_instances.Contains(gameObject))
        {
            Debug.Log("contains in out");
            gameObject.GetComponent<CardHoverEffect>().isInteractable = true;
            Round_Message.RMsg.Hand_out_card_num--;
            int pos = Round_Message.RMsg.hand_out_instances.IndexOf(gameObject);
            UnSelect_card(pos);
            return;
        }
        if (Round_Message.RMsg.hand_in_instances.Contains(gameObject))
        {
            if (Round_Message.RMsg.Hand_out_card_num >= Round_Message.RMsg.Hand_out_card_num_max) return;
            Debug.Log("contains in in");
            gameObject.GetComponent<CardHoverEffect>().isInteractable = false;
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
    //ѡ������
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
    //ȡ��ѡ������
    public void UnSelect_card(int n)
    {
        Round_Message.RMsg.hand_in_card_list.Add(Round_Message.RMsg.hand_out_card_list[n]);
        Round_Message.RMsg.hand_in_instances.Add(Round_Message.RMsg.hand_out_instances[n]);
        Round_Message.RMsg.hand_out_card_list.Remove(Round_Message.RMsg.hand_out_card_list[n]);
        Round_Message.RMsg.hand_out_instances.Remove(Round_Message.RMsg.hand_out_instances[n]);
    }
}