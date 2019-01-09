using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBotArm : MonoBehaviour {

    public float forceMult = 1;
    public Rigidbody[] relRigidbodies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = Input.GetAxis("HorizontalRIGHT");
        float y = Input.GetAxis("VerticalRIGHT");

        if(x == 0 && y == 0) // if not needed: idle arms
        {
            foreach(Rigidbody r in relRigidbodies)
                r.useGravity = true;
            return;
        }
        foreach (Rigidbody r in relRigidbodies)
            r.useGravity = false;
        
        // move Tool in desired direction
        foreach (Rigidbody r in relRigidbodies)
        {
            r.AddForce(x * forceMult, y * forceMult, 0, ForceMode.Acceleration);
        }
    }
}
