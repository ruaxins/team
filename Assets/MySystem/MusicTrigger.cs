using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public GameObject Btn_E;
    public GameObject Btn_X;
    Dictionary<string, AudioClip> music = new Dictionary<string, AudioClip>();
    private void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/Start");
        music.Add("Start", clip);
        clip = Resources.Load<AudioClip>("Music/Normal");
        music.Add("Normal", clip);
        clip = Resources.Load<AudioClip>("Music/Warp");
        music.Add("Warp", clip);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Start" || other.tag == "Normal" || other.tag == "Warp")
        {
            music.TryGetValue(other.tag, out AudioClip clip);
            if (clip == Message.Msg.backclip) return;
            Message.Msg.backclip = clip;
            MusicController.Instance.ChangeBackgroundMusic(clip);
        }
        else if(other.tag == "Shop")
        {
            Message.Msg.Enable_e = true;
        }
        else if (other.tag == "Stone")
        {
            Message.Msg.Enable_Talk = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shop")
        {
            Message.Msg.Enable_e = true;
            Btn_E.SetActive(true);
            Btn_E.transform.position = other.transform.position + new Vector3(0,50,0);
        }
        else if (other.tag == "Stone")
        {
            Message.Msg.Enable_Talk = true;
            Btn_E.SetActive(true);
            Btn_E.transform.position = other.transform.position + new Vector3(0, 50, 0);
        }
        else if (other.tag == "Enemy")
        {
            Btn_X.SetActive(true);
            Btn_X.transform.position = other.transform.position + new Vector3(0, 50, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            Message.Msg.Enable_e = false;
            Btn_E.SetActive(false);
        }
        else if (other.tag == "Stone")
        {
            Message.Msg.Enable_Talk = false;
            Btn_E.SetActive(false);
        }
        else if (other.tag == "Enemy")
        {
            Btn_X.SetActive(false);
        }
    }
}
