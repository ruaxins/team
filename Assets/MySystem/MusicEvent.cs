using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEvent : MonoBehaviour
{
    public static MusicEvent Instance;

    private AudioSource eventSource;

    public AudioClip Shuffle;
    public AudioClip Click;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            eventSource = gameObject.AddComponent<AudioSource>();
            eventSource.loop = false;
            eventSource.volume = 0.5f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �����������ű��е��� MusicEvent.Instance.ChangeEventMusic(newClip);
    public void ShuffleEventMusic()
    {
        eventSource.Stop();
        eventSource.clip = Shuffle;
        eventSource.Play();
    }

    public void ClickEventMusic()
    {
        eventSource.Stop();
        eventSource.clip = Click;
        eventSource.Play();
    }
}
