using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    public AudioClip backgroundMusic;
    private AudioSource bgmSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.clip = backgroundMusic;
            Message.Msg.backclip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.volume = 0.5f;
            bgmSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 可以在其他脚本中调用 MusicController.Instance.ChangeBackgroundMusic(newClip);
    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();
    }


}
