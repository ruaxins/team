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

    #region ��ʼ�����
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

        //Ĭ��ָ����һ������
        Round_Message.RMsg.Enemy_Now = manager.Get_Enemy_Data(Round_Message.RMsg.enemy_instances[0]);
    }
    //��ʼ����Ϸ����
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


        #region ���ƣ��������
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

        //���
        Round_Message.RMsg.Player = new Player(100);
        #endregion
    }
    public void Flash_pos()
    {
        foreach (string type in Round_Message.RMsg.hand_in_instances)
        {
            GameObject instance = manager.Get_Card_Instances(type);
            instance.SetActive(true);
            // ���ø�����
            instance.transform.SetParent(hand_in_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            int pos = Round_Message.RMsg.hand_in_instances.IndexOf(type);
            rt.anchoredPosition = potision[pos]; 

            instance.transform.localScale = new Vector2(1, 1);
            instance.GetComponent<CardHoverEffect>().originalPosition = instance.transform.position;
            instance.GetComponent<CardHoverEffect>().originalScale = new Vector2(1, 1);
            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = manager.Get_Card_Data(type).type + '\n' + manager.Get_Card_Data(type).point_show;
            }
        }
        foreach (string type in Round_Message.RMsg.equipment_instances)
        {
            GameObject instance = manager.Get_Card_Instances(type);
            instance.SetActive(true);
            // ���ø�����
            instance.transform.SetParent(equipement_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            int pos = Round_Message.RMsg.equipment_instances.IndexOf(type);
            rt.anchoredPosition = potision[pos];

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = manager.Get_Card_Data(type).type + '\n' + manager.Get_Card_Data(type).point_show;
            }
        }
        foreach (string type in Round_Message.RMsg.enemy_instances)
        {
            GameObject instance = manager.Get_Enemy_Instances(type);
            instance.SetActive(true);
            // ���ø�����
            instance.transform.SetParent(enemy_list.transform, false);
            // ��ȡRectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // ����λ�ã����ĵ����꣩
            int pos = Round_Message.RMsg.enemy_instances.IndexOf(type);
            rt.anchoredPosition = potision_enemy[pos];

            Transform name = instance.transform.Find("Name");
            Transform health = instance.transform.Find("Health");
            Transform attack = instance.transform.Find("Attack");
            Transform defend = instance.transform.Find("Defend");
            if (name != null)
            {
                GameObject childObject = name.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = "���ƣ�" + type;
            }
            if (health != null)
            {
                GameObject childObject = health.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = "Ѫ����" + manager.Get_Enemy_Data(type).enemy_health.ToString();
            }
            if (attack != null)
            {
                GameObject childObject = attack.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = "����ֵ��" + manager.Get_Enemy_Data(type).enemy_attack_point.ToString();
            }
            if (defend != null)
            {
                GameObject childObject = defend.gameObject;
                // ʹ���Ӷ���...
                childObject.GetComponent<Text>().text = "����ֵ��" + manager.Get_Enemy_Data(type).enemy_armor_point.ToString();
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
        
        //����������Ϻ������ֵ����ʾ
        if (Round_Message.RMsg.hand_in_instances.Count > 0)
        {
            manager.Get_card_combination(Round_Message.RMsg.Player);
            manager.Use_card(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
        card_combination.text = "��ǰ���ͱ��ʣ�" + Round_Message.RMsg.Player.scale.ToString();
        card_attack.text = "��ҹ���ֵ��" + Round_Message.RMsg.Player.player_attack_point.ToString();
        card_defend.text = "��һ���ֵ��" + Round_Message.RMsg.Player.player_armor_point_origin.ToString();

        gameround.text = Round_Message.RMsg.Round.ToString();
        // �жϵ�ǰ�����Ƿ�����
        if (Round_Message.RMsg.enemy_instances.Count > 0 && manager.Death_enemy(Round_Message.RMsg.Enemy_Now))
        {
            Round_Message.RMsg.Enemy_Now = manager.Get_Enemy_Data(Round_Message.RMsg.enemy_instances[0]);
            Flash_pos();
        }
        //�ж���Ϸ�Ƿ����
        if (Round_Message.RMsg.enemy_instances.Count <= 0)
        {
            Debug.Log("Win");
            //���ս������
            manager.Data_clear_combat();
            //�˳�ս��
            GameObject.Find("Manager").GetComponent<Btn_Controller>().Combat_Exit();
        }
        if (Round_Message.RMsg.Player.player_health <= 0)
        {
            Debug.Log("Loss");
            //���ս������
            manager.Data_clear_combat();
            //�˳�ս��
            GameObject.Find("Manager").GetComponent<Btn_Controller>().Combat_Exit();
        }
    }
    #region �ⲿ���÷���
    //Fight��ť
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
    //���»غ�
    public void Next_round()
    {
        //ʹ�ÿ���
        foreach (string skills in Round_Message.RMsg.skill_action)
        {
            skill.Get_skills(skills, Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        }
        //�Ե�ǰ��������˺�
        manager.Get_hurt_ennemy(Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
        //����
        manager.Drop_card();
        //���ٳ��ƴ���
        Round_Message.RMsg.Round ++;
        Flash_pos();
        if (Round_Message.RMsg.Round >= Round_Message.RMsg.MaxRound) GameObject.Find("RoundManager").GetComponent<TurnManager>().EndPlayerTurn();
        else GameObject.Find("RoundManager").GetComponent<TurnManager>().StartNewTurn();
    }
    //����_Drop
    public void Drop()
    {
        if (Round_Message.RMsg.DropRound >= Round_Message.RMsg.MaxDropRound) return;
        manager.Drop_card();
        Round_Message.RMsg.DropRound++;
        //���ƿⲹ�俨��������
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
