using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Extra_Select_Manager;

public class Btn_Controller : MonoBehaviour
{
    public GameObject combat_system;
    public GameObject combat_exit;
    public GameObject settings;
    public GameObject round_manager;
    public GameObject rule_box;
    public GameObject select_box;
    public Button select_return;
    public Button select_end;
    public Button drop;
    public Button fight;
    public Button rules;
    public Button rule_exit;
    public bool enable_x;
    public bool enable_e;
    string state = null;

    Manager manager = new Manager();
    Skills skill = new Skills();
    void Start()
    {
        if (round_manager == null)
        {
            round_manager = GameObject.Find("RoundManager");
            // 或者 GetComponent 也可以，看你的设计
        }
    }
    void Update()
    {
        if (enable_e && Input .GetKeyDown(KeyCode.E))
        {
            Message.Msg.IsLock = true;
            state = "interaction";
            enable_e = false;
        }
        if (enable_x && Input .GetKeyDown(KeyCode.X))
        {
            Message.Msg.IsLock = true;
            state = "combat";
            combat_system.SetActive(true);
            enable_x = false;

            Combat_Load();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                //case null: state = "settings"; settings.SetActive(true); break;
                //case "settings": state = null; settings.SetActive(false); break;
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
        Destroy(round_manager.GetComponent<Combat_System>());
        Destroy(round_manager.GetComponent<TurnManager>());
        Destroy(round_manager.GetComponent<Extra_Select_Manager>());
    }

    void Combat_Load()
    {
        round_manager.AddComponent<Combat_System>();
        round_manager.AddComponent<TurnManager>();
        round_manager.AddComponent<Extra_Select_Manager>();
        fight.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Combat_System>().Select_Open();
        });
        drop.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Combat_System>().Drop();
        });
        select_return.onClick.AddListener(() =>
        {
            Return_Select();
        });
        select_end.onClick.AddListener(() =>
        {
            End_Select();
        });
        rules.onClick.AddListener(() =>
        {
            Show_Rules();
        });
        rule_exit.onClick.AddListener(() =>
        {
            Exit_Rule();
        });
    }
    public void Combat_Back()
    {
        combat_exit.SetActive(false);
    }
    void Show_Rules()
    {
        rule_box.SetActive(true);
    }
    void Exit_Rule()
    {
        rule_box.SetActive(false);
    }

    public void End_Select()
    {
        skill.Get_skills(Round_Message.RMsg.select_action[0] + "_plus");
        Return_Select();
    }
    public void Return_Select()
    {
        foreach (string type in Message.Msg.bank_in_instances)
        {
            GameObject instance = manager.Get_Select_Instance(type);
            instance.SetActive(false);
        }
        select_box.SetActive(false);
        Round_Message.RMsg.IsComplete = true;

        Round_Message.RMsg.select_action.Remove(Round_Message.RMsg.select_action[0]);

        if (Round_Message.RMsg.select_action.Count > 0)
        {
            GameObject.Find("RoundManager").GetComponent<Extra_Select_Manager>().StartSelectTurn();
        }
        else
        {
            GameObject.Find("RoundManager").GetComponent<Combat_System>().Next_round();
        }
    }
}
