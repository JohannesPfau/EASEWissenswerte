using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhysically : MonoBehaviour {

    public float forceMult = 1f;
    public float speedRotation = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //GetComponent<Rigidbody>().AddForce(forceMult * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0),ForceMode.Acceleration);
            GetComponent<Rigidbody>().velocity = forceMult * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        if(Input.GetAxis("HorizontalRIGHT") != 0)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.Rotate(-Vector3.forward, Input.GetAxis("HorizontalRIGHT") * speedRotation);
            //transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);
        }
	}
}
