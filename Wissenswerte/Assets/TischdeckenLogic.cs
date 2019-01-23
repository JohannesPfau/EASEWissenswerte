using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TischdeckenLogic : MonoBehaviour {

    float lastInput;
    int progress = 0;
    GameObject selected;
    GameObject[] selectables;
    public GameObject[] dialog1_selectables;
    public List<GameObject> MovableObjects;
    public GameObject fertig_B;
    public GameObject fertig_Dialog;

    private void Start()
    {
        lastInput = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey || Input.GetAxis("VerticalDPAD") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("HorizontalRIGHT") != 0 || Input.GetAxis("VerticalRIGHT") != 0)
            lastInput = Time.time;
        if (Time.time - lastInput > 60)
            SceneManager.LoadScene("SleepMode");

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
            SceneManager.LoadScene("SleepMode");

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)) // "A" button
        {
            switch (progress)
            {
                case 0:
                    progress++;
                    foreach (Animator a in GameObject.Find("Dialog0").GetComponentsInChildren<Animator>())
                        a.SetTrigger("Weiter");
                    //selectables = dialog1_selectables;
                    //selected = selectables[0];
                    break;
            }
        }

        if (fertig_B && (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Escape)))
        {
            GameObject.Find("Canvas").SetActive(false);
            GameObject.Find("2DArm").SetActive(false);
            string str = "Screenshots/TD/TD_" + System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_"+System.DateTime.Now.Hour+"."+ System.DateTime.Now.Minute + "."+ System.DateTime.Now.Second + ".png";
            ScreenCapture.CaptureScreenshot(str);
            PlayerPrefs.SetString("Learned", str);
            PlayerPrefs.SetString("LearnedType", "TD");
            SceneManager.LoadScene("SleepMode"); // change in LearnScene
        }

        //if(Input.GetKeyDown(KeyCode.P))
        //    ScreenCapture.CaptureScreenshot("TD.png");
    }

    public void place(GameObject movableObject)
    {
        if(MovableObjects.Contains(movableObject))
            MovableObjects.Remove(movableObject);
        if (MovableObjects.Count == 0)
            done();
    }

    public void done()
    {
        fertig_B.SetActive(true);
        fertig_Dialog.SetActive(true);
    }
}
