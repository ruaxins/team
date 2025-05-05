using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Controller : MonoBehaviour
{
    public GameObject combat_system;
    public GameObject combat_exit;
    public GameObject settings;
    public GameObject round_manager;
    public GameObject rule_box;
    public GameObject select_box;
    public GameObject cover_box;
    public Button select_return;
    public Button select_end;
    public Button drop;
    public Button fight;
    public Button rules;
    public Button rule_exit;
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
        if (Message.Msg.Enable_e && Input .GetKeyDown(KeyCode.E))
        {
            Message.Msg.IsLock = true;
            state = "interaction";
            Message.Msg.Enable_e = false;
        }
        if (Message.Msg.Enable_x && Input .GetKeyDown(KeyCode.X))
        {
            Message.Msg.IsLock = true;
            state = "combat";
            combat_system.SetActive(true);
            Message.Msg.Enable_x = false;

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
        foreach (string enemy in Message.Msg.enemy_in_instances)
        {
            manager.Get_Enemy_Data(enemy).FlushEnemy();
        }
        //清空战斗数据
        manager.Data_clear_combat();
        MusicController.Instance.ChangeBackgroundMusic(Message.Msg.backclip);
        fight.onClick.RemoveAllListeners();
        drop.onClick.RemoveAllListeners();
        select_return.onClick.RemoveAllListeners();
        select_end.onClick.RemoveAllListeners();
        rules.onClick.RemoveAllListeners();
        rule_exit.onClick.RemoveAllListeners();
        Message.Msg.IsLock = false;
        state = null;    
        combat_exit.SetActive(false);
        combat_system.SetActive(false);
        Destroy(round_manager.GetComponent<Combat_System>());
        Destroy(round_manager.GetComponent<TurnManager>());
        Destroy(round_manager.GetComponent<Extra_Select_Manager>());
        //Destroy(Message.Msg.Enemy);
        Message.Msg.Enemy = null;
    }
    public void WinGame()
    {
        //清空战斗数据
        manager.Data_clear_combat();
        fight.onClick.RemoveAllListeners();
        drop.onClick.RemoveAllListeners();
        select_return.onClick.RemoveAllListeners();
        select_end.onClick.RemoveAllListeners();
        rules.onClick.RemoveAllListeners();
        rule_exit.onClick.RemoveAllListeners();
        Message.Msg.IsLock = false;
        state = null;
        combat_exit.SetActive(false);
        combat_system.SetActive(false);
        Destroy(round_manager.GetComponent<Combat_System>());
        Destroy(round_manager.GetComponent<TurnManager>());
        Destroy(round_manager.GetComponent<Extra_Select_Manager>());
        Destroy(Message.Msg.Enemy);
        Message.Msg.Enemy = null;
    }
    public void LossGame()
    {
        foreach (string enemy in Message.Msg.enemy_in_instances)
        {
            manager.Get_Enemy_Data(enemy).FlushEnemy();
        }
        //清空战斗数据
        manager.Data_clear_combat();
        fight.onClick.RemoveAllListeners();
        drop.onClick.RemoveAllListeners();
        select_return.onClick.RemoveAllListeners();
        select_end.onClick.RemoveAllListeners();
        rules.onClick.RemoveAllListeners();
        rule_exit.onClick.RemoveAllListeners();
        Message.Msg.IsLock = false;
        state = null;
        combat_exit.SetActive(false);
        combat_system.SetActive(false);
        Destroy(round_manager.GetComponent<Combat_System>());
        Destroy(round_manager.GetComponent<TurnManager>());
        Destroy(round_manager.GetComponent<Extra_Select_Manager>());
        //Destroy(Message.Msg.Enemy);
        Message.Msg.Enemy = null;
    }
    void Combat_Load()
    {
        if (Message.Msg.enemy_in_instances.Count == 0) Debug.Log("No Enemy");
        AudioClip newClip = Resources.Load<AudioClip>("Music/" + manager.Get_Enemy_Data(Message.Msg.enemy_in_instances[0]).enemy_music);
        MusicController.Instance.ChangeBackgroundMusic(newClip);
        cover_box.SetActive(true);
        round_manager.AddComponent<Combat_System>();
        round_manager.AddComponent<TurnManager>();
        round_manager.AddComponent<Extra_Select_Manager>();
        fight.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Combat_System>().Dissolve_Play();
        });
        drop.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Combat_System>().Dissolve_Drop();
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
    //回到游戏界面
    public void Combat_Back()
    {
        MusicEvent.Instance.ClickEventMusic();
        combat_exit.SetActive(false);
    }
    //打开规则界面
    void Show_Rules()
    {
        MusicEvent.Instance.ClickEventMusic();
        rule_box.SetActive(true);
    }
    //关闭规则界面
    void Exit_Rule()
    {
        MusicEvent.Instance.ClickEventMusic();
        rule_box.SetActive(false);
    }
    //取消选择
    public void End_Select()
    {
        MusicEvent.Instance.ClickEventMusic();
        skill.Get_skills(Round_Message.RMsg.select_action[0] + "_plus", Round_Message.RMsg.Player, Round_Message.RMsg.Enemy_Now);
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
