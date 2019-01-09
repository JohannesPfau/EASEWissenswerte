using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

    public float accMult = 0.15f;
    public float rotMult = 1;
    public float maxWheelieTilt = 5;
    public float maxWheelieTiltBack = 1;
    public float wheelsMult = 5;
    public Transform[] wheels;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0)
        {
            // velocity
            transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * accMult, 0, 0);

            //spin wheels
            foreach (Transform wheel in wheels)
                wheel.localRotation = Quaternion.Euler(wheel.localRotation.eulerAngles + new Vector3(0,0,Input.GetAxis("Horizontal") * wheelsMult));

            // "wheelie" animation effect
            if (Input.GetAxis("Horizontal") > 0 && transform.localRotation.eulerAngles.z < maxWheelieTilt)
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, 0, Input.GetAxis("Horizontal") * rotMult));

        }
        
        //rollover protection
        if (transform.localRotation.eulerAngles.z > maxWheelieTilt && transform.localRotation.eulerAngles.z < 180)
            transform.localRotation = Quaternion.Euler(0, 0, maxWheelieTilt);
        if(transform.localRotation.eulerAngles.z > 180 && transform.localRotation.eulerAngles.z < 360- maxWheelieTiltBack)
            transform.localRotation = Quaternion.Euler(0, 0, 360- maxWheelieTiltBack);
    }
}
