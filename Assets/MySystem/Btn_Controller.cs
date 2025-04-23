using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Controller : MonoBehaviour
{
    public GameObject combat_system;
    public GameObject combat_exit;
    public GameObject settings;
    public GameObject manager;
    public GameObject rule_box;
    public Button select;
    public Button drop;
    public Button fight;
    public Button rules;
    public Button rule_exit;
    public bool enable_x;
    public bool enable_e;
    string state = null;
    void Start()
    {
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
                case null: state = "settings"; settings.SetActive(true); break;
                case "settings": state = null; settings.SetActive(false); break;
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
        Destroy(manager.GetComponent<TurnManager>());
    }
    public void Combat_Back()
    {
        combat_exit.SetActive(false);
    }
    void Combat_Load()
    {
        manager.AddComponent<Combat_System>();
        manager.AddComponent<TurnManager>();
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
        rules.onClick.AddListener(() =>
        {
            Show_Rules();
        });
        rule_exit.onClick.AddListener(() =>
        {
            Exit_Rule();
        });
    }

    void Show_Rules()
    {
        rule_box.SetActive(true);
    }
    void Exit_Rule()
    {
        rule_box.SetActive(false);
    }
}
