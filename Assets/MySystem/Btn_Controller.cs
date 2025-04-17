using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Controller : MonoBehaviour
{
    public GameObject combat_system;
    public GameObject bag;
    public GameObject combat_exit;
    public GameObject settings;
    bool bag_isopen = false;
    public bool enable_x = false;
    public bool enable_e = false;
    public bool enable_f = false;
    string state = null;
    void Start()
    {
        
    }
    void Update()
    {
        if (enable_x && Input .GetKeyDown(KeyCode.X))
        {
            Message.Msg.IsLock = !Message.Msg.IsLock;
            if (bag_isopen)
            {
                bag.SetActive(false);
                state = null;
            }
            else
            {
                bag.SetActive(true);
                state = "bag";
            }
            bag_isopen = !bag_isopen;
        }
        if (enable_e && Input .GetKeyDown(KeyCode.E))
        {
            Message.Msg.IsLock = !Message.Msg.IsLock;
        }
        if (enable_f && Input .GetKeyDown(KeyCode.F))
        {
            state = "combat";
            Message.Msg.IsLock = !Message.Msg.IsLock;
            combat_system.SetActive(true);
            enable_f = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case null: state = "settings"; settings.SetActive(true); break;
                case "settings": state = null; settings.SetActive(false); break;
                case "bag": state = null; bag.SetActive(false); break;
                case "combat": combat_exit.SetActive(true); break;
                default:
                    break;
            }
            Message.Msg.IsLock = !Message.Msg.IsLock;
        }
    }
    public void Combat_Exit()
    {
        state = null;
        Message.Msg.IsLock = !Message.Msg.IsLock;
        combat_exit.SetActive(false);
        combat_system.SetActive(false);
    }
    public void Combat_Back()
    {
        combat_exit.SetActive(false);
    }
}
