using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    Dictionary<string, AudioClip> music = new Dictionary<string, AudioClip>();
    private void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/Start");
        music.Add("Start", clip);
        clip = Resources.Load<AudioClip>("Music/Normal");
        music.Add("Normal", clip);
        clip = Resources.Load<AudioClip>("Music/Shop");
        music.Add("Shop", clip);
        clip = Resources.Load<AudioClip>("Music/Warp");
        music.Add("Warp", clip);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged") return;
        music.TryGetValue(other.tag, out AudioClip clip);
        if (clip == Message.Msg.backclip) return;
        Message.Msg.backclip = clip;
        MusicController.Instance.ChangeBackgroundMusic(clip);
    }
}
