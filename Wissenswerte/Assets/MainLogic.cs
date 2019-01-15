using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

    int progress = 0;
    GameObject selected;
    GameObject[] selectables;
    public GameObject[] dialog1_selectables;
    public GameObject[] ControllerButtonIcons;
    public BotController easeBotController;
    bool justScrolled;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.JoystickButton0)) // "A" button
        {
            switch(progress)
            {
                case 0:
                    progress++;
                    foreach (Animator a in GameObject.Find("Dialog0").GetComponentsInChildren<Animator>())
                        a.SetTrigger("Weiter");
                    selectables = dialog1_selectables;
                    selected = selectables[0];
                    ControllerButtonIcons[3].SetActive(true);
                    foreach (Animator a in GameObject.Find("Dialog1").GetComponentsInChildren<Animator>())
                        a.SetTrigger("Start");
                    break;
                case 1:
                    progress++;
                    foreach (Animator a in GameObject.Find("Dialog1").GetComponentsInChildren<Animator>())
                        a.SetTrigger("Weiter");
                    easeBotController.isMovable = true;
                    ControllerButtonIcons[1].SetActive(true);
                    ControllerButtonIcons[2].SetActive(true);
                    ControllerButtonIcons[0].SetActive(false);
                    break;
            }
        }


        if (Input.GetAxis("VerticalDPAD") < 0 && !justScrolled) // "DPAD UP"
        {
            justScrolled = true;
            switch (progress)
            {
                case 1:
                    int index = Array.IndexOf(selectables, selected);
                    updateSelection((index - 1 + selectables.Length) % selectables.Length);
                    break;
            }
        }
        if (Input.GetAxis("VerticalDPAD") > 0 && !justScrolled) // "DPAD DOWN"
        {
            justScrolled = true;
            switch (progress)
            {
                case 1:
                    int index = Array.IndexOf(selectables, selected);
                    updateSelection((index + 1) % selectables.Length);
                    break;
            }
        }
        if (Input.GetAxis("VerticalDPAD") == 0)
            justScrolled = false;
    }

    void updateSelection(int index)
    {
        foreach (GameObject go in selectables)
            go.GetComponent<Text>().color = Color.white;
        selectables[index].GetComponent<Text>().color = new Color(130/255f, 222/255f,245/255f);
        selected = selectables[index];
    }
}
