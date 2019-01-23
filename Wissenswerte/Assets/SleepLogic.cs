using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepLogic : MonoBehaviour {
    bool changing;
    public GameObject PreLearned;
    public GameObject PostLearned;
    public GameObject learnedText;
    public GameObject LearnedMemory;

    private void Start()
    {
        if(PlayerPrefs.GetString("Learned") != "")
        {
            PreLearned.SetActive(true);
            PostLearned.SetActive(true);
            learnedText.SetActive(true);
            PostLearned.GetComponent<RawImage>().texture = LoadPNG(PlayerPrefs.GetString("Learned"));
            PreLearned.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("LearnedType"));
            PlayerPrefs.SetString("Learned", "");
            PlayerPrefs.SetString("LearnedType", "");
        }
        Invoke("showMemory", 10);
    }

    // Update is called once per frame
    void Update () {
		if(!changing && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return)))
        {
            changing = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("Proceed");
            Invoke("changeScene",1);
        }
	}

    void changeScene()
    {
        SceneManager.LoadScene("Wissenswerte");
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public void showMemory()
    {
        List<string> memories = new List<string>();
        foreach(string s in Directory.GetFiles("Screenshots\\TD"))
            memories.Add(s);
        if(memories.Count == 0)
            return;

        LearnedMemory.SetActive(true);
        LearnedMemory.GetComponent<RawImage>().texture = LoadPNG(memories[Random.Range(0, memories.Count)]);
        Invoke("showMemory", 10);
    }
}
