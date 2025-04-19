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

    #region ��ʼ�����
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
        //����
        if (Round_Message.RMsg.hand_in_card_list.Count < Round_Message.RMsg.Hand_in_card_num_max)
            for (int i = Round_Message.RMsg.hand_in_card_list.Count; i < Round_Message.RMsg.Hand_in_card_num_max; i++)
            {
                manager.Get_card();
                manager.LoadCard(i, "card");
                if (manager.Search_equipment("club10")) clubEvent10.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
            }
        //װ��ʵ��
        for (int i = 0; i < Round_Message.RMsg.equipment_bar.Count; i++)
        {
            manager.LoadCard(i, "equip");
        }
        for (int i = 0; i < Round_Message.RMsg.enemy_fight.Count; i++)
        {
            manager.LoadEnemy(i, "enemy");
        }

        //Ĭ��ָ����һ������
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
        //���ֵ���club
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
    //��ʼ����Ϸ����
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
        #region ��������ʵ��

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
        #region ��������
        //���˹���ģʽ
        //0-��ɹ���ֵ�˺���1-������ǿ��2-����+5��3-��������4-��ÿ�Ѫ���Ļ��ף�5-���һ������
        //6-ʧȥһ�����ƣ�7-����+15��8-����������У�9-�����ǿ��10-�Ĳ�ֵ�
        List<int> mode = new List<int>
        {
            0,0,1
        };
        //��ʼ�����ˣ�Ѫ��������ֵ�����ף�����ģʽ��
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
        #region ���ƣ��������
        //�������
        //������
        Round_Message.RMsg.bank_in_cards.Add(d7);
        Round_Message.RMsg.bank_in_cards.Add(d8);
        Round_Message.RMsg.bank_in_cards.Add(d9);
        Round_Message.RMsg.bank_in_cards.Add(d10);
        Round_Message.RMsg.bank_in_cards.Add(dJ);
        //Round_Message.RMsg.bank_in_cards.Add(dQ);
        //Round_Message.RMsg.bank_in_cards.Add(dK);
        //Round_Message.RMsg.bank_in_cards.Add(dA);
        //������
        Round_Message.RMsg.bank_in_cards.Add(sJ);
        Round_Message.RMsg.bank_in_cards.Add(sQ);
        Round_Message.RMsg.bank_in_cards.Add(sK);
        Round_Message.RMsg.bank_in_cards.Add(sA);
        ////װ����
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
        ////���俨
        //Round_Message.RMsg.equipment_bar.Insert(0, z1);
        //Round_Message.RMsg.equipment_bar.Insert(0, z2);
        //Round_Message.RMsg.equipment_bar.Insert(0, z3);
        //Round_Message.RMsg.equipment_bar.Insert(0, z4);
        //Round_Message.RMsg.equipment_bar.Insert(0, z5);
        //���ܿ�
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
        //�������
        Round_Message.RMsg.enemy_fight.Add(e1);
        Round_Message.RMsg.enemy_fight.Add(e2);
        Round_Message.RMsg.enemy_fight.Add(e3);
        //���
        Round_Message.RMsg.Player = player;
        #endregion
    }
    public void Flash_pos()
    {
        for (int i = 0; i < Round_Message.RMsg.hand_in_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.hand_in_instances[i];
            // ���ø�����
            instance.transform.SetParent(hand_in_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            rt.anchoredPosition = potision[i]; // ����100���أ�����50����
            //Debug.Log("pos"+instance.transform.position);  
            instance.transform.localScale = new Vector2(1, 1);
            instance.GetComponent<CardHoverEffect>().originalPosition = instance.transform.position;
            instance.GetComponent<CardHoverEffect>().originalScale = new Vector2(1, 1);
            //Debug.Log("update" + instance.GetComponent<CardHoverEffect>().originalPosition + "," + instance.GetComponent<CardHoverEffect>().originalScale);
            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.hand_in_card_list[i].type + '\n' + Round_Message.RMsg.hand_in_card_list[i].point_show;
            }
        }
        for (int i = 0; i < Round_Message.RMsg.equipment_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.equipment_instances[i];
            // ���ø�����
            instance.transform.SetParent(equipement_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            rt.anchoredPosition = potisions[i]; // ����100���أ�����50����
            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = Round_Message.RMsg.equipment_bar[i].type + '\n' + Round_Message.RMsg.equipment_bar[i].point_show;
            }
        }
        for (int i = 0; i < Round_Message.RMsg.enemy_instances.Count; i++)
        {
            GameObject instance = Round_Message.RMsg.enemy_instances[i];
            // ���ø�����
            instance.transform.SetParent(enemy_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            rt.anchoredPosition = potision_e[i]; // ����100���أ�����50����

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // ʹ���Ӷ���...
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
        detial.text = "���Ѫ��:" + Round_Message.RMsg.Player.player_health.ToString() + '\n' +
                      //"��ҹ���ֵ:" + player.player_attack_point.ToString() + '\n' +
                      //"��Ҽ��ܹ���ֵ:" + player.player_skill_point.ToString() + '\n' +
                      "��һ���ֵ:" + Round_Message.RMsg.Player.player_armor_point_origin.ToString() + '\n' +
                      "��ǰ����Ѫ��:" + Round_Message.RMsg.Enemy_Now.enemy_health.ToString() + '\n' +
                      "��ǰ���˹���ֵ:" + Round_Message.RMsg.Enemy_Now.enemy_attack_point.ToString() + '\n' +
                      "��ǰ���˻���ֵ:" + Round_Message.RMsg.Enemy_Now.enemy_armor_point.ToString() + '\n' +
                      //"����ܵ��˺�:" + player.player_get_hurt.ToString() + '\n' +
                      "��ǰ�����ܵ��˺�:" + Round_Message.RMsg.Enemy_Now.enemy_get_hurt.ToString();

        //�������Ƿ�����
        if (Round_Message.RMsg.enemy_fight.Count <= 0) Debug.Log("game win");

        //�������Ƿ�����
        manager.Death_player(Round_Message.RMsg.Player);

        //�ж��Ƿ��ɱ����
        if (Round_Message.RMsg.enemy_fight.Count > 0 && manager.Death_enemy(Round_Message.RMsg.Enemy_Now)) Round_Message.RMsg.Enemy_Now = Round_Message.RMsg.enemy_fight[0];

    }
    #region �ⲿ���÷���
    public string Show_enemy(int n)
    {
        if (Round_Message.RMsg.enemy_fight.Count <= n) return null;
        return Round_Message.RMsg.enemy_fight[n].enemy_health.ToString();
    }

    //���»غ�_Fight��ť
    public void Next_round()
    {
        //���ӻغ���
        Round_Message.RMsg.Round += 1;
        //ִ�п���Ч�����������ͱ��ʣ����ѡ�п���
        Use_card(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        if (manager.Search_equipment("curse3")) curseEvent3.Invoke(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        //�ж���һغ��Ƿ����
        if (Round_Message.RMsg.Round > Round_Message.RMsg.MaxRound)
        {
            //�����غϽ���Ч��
            if (Round_Message.RMsg.round_end_action.Count > 0)
            {
                foreach (string action in Round_Message.RMsg.round_end_action)
                {
                    skill.Get_skills(action, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
                }
            }
            //�������غ�
            Debug.Log("enemy_round");
            Enemy_attack();
            //�غϽ�����ռӳ�
            manager.Data_clear_enemy(Round_Message.RMsg.Enemy_Now);
            manager.Data_clear_card();
            //���ûغ���
            Round_Message.RMsg.Round = 1;
            //�ж��Ƿ����club
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
        //�غϽ�����ռӳ�
        manager.Data_clear_player(Round_Message.RMsg.Player, false);
        //���ƿⲹ�俨��������
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
    //���ƿ�ѡ���Ƶ��ⲿ����
    public void Heart8_plus(int n)
    {
        manager.Get_card(n, true);
    }
    //�����ƶ�ѡ���Ƶ��ⲿ����
    public void HeartJ_plus(int n)
    {
        manager.Get_card(n, false);
    }
    //������ѡ���Ƶ��ⲿ����
    public void Heart10_plus(int n)
    {
        if (Round_Message.RMsg.hand_out_card_list[n].type == "diamond")
            Round_Message.RMsg.Player.player_attack_point++;
        else if (Round_Message.RMsg.hand_out_card_list[n].type == "spade")
            Round_Message.RMsg.Player.player_armor_point++;
        else
            Debug.Log("�ÿ����޷���ǿ��");

    }
    //������ѡ���Ƶ��ⲿ����
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
            Debug.Log("�ÿ����޷���ת��");
    }
    //������ѡ���Ƶ��ⲿ����
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
    //����_Drop
    public void Drop()
    {
        if (Round_Message.RMsg.DropRound >= Round_Message.RMsg.MaxDropRound) return;
        Drop_card();
        Round_Message.RMsg.DropRound++;
        //���ƿⲹ�俨��������
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
    //�������
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
