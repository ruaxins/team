using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour
{
    public Text health;
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (slider != null)
        {
            slider.value = Round_Message.RMsg.Player.player_health;
            health.text = Round_Message.RMsg.Player.player_health.ToString();
        }
        else
        {
            Debug.Log("can not find slider");
        }
    }
}
