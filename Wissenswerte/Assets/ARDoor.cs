using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDoor : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<BotController>() && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Aufraeumen");
        }
    }
}
