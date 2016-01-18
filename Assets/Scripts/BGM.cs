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

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && bgm.isPlaying)
        {
            bgm.Stop();
        }
        if (SceneManager.GetActiveScene().buildIndex == 15 && bgm.isPlaying)
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
