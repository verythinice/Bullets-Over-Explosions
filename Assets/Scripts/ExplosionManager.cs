using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour {
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = this.GetComponent<AudioPlayer>(); 
    }

    void Start()
    {
        audioPlayer.Play("explosion");
    }
}
