using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimController : MonoBehaviour {

    [SerializeField] GameObject player;

    private Vector3 bestPos;
    private Vector3 orgPos;

    bool startMove = false;
    private float timer = 0;
    private float SPEED = 2f;

    void Awake()
    {
        Messenger.AddListener(GameEvent.END_SCENE, enablePlayer);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.END_SCENE, enablePlayer);
    }

    public void startCamMove(Transform trans)
    {
        orgPos = player.transform.position;
        bestPos = trans.position;
        bestPos += trans.forward * 7f;
        bestPos.y = orgPos.y;


        enableAllComponents(false);

        player.GetComponent<RotateTowards>().enabled = true;
        player.GetComponent<RotateTowards>().setTarget(trans);

        timer = 0;
        startMove = true;

    }

    void enablePlayer()
    {
        enableAllComponents(true);
        player.GetComponent<RotateTowards>().enabled = false;
    }

    void Update()
    {
        if (startMove)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime * SPEED, 0, 1f);
            player.transform.position = Vector3.Lerp(orgPos, bestPos, timer);

            if (timer == 1f)
            {
                startMove = false;
                Messenger.Invoke(GameEvent.START_SCENE);
            }
        }
    }

    void enableAllComponents(bool state)
    {
        MonoBehaviour[] comps = player.GetComponents<MonoBehaviour>();
        for (int i = 0; i < comps.Length; i++)
        {
            comps[i].enabled = state;
        }
    }
}
