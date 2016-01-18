using UnityEngine;
using System.Collections;

public class DearSister : MonoBehaviour {
    AudioSource sfxPlayer;
    private bool played = false;

    void Awake()
    {
        sfxPlayer = GameObject.Find("MMMPlayer").GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (this.CompareTag("Dead") && !played)
        {
            sfxPlayer.Stop();
            sfxPlayer.PlayOneShot(sfxPlayer.clip);
            played = true;
        }
    }
}
