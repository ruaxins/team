using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<GameObject> Door_0;
    public List<GameObject> Door_1;
    public List<GameObject> Door_2;
    public List<GameObject> Door_3;
    public List<GameObject> Door_4;
    public List<GameObject> Door_5;
    public List<GameObject> Door_6;
    public List<GameObject> Door_7;
    public List<GameObject> Door_8;
    public List<GameObject> Door_9;
    public List<GameObject> Door_10;
    public List<GameObject> Door_11;
    public List<GameObject> Door_12;

    public void Open_Door_0()
    {
        //开0
        foreach (GameObject door in Door_0)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_1()
    {
        //开1
        foreach (GameObject door in Door_1)
        { 
            door.SetActive(false);
        }
    }
    public void Open_Door_2()
    {
        //开2
        foreach (GameObject door in Door_2)
        {
            door.SetActive(false);
        }
        //闭1
        foreach (GameObject door in Door_1)
        {
            door.SetActive(true);
        }
    }
    public void Open_Door_3()
    {
        //开3,4
        foreach (GameObject door in Door_3)
        {
            door.SetActive(false);
        }
        foreach (GameObject door in Door_4)
        {
            door.SetActive(false);
        }
        //闭2
        foreach (GameObject door in Door_2)
        {
            door.SetActive(true);
        }
    }
    public void Open_Door_4()
    {
        //开1,5,11
        foreach (GameObject door in Door_1)
        {
            door.SetActive(false);
        }
        foreach (GameObject door in Door_5)
        {
            door.SetActive(false);
        }
        foreach (GameObject door in Door_11)
        {
            door.SetActive(false);
        }
        //闭3,4
        foreach (GameObject door in Door_3)
        {
            door.SetActive(true);
        }
        foreach (GameObject door in Door_4)
        {
            door.SetActive(true);
        }
    }
    public void Open_Door_5()
    {
        //开10
        foreach (GameObject door in Door_10)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_6()
    {
        //开9
        foreach (GameObject door in Door_9)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_7()
    {
        //开8，12
        foreach (GameObject door in Door_8)
        {
            door.SetActive(false);
        }
        foreach (GameObject door in Door_12)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_8()
    {
        //开7
        foreach (GameObject door in Door_7)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_9()
    {
        //开6
        foreach (GameObject door in Door_6)
        {
            door.SetActive(false);
        }
    }
}
