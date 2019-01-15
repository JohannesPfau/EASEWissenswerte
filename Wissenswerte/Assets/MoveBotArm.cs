using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBotArm : MonoBehaviour {

    public float forceMult = 1;
    public Rigidbody[] relRigidbodies;
    public BotController easeBotController;
	
	// Update is called once per frame
	void Update ()
    {
        if (!easeBotController.isMovable)
            return;

        // physics correction
        transform.localRotation = Quaternion.Euler(0,0,transform.localRotation.eulerAngles.z);

        float x = Input.GetAxis("HorizontalRIGHT");
        float y = Input.GetAxis("VerticalRIGHT");

        if(x == 0 && y == 0) // if not needed: idle arms
        {
            foreach (Rigidbody r in relRigidbodies)
                //    r.useGravity = true;
                //r.isKinematic = true;
                r.velocity = Vector3.zero;
            return;
        }
        foreach (Rigidbody r in relRigidbodies)
        {
            r.useGravity = false;
            //r.isKinematic = false;
        }
        
        // move Tool in desired direction
        foreach (Rigidbody r in relRigidbodies)
        {
            r.AddForce(x * forceMult, y * forceMult, 0, ForceMode.Acceleration);
        }
    }
}
