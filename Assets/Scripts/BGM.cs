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
    }


    public void Play()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
