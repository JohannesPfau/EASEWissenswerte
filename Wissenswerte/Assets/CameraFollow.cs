using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject toFollow;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3((toFollow.transform.position + offset).x,transform.position.y,transform.position.z);

    }
}
