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
    public GameObject shop;
    public GameObject shop_list;
    public GameObject shop_box;
    public GameObject equip_box;
    public GameObject equiped;
    public GameObject unequiped;
    public Button select_return;
    public Button select_end;
    public Button drop;
    public Button fight;
    public Button rules;
    public Button rule_exit;
    public Button shop_exit;
    public Button shop_cancel;
    public Button shop_confirm;
    public Button equip_exit;
    public Button page_down;
    public Button page_up;
    public Text shop_text;
    public Text money;
    public Text page;
    string state = null;

    List<Vector2> potision = new List<Vector2>
    {
        new Vector2(-650, 180),
        new Vector2(-400, 180),
        new Vector2(-150, 180),
        new Vector2(100, 180),
        new Vector2(350, 180),
        new Vector2(600, 180),
        new Vector2(-650, -180),
        new Vector2(-400, -180),
        new Vector2(-150, -180),
        new Vector2(100, -180),
        new Vector2(350, -180),
        new Vector2(600, -180),
    };
    List<Vector2> potision_equip = new List<Vector2>
    {        
        new Vector2(-200, 0),
        new Vector2(0, 0),
        new Vector2(200, 0),
        new Vector2(-400, 0),
        new Vector2(400, 0),
    };

    Manager manager = new Manager();
    Skills skill = new Skills();
    void Start()
    {
        if (round_manager == null)
        {
            round_manager = GameObject.Find("RoundManager");
        }
    }
    void Update()
    {
        money.text = "金币：" + Message.Msg.Money;
        if (Message.Msg.Enable_e && Input .GetKeyDown(KeyCode.E))
        {
            Message.Msg.IsLock = true;
            state = "shop";
            Message.Msg.Enable_e = false;

            Shop_Load();
        }
        if (Message.Msg.Enable_x && Input .GetKeyDown(KeyCode.X))
        {
            Message.Msg.IsLock = true;
            state = "combat";
            combat_system.SetActive(true);
            Message.Msg.Enable_x = false;

            Combat_Load();
        }
        if (Message.Msg.Enable_Tab && Input.GetKeyDown(KeyCode.Tab))
        {
            Message.Msg.IsLock = true;
            state = "equip";
            Message.Msg.Enable_Tab = false;

            Equip_Load();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case "combat": combat_exit.SetActive(true); Message.Msg.IsLock = false; break;
                default:
                    break;
            }
            Message.Msg.IsLock = !Message.Msg.IsLock;
        }
    }
    public void Flash_Pos()
    {
        foreach (string type in Message.Msg.shop_instances)
        {
            GameObject instance = manager.Get_Shop_Instance(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(shop_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Message.Msg.shop_instances.IndexOf(type);
            rt.anchoredPosition = potision[pos];
        }
    }
    public void Flash_Pos_Equip()
    {
        foreach (string type in Message.Msg.equip_instances)
        {
            Debug.Log("equiped"+type);
            GameObject instance = manager.Get_Equip_Instance(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(equiped.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Message.Msg.equip_instances.IndexOf(type);
            rt.anchoredPosition = potision_equip[pos];
        }
        foreach (string type in Message.Msg.equipement_instance)
        {
            Debug.Log("unequiped" + type);
            GameObject instance = manager.Get_Equip_Instance(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(unequiped.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = Message.Msg.equipement_instance.IndexOf(type);
            rt.anchoredPosition = potision[pos];
        }
    }
    public void Equip_Load()
    {
        equip_box.SetActive(true);
        Flash_Pos_Equip();
    }
    public void Equip_Exit()
    {
        equip_box.SetActive(false);
        Message.Msg.Enable_Tab = true;
        Message.Msg.IsLock = false;
    }
    public void Shop_Load()
    {
        shop.SetActive(true);
        AudioClip newClip = Resources.Load<AudioClip>("Music/Shop");
        MusicController.Instance.ChangeBackgroundMusic(newClip);

        if (Message.Msg.shop_instances.Count <= 0)
        {
            Debug.Log("无卡牌在售");
            return;
        }
        Flash_Pos();
    }
    public void Shop_Exit()
    {        
        shop.SetActive(false);
        MusicController.Instance.ChangeBackgroundMusic(Message.Msg.backclip);
        Message.Msg.Enable_e = true;
        Message.Msg.IsLock = false;
    }
    public void Shop_Cancel()
    {
        shop_box.SetActive(false);
    }
    public void Shop_Confirm()
    {        
        shop_box.SetActive(false);
        if (Message.Msg.Money < manager.Get_Card_Data(Message.Msg.Buy_Card).price) return;
        //买
        Message.Msg.Money -= manager.Get_Card_Data(Message.Msg.Buy_Card).price;
        Message.Msg.shop_instances.Remove(Message.Msg.Buy_Card);
        manager.Get_Equipement_Outside(Message.Msg.Buy_Card);
        manager.Get_Shop_Instance(Message.Msg.Buy_Card).SetActive(false);
        Message.Msg.Buy_Card = null;
        Flash_Pos();
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
        page_down.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Extra_Select_Manager>().Page_Down();
        });
        page_up.onClick.AddListener(() =>
        {
            round_manager.GetComponent<Extra_Select_Manager>().Page_Up();
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
    //确认选择
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
