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
        //��0
        foreach (GameObject door in Door_0)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_1()
    {
        //��1
        foreach (GameObject door in Door_1)
        { 
            door.SetActive(false);
        }
    }
    public void Open_Door_2()
    {
        //��2
        foreach (GameObject door in Door_2)
        {
            door.SetActive(false);
        }
        //��1
        foreach (GameObject door in Door_1)
        {
            door.SetActive(true);
        }
    }
    public void Open_Door_3()
    {
        //��3,4
        foreach (GameObject door in Door_3)
        {
            door.SetActive(false);
        }
        foreach (GameObject door in Door_4)
        {
            door.SetActive(false);
        }
        //��2
        foreach (GameObject door in Door_2)
        {
            door.SetActive(true);
        }
    }
    public void Open_Door_4()
    {
        //��1,5,11
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
        //��3,4
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
        //��10
        foreach (GameObject door in Door_10)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_6()
    {
        //��9
        foreach (GameObject door in Door_9)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_7()
    {
        //��8��12
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
        //��7
        foreach (GameObject door in Door_7)
        {
            door.SetActive(false);
        }
    }
    public void Open_Door_9()
    {
        //��6
        foreach (GameObject door in Door_6)
        {
            door.SetActive(false);
        }
    }
}
