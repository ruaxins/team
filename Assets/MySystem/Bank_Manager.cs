using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Manager : MonoBehaviour
{
    Manager manager = new Manager();
    void Start()
    {
        Get_Card("diamond3");
        Get_Card("diamond4");
        Get_Card("diamond5");
        Get_Card("diamond6");
        Get_Card("diamond7");
        Get_Card("diamond8");
        Get_Card("diamond9");
        Get_Card("diamond10");
        Get_Card("diamondJ");
        Get_Card("diamondQ");
        Get_Card("diamondK");
        Get_Card("diamondA");

        Get_Card("heart3");
        Get_Card("heart4");
        Get_Card("heart5");
        Get_Card("heart6");
        Get_Card("heart7");
        Get_Card("heart8");
        Get_Card("heart9");
        Get_Card("heart10");
        Get_Card("heartJ");
        Get_Card("heartQ");
        Get_Card("heartK");
        Get_Card("heartA");

        Get_Card("spade3");
        Get_Card("spade4");
        Get_Card("spade5");
        Get_Card("spade6");
        Get_Card("spade7");
        Get_Card("spade8");
        Get_Card("spade9");
        Get_Card("spade10");
        Get_Card("spadeJ");
        Get_Card("spadeQ");
        Get_Card("spadeK");
        Get_Card("spadeA");

        Get_Equipement("club3");
        Get_Equipement("club4");
        Get_Equipement("club5");
        Get_Equipement("club6");
        Get_Equipement("club7");
        Get_Equipement("club8");
        Get_Equipement("club9");
        Get_Equipement("club10");
        Get_Equipement("clubJ");
        Get_Equipement("clubQ");
        Get_Equipement("clubK");
        Get_Equipement("clubA");

        //Get_Equipement("curse1");
        //Get_Equipement("curse2");
        //Get_Equipement("curse3");
        //Get_Equipement("curse4");
        //Get_Equipement("curse5");
    }

    void Get_Card(string type)
    {
        manager.Get_Card_Outside(type);
    }
    void Get_Equipement(string type)
    {
        if (Message.Msg.card_bank.Count <= 0) return;
        if (Message.Msg.shop_instances.Contains(type)) return;
        Message.Msg.shop_instances.Add(type);
    }
}
