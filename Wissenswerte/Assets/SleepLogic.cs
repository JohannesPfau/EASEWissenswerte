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
        // Ensure screenshot dir exists
        if (!Directory.Exists(getDirName()))
        {
            Directory.CreateDirectory(getDirName());
            Directory.CreateDirectory(getDirName() + "/TD");
            Directory.CreateDirectory(getDirName() + "/AR");
            Directory.CreateDirectory(getDirName() + "/KC");
            Directory.CreateDirectory(getDirName() + "/TR");
        }

        if (PlayerPrefs.GetString("Learned") != "")
        {
            PreLearned.SetActive(true);
            PostLearned.SetActive(true);
            learnedText.SetActive(true);


#if UNITY_EDITOR
            PostLearned.GetComponent<RawImage>().texture = LoadPNG(PlayerPrefs.GetString("Learned"));
#else
            PostLearned.GetComponent<RawImage>().texture = LoadPNG("Builds_Data/" + PlayerPrefs.GetString("Learned"));
#endif
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
            tex.LoadImage(fileData);
        }
        return tex;
    }

    public void showMemory()
    {
        List<string> memories = new List<string>();

        foreach (string s in Directory.GetFiles(getDirName() + "/TD"))
            memories.Add(s);
        foreach (string s in Directory.GetFiles(getDirName() + "/AR"))
            memories.Add(s);
        foreach (string s in Directory.GetFiles(getDirName() + "/KC"))
            memories.Add(s);
        foreach (string s in Directory.GetFiles(getDirName() + "/TR"))
            memories.Add(s);
        if (memories.Count == 0)
            return;

        LearnedMemory.SetActive(true);
        LearnedMemory.GetComponent<RawImage>().texture = LoadPNG(memories[Random.Range(0, memories.Count)]);
        Invoke("showMemory", 10);
    }

    public string getDirName()
    {
    #if UNITY_EDITOR
        return "Screenshots";
    #else
        return "Builds_Data/Screenshots";
    #endif
    }
}
