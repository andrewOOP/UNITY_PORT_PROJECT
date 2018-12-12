using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    // The target marker.
    public Transform target;

    // Angular speed in radians per sec.
    public float speed = 1;

    // Update is called once per frame
    void Update () {

        if (target != null)
        {

            Vector3 targetDir = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

            // Move our position a step closer to the target.
            Quaternion newRot = Quaternion.LookRotation(newDir);
            Vector3 newRotEuler = new Vector3(0, newRot.eulerAngles.y, 0);

            transform.eulerAngles = newRotEuler;
        }
    }

    public void setTarget(Transform trans)
    {
        target = trans;
    }
}
