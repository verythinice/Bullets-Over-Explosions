using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour {

    public string[] audioClipNames;

    Dictionary<string, AudioClip> library = new Dictionary<string, AudioClip>();

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    void Awake()
    {
        source = this.GetComponent<AudioSource>();
        foreach (string name in audioClipNames)
        {
            library[name] = Resources.Load("Sounds/" + name) as AudioClip;
        }
    }

    public void Play(string clipName)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(library[clipName], vol);
    }
}