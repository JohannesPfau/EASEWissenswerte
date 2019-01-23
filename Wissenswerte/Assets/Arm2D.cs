using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm2D : MonoBehaviour {

    public float speed = 1;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    GameObject lastTouched;
    GameObject attached;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float dx = 0;
        float dy = 0;
        if (transform.position.x > minX && Input.GetAxis("Horizontal") < 0)
            dx = Input.GetAxis("Horizontal");
        if (transform.position.x < maxX && Input.GetAxis("Horizontal") > 0)
            dx = Input.GetAxis("Horizontal");
        if (transform.position.y > minY && Input.GetAxis("Vertical") < 0)
            dy = Input.GetAxis("Vertical");
        if (transform.position.y < maxY && Input.GetAxis("Vertical") > 0)
            dy = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(dx, dy, 0) * speed;

        if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!lastTouched)
                return;
            if(!attached)
            {
                attached = lastTouched;
                attached.transform.parent = transform;
            }
            else
            {
                GameObject.Find("TischdeckenLogic").GetComponent<TischdeckenLogic>().place(attached);
                attached.transform.parent = null;
                attached = null;
            }
        }
        //float x = (transform.localRotation.eulerAngles.x + 360) % 360;
        //transform.localRotation = Quaternion.Euler(new Vector3(x + Input.GetAxis("HorizontalRIGHT"), 90, 90));
        transform.Rotate(Vector3.up, Input.GetAxis("HorizontalRIGHT"));
    }

    private void OnTriggerStay(Collider other)
    {
        lastTouched = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!attached && other.gameObject == lastTouched)
            lastTouched = null;
    }
}
