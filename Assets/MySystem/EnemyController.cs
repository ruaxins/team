using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string enemy;

    Manager manager = new Manager();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.Get_Enemy_Outside(enemy);
            //Message.Msg.Enable_x = true;
            Message.Msg.Enemy = gameObject.transform.parent.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") Message.Msg.Enable_x = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.Remove_Enemy_Outside(enemy);
            Message.Msg.Enable_x = false;
            Message.Msg.Enemy = null;
        }
    }
    private void OnDestroy()
    {
        GameObject doorcontroller = GameObject.Find("Manager");
        switch (enemy)
        {
            case "fire_ghost": 
                doorcontroller.GetComponent<DoorController>().Open_Door_0();break;
            case "fire_insect": 
                doorcontroller.GetComponent<DoorController>().Open_Door_1(); break;
            case "fire_slime": 
                doorcontroller.GetComponent<DoorController>().Open_Door_2(); break;
            case "fire_puppet": 
                doorcontroller.GetComponent<DoorController>().Open_Door_3(); break;
            case "fire_specter": 
                doorcontroller.GetComponent<DoorController>().Open_Door_4(); break;
            case "fire_dog": 
                doorcontroller.GetComponent<DoorController>().Open_Door_5(); break;
            case "fire_knight": 
                doorcontroller.GetComponent<DoorController>().Open_Door_6(); break;
            case "fire_monster": 
                doorcontroller.GetComponent<DoorController>().Open_Door_7(); break;
            case "fire_witch": 
                doorcontroller.GetComponent<DoorController>().Open_Door_8(); break;
            case "fire_king": 
                doorcontroller.GetComponent<DoorController>().Open_Door_9(); break;
            default:
                break;
        }
    }
}
