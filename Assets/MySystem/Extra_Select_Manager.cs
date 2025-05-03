using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Extra_Select_Manager : MonoBehaviour
{
    GameObject card_select;
    GameObject select_list;
    Manager manager = new Manager();
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
    };
    public void Flash()
    {
        if(Round_Message.RMsg.bank_in_instances.Count <= 0) return;
        card_select.transform.position = manager.Get_Select_Instance(Round_Message.RMsg.Card_Now).transform.position;
    }
    public void Start_Select(List<string> list)
    {
        if (list.Count > 0) Round_Message.RMsg.Card_Now = list[0];
        GameObject.Find("Manager").GetComponent<Btn_Controller>().select_box.SetActive(true);
        select_list = GameObject.Find("select_list");
        card_select = GameObject.Find("select_card");
        if (list.Count <= 0)
        {
            Debug.Log("无卡牌可以选择");
            return;
        }

        foreach (string type in list)
        {
            GameObject instance = manager.Get_Select_Instance(type);
            instance.SetActive(true);
            // 设置父对象
            instance.transform.SetParent(select_list.transform, false);
            // 获取RectTransform
            RectTransform rt = instance.GetComponent<RectTransform>();
            // 设置位置（中心点坐标）
            int pos = list.IndexOf(type);
            rt.anchoredPosition = potision[pos];

            Transform childTransform = instance.transform.Find("Name");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // 使用子对象...
                childObject.GetComponent<Text>().text = manager.Get_Card_Data(type).type + '\n' + manager.Get_Card_Data(type).point_show;
            }
        }
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
            case "heart8": Start_Select(Round_Message.RMsg.bank_in_instances); break;
            case "heartJ": Start_Select(Round_Message.RMsg.bank_out_instances); break;
            case "heart10": Start_Select(Round_Message.RMsg.hand_in_instances); break;
            case "heartA": Start_Select(Round_Message.RMsg.hand_in_instances); break;
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
