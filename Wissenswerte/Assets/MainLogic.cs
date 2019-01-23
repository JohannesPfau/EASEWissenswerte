using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

    int progress = 0;
    GameObject selected;
    GameObject[] selectables;
    public GameObject[] dialog1_selectables;
    public GameObject[] ControllerButtonIcons;
    public BotController easeBotController;
    bool justScrolled;
    float lastInput;

    private void Start()
    {
        lastInput = Time.time;
    }

    // Update is called once per frame
    void Update () {
        if(Input.anyKey || Input.GetAxis("VerticalDPAD") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("HorizontalRIGHT") != 0 || Input.GetAxis("VerticalRIGHT") != 0)
            lastInput = Time.time;
        if(Time.time - lastInput > 60)
            SceneManager.LoadScene("SleepMode");

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
            SceneManager.LoadScene("SleepMode");

		if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)) // "A" button
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

                    // scenario selection:
                    if(selected == selectables[0]) // TISCHDECKEN
                    {
                        ControllerButtonIcons[0].SetActive(true);
                        ControllerButtonIcons[0].GetComponentInChildren<Text>().text = "Nehmen";

                        displayTextDelayed(GameObject.Find("Dialog2Text0").GetComponent<Text>(), "Starte Programm \"Tischdecken\".\r\nAnzahl = 4 Personen.\r\nErforderlich: \r\n4x Besteck.\r\n");
                        Invoke("Dialog2Text1", 4.5f);
                    }
                    break;
            }
        }


        if ((Input.GetAxis("VerticalDPAD") < 0 || Input.GetKeyDown(KeyCode.UpArrow)) && !justScrolled) // "DPAD UP"
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
        if ((Input.GetAxis("VerticalDPAD") > 0 || Input.GetKeyDown(KeyCode.DownArrow)) && !justScrolled) // "DPAD DOWN"
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

    void displayTextDelayed(Text text, string textToDisplay)
    {
        currentText = text;
        currentStr = textToDisplay;
        Invoke("addToText", textDelayInitial);
    }
    float textDelay = 0.03f;
    float textDelayInitial = 1f;
    Text currentText;
    string currentStr;
    void addToText()
    {
        if (currentStr.Length == 0)
            return;

        string add = currentStr.Substring(0,1);
        currentText.text += add;
        currentStr = currentStr.Substring(1, currentStr.Length - 1);
        float mult = 1;
        if (add == ".")
            mult *= 10;
        if (add == ",")
            mult *= 5;
        if (add == ":")
            mult *= 10;

        Invoke("addToText", mult * textDelay);
    }

    void Dialog2Text1()
    {
        GameObject.Find("Dialog2Text1").GetComponent<Animator>().SetTrigger("Start");
    }
}
