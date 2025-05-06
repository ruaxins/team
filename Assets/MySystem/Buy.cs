using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    public Text price;
    string buy_card;
    Manager manager = new Manager();
    private void Start()
    {
        foreach (var type in Message.Msg.instance_shop)
        {
            if (type.Value == gameObject)
            {
                buy_card = type.Key;
                break;
            }
        }
        price.text = manager.Get_Card_Data(buy_card).price.ToString();
    }
    public void Shop_Box_Open()
    {
        GameObject.Find("Manager").GetComponent<Btn_Controller>().shop_box.SetActive(true);
        Message.Msg.Buy_Card = buy_card;
        if (Message.Msg.Money >= manager.Get_Card_Data(Message.Msg.Buy_Card).price)
            GameObject.Find("Manager").GetComponent<Btn_Controller>().shop_text.text =
            "是否花费" + manager.Get_Card_Data(Message.Msg.Buy_Card).price + "金币购买卡牌？";
        else
            GameObject.Find("Manager").GetComponent<Btn_Controller>().shop_text.text = "金币不足！";
    }
}
