using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public int level;
    public int chapter;
    public Button[] buttons;
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void openScene()
    {   
        if(!levelName.Equals(""))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            SceneManager.LoadScene("Chapter" + chapter.ToString() + "_" + "Level" + level.ToString());
        }
        
    }

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void UnlockAll()
    {
        PlayerPrefs.SetInt("ReachedIndex", 13);
        PlayerPrefs.SetInt("UnlockedLevel", 13);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
