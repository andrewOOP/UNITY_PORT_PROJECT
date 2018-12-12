using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

    SoundManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

    }

    public void playSound(string sound)
    {

        manager.playSound(sound);
    }
}
