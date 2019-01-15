using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

    public bool isMovable = false;
    public float accMult = 0.15f;
    public float rotMult = 1;
    public float maxWheelieTilt = 5;
    public float maxWheelieTiltBack = 1;
    public float wheelsMult = 5;
    public Transform[] wheels;
    float stretched = 0;
    public GameObject head;
    public GameObject body;
    public GameObject upperConnector;
    public GameObject lowerConnector;
    public GameObject rightArm;
    public GameObject leftArm;
    public float stretchFactor = 2;
	
	// Update is called once per frame
	void Update () {
        if (!isMovable)
            return;

        // MOVEMENT
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

        // STRETCHING
        if(Input.GetAxis("VerticalDPAD") < 0 && stretched <= 1)
        {
            stretched = Mathf.Min(stretched+Time.deltaTime,1);
            head.transform.localPosition = new Vector3(0, stretched*stretchFactor*2, 0);
            body.transform.localPosition = new Vector3(0, stretched * stretchFactor, 0);
            upperConnector.transform.localScale = new Vector3(0.4f, 1 + (stretched * stretchFactor/2 * 4), 0.1f);
            lowerConnector.transform.localScale = new Vector3(0.5f, 1 + (stretched * stretchFactor/2 * 3), 0.1f);
            upperConnector.transform.localPosition = new Vector3(0, 0.779f+(stretched * stretchFactor), 0.0023f);
            rightArm.GetComponent<HingeJoint>().anchor = new Vector3(-0.5f, stretched * stretchFactor, 0);
            //rightArm.GetComponent<HingeJoint>().connectedAnchor = new Vector3(0.999605f + (stretched * (0.7955037f- 0.999605f)), 0.6637001f + stretched * stretchFactor * 0.83623f, 0.021f);
            leftArm.GetComponent<HingeJoint>().anchor = new Vector3(-0.5f, stretched * stretchFactor, 0);
            //leftArm.GetComponent<HingeJoint>().connectedAnchor = new Vector3(-0.4948413f + (stretched * (-0.9934424f + 0.4948413f)), 0.6882999f + stretched * stretchFactor * 0.83623f, 0.021f);
        }
        if(Input.GetAxis("VerticalDPAD") > 0 && stretched > 0)
        {
            stretched = Mathf.Max(stretched - Time.deltaTime, 0);
            head.transform.localPosition = new Vector3(0, stretched * stretchFactor * 2, 0);
            body.transform.localPosition = new Vector3(0, stretched * stretchFactor, 0);
            upperConnector.transform.localScale = new Vector3(0.4f, 1 + (stretched * stretchFactor / 2 * 4), 0.1f);
            lowerConnector.transform.localScale = new Vector3(0.5f, 1 + (stretched * stretchFactor / 2 * 3), 0.1f);
            upperConnector.transform.localPosition = new Vector3(0, 0.779f + (stretched * stretchFactor), 0.0023f);
            rightArm.GetComponent<HingeJoint>().anchor = new Vector3(-0.5f, stretched * stretchFactor, 0);
            //rightArm.GetComponent<HingeJoint>().connectedAnchor = new Vector3(0.999605f + (stretched * (0.7955037f - 0.999605f)), 0.6637001f + stretched * stretchFactor * 0.83623f, 0.021f);
            leftArm.GetComponent<HingeJoint>().anchor = new Vector3(-0.5f, stretched * stretchFactor, 0);
            //leftArm.GetComponent<HingeJoint>().connectedAnchor = new Vector3(-0.4948413f + (stretched * (-0.9934424f + 0.4948413f)), 0.6882999f + stretched * stretchFactor * 0.83623f, 0.021f);
        }
    }
}
