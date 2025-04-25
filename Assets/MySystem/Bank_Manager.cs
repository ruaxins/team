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
        Get_Card("spade3");
        Get_Card("spade4");
        Get_Card("spade5");
        Get_Card("spade6");
        Get_Card("spade7");

        Get_Enemy("fire_ghost");
        Get_Enemy("fire_insect");
    }

    void Get_Card(string type)
    {
        manager.Get_Card_Outside(type);
    }
    void Get_Enemy(string type)
    {
        manager.Get_Enemy_Outside(type);
    }
}
