using UnityEngine;
using System.Collections;

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


    public void Play()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }
}
