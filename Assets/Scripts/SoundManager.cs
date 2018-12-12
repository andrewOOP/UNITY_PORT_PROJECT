using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    Dictionary<string, AudioClip> soundLibrary;
    AudioSource player;

    void Start()
    {
        soundLibrary = new Dictionary<string, AudioClip>();

        player = GetComponent<AudioSource>();

        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds");

        for (int i = 0; i < sounds.Length; i++)
        {

            soundLibrary.Add(sounds[i].name, sounds[i]);
        }
    }

    public void playSound(string sound)
    {
        if (soundLibrary.ContainsKey(sound)) {
            player.PlayOneShot(soundLibrary[sound]);
        }
    }
}
