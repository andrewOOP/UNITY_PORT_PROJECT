using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    public LayerMask layerMask;
    [SerializeField] InteractController interactController;
    [SerializeField] CameraAnimController animController;

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
            {
                GetComponent<SoundTrigger>().playSound("select");
                interactController.playScene(hit.collider.gameObject.GetComponent<Interactable>().getScript());
                animController.startCamMove(hit.transform);
            }
        }
    }
}
