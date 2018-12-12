using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {

    [SerializeField] SceneController sceneController;

    public void playScene(TextAsset script)
    {
        sceneController.loadScene(script);
    }
}
