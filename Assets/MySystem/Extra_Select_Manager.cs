using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Extra_Select_Manager : MonoBehaviour
{
    GameObject card_select;
    GameObject select_list;
    List<string> list;
    Manager manager = new Manager();
    public int page = 1;
    int page_max;
    List<Vector2> potision = new List<Vector2>
    {
        new Vector2(-600, 200),
        new Vector2(-300, 200),
        new Vector2(0, 200),
        new Vector2(300, 200),
        new Vector2(600, 200),
        new Vector2(-600, -200),
        new Vector2(-300, -200),
        new Vector2(0, -200),
        new Vector2(300, -200),
        new Vector2(600, -200),
    };
    public void Flash()
    {
        if(Round_Message.RMsg.bank_in_instances.Count <= 0) return;
        card_select.transform.position = manager.Get_Select_Instance(Round_Message.RMsg.Card_Now).transform.position;
    }
    public void Flash_Page()
    {
        if (list.Count > 0) Round_Message.RMsg.Card_Now = list[(page - 1) * 10];
        GameObject.Find("Manager").GetComponent<Btn_Controller>().page.text = page.ToString();
        foreach (string type in list)
        {
            manager.Get_Select_Instance(type).SetActive(false);
        }
        for (int i = (page - 1) * 10; i < page * 10; i++)
        {
            if (i >= list.Count) break;
            string type = list[i];
            GameObject instance = manager.Get_Select_Instance(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(select_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            rt.anchoredPosition = potision[i%10];
        }
    }
    public void Page_Down()
    {
        if (page >= page_max) return;
        page++;
        Flash_Page();
    }
    public void Page_Up()
    {
        if(page <= 1) return;
        page--;
        Flash_Page();
    }
    public void Start_Select()
    {
        page_max = (list.Count-1)/10+1;
        GameObject.Find("Manager").GetComponent<Btn_Controller>().select_box.SetActive(true);
        select_list = GameObject.Find("select_list");
        card_select = GameObject.Find("select_card");
        if (list.Count <= 0)
        {
            Debug.Log("无卡牌可以选择");
            return;
        }

        Flash_Page();
    }
    public void StartSelectTurn()
    {
        StartCoroutine(SelectAction());
    }
    public IEnumerator SelectAction()
    {
        Round_Message.RMsg.IsComplete = false;
        switch (Round_Message.RMsg.select_action[0])
        {
            case "heart8":list = Round_Message.RMsg.bank_in_instances; Start_Select(); break;
            case "heartJ": list = Round_Message.RMsg.bank_out_instances; Start_Select(); break;
            case "heart10": list = Round_Message.RMsg.hand_in_instances; Start_Select(); break;
            case "heartA": list = Round_Message.RMsg.hand_in_instances; Start_Select(); break;
            case "clubA": list = Round_Message.RMsg.bank_in_instances; Start_Select(); break;
            default:
                break;
        }
        while (!Round_Message.RMsg.IsComplete)
        {
            Flash();
            yield return null;
        }
    }
}
