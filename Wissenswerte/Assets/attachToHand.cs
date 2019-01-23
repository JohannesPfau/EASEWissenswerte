using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class attachToHand : MonoBehaviour {
    bool attached;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerStay(Collider other)
    {
        if(!attached && other.GetComponent<MoveBotArm>() && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)))
        {
            attached = true;
            transform.parent = other.transform;

            GameObject.Find("MainLogic").GetComponent<MainLogic>().ControllerButtonIcons[0].GetComponentInChildren<Text>().text = "Platzieren";
        }
        if(attached && other.GetComponent<TableArea>() && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)))
        {
            attached = false;
            transform.parent = other.transform;
            SceneManager.LoadScene("Tischdecken");
        }
    }
}
