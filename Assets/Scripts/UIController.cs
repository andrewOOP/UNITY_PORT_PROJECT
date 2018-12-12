using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField] GameObject textbox;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.START_SCENE, enableTextbox);
        Messenger.AddListener(GameEvent.END_SCENE, disableTextbox);

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_SCENE, enableTextbox);
        Messenger.RemoveListener(GameEvent.END_SCENE, disableTextbox);

    }


    // Use this for initialization
    void Start () {
        textbox.SetActive(false);

    }
	

    void enableTextbox()
    {
        textbox.SetActive(true);
    }

    void disableTextbox()
    {
        textbox.SetActive(false);
    }
}
