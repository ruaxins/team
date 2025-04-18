using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Controller : MonoBehaviour
{
    public GameObject combat_system;
    public GameObject bag;
    public GameObject combat_exit;
    public GameObject settings;
    public GameObject manager;
    public Button select;
    public Button drop;
    public Button fight;
    public bool enable_x;
    public bool enable_e;
    public bool enable_f;
    string state = null;
    void Start()
    {
        
    }
    void Update()
    {
        if (enable_x && Input .GetKeyDown(KeyCode.X))
        {
            Message.Msg.IsLock = true;
            bag.SetActive(true);
            state = "bag";
            enable_x = false;
        }
        if (enable_e && Input .GetKeyDown(KeyCode.E))
        {
            Message.Msg.IsLock = true;
            state = "interaction";
            enable_e = false;
        }
        if (enable_f && Input .GetKeyDown(KeyCode.F))
        {
            Message.Msg.IsLock = true;
            state = "combat";
            combat_system.SetActive(true);
            enable_f = false;

            Combat_Load();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case null: state = "settings"; settings.SetActive(true); break;
                case "settings": state = null; settings.SetActive(false); break;
                case "bag": state = null; bag.SetActive(false); break;
                case "combat": combat_exit.SetActive(true); Message.Msg.IsLock = false; break;
                default:
                    break;
            }
            Message.Msg.IsLock = !Message.Msg.IsLock;
        }
    }
    public void Combat_Exit()
    {
        Message.Msg.IsLock = false;
        state = null;    
        combat_exit.SetActive(false);
        combat_system.SetActive(false);
        Destroy(manager.GetComponent<Combat_System>());
        Combat_Clear();
    }
    public void Combat_Back()
    {
        combat_exit.SetActive(false);
    }
    void Combat_Load()
    {
        manager.AddComponent<Combat_System>();
        fight.onClick.AddListener(() =>
        {
            manager.GetComponent<Combat_System>().Next_round();
        });
        drop.onClick.AddListener(() =>
        {
            manager.GetComponent<Combat_System>().Drop();
        });
        select.onClick.AddListener(() =>
        {
            
        });
    }
    public void Combat_Clear()
    {
        Round_Message.RMsg.equipment_bar.Clear();
        Round_Message.RMsg.bank_out_cards.Clear();
        Round_Message.RMsg.hand_in_card_list.Clear();
        Round_Message.RMsg.hand_out_card_list.Clear();
        Round_Message.RMsg.round_end_action.Clear();
        Round_Message.RMsg.enemy_fight.Clear();
        Round_Message.RMsg.hand_in_instances.Clear();
        Round_Message.RMsg.hand_out_instances.Clear();
        Round_Message.RMsg.enemy_instances.Clear();
        Round_Message.RMsg.equipment_instances.Clear();
        Round_Message.RMsg.pool.Clear();
        Round_Message.RMsg.enemy_pool.Clear();
        Round_Message.RMsg.Hand_in_card_num_max = 8;
        Round_Message.RMsg.Hand_in_card_num = 0;
        Round_Message.RMsg.Hand_out_card_num_max = 5;
        Round_Message.RMsg.Hand_out_card_num = 0;
        Round_Message.RMsg.Round = 1;
        Round_Message.RMsg.MaxRound = 3;
        Round_Message.RMsg.DropRound = 0;
        Round_Message.RMsg.MaxDropRound = 3;
        Round_Message.RMsg.ClubJ = false;
        Round_Message.RMsg.Card_choose = 0;
        Round_Message.RMsg.Enemy_Now = null;

    }
}
