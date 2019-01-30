using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm2D : MonoBehaviour {

    public float speed = 1;
    public float speedRotation = 1f;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float minXforSlicing = 0;
    GameObject lastTouched;
    GameObject attached;
    public bool pauseRB;
    public float overrideSize = -1f;

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
                if (pauseRB)
                {
                    attached.GetComponent<Rigidbody>().useGravity = false;
                    attached.GetComponent<Rigidbody>().isKinematic = true;
                    attached.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                GameObject.Find("TischdeckenLogic").GetComponent<TischdeckenLogic>().place(attached);
                if (pauseRB)
                {
                    attached.GetComponent<Rigidbody>().useGravity = true;
                    attached.GetComponent<Rigidbody>().isKinematic = false;
                    attached.GetComponent<Collider>().enabled = true;
                }
                attached.transform.parent = null;

                if (overrideSize != -1)
                    attached.transform.localScale = new Vector3(overrideSize, overrideSize, overrideSize);

                if (attached.GetComponent<Sliceable>() && transform.position.x >= minXforSlicing)
                {
                    for(int i = 0; i < attached.GetComponent<Sliceable>().nrOfParts; i++)
                    {
                        Instantiate(attached.GetComponent<Sliceable>().slicePrefab, attached.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0), Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
                    }
                    Destroy(attached);
                }

                attached = null;
            }
        }
        //float x = (transform.localRotation.eulerAngles.x + 360) % 360;
        //transform.localRotation = Quaternion.Euler(new Vector3(x + Input.GetAxis("HorizontalRIGHT"), 90, 90));
        transform.Rotate(Vector3.up, Input.GetAxis("HorizontalRIGHT") * speedRotation);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90f, 90f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "NonGrabbable")
            lastTouched = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!attached && other.gameObject == lastTouched)
            lastTouched = null;
    }
}
