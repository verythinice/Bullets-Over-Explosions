using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour {

    AudioSource bgm;
    private static BGM instance = null;
    public static BGM Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            bgm = GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this.gameObject);
        Play();
    }

    void OnLevelWasLoaded(int level)
    {
        if (level != 0 && level != 15 && level != 16)
        {
            if (!bgm.isPlaying)
            {
                bgm.clip = Resources.Load("Sounds/Slamstorm - Quad City DJs vs. Darude") as AudioClip;
                Play();
            }
        }
        else if (level == 16)
        {
            if (!bgm.isPlaying) {
                bgm.clip = Resources.Load("Sounds/theme of sanic hegehog") as AudioClip;
                Play();
            }
        }
        else
        {
            bgm.Stop();
        }

    }

    public void Play()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }
}
