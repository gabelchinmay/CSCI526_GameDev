using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public int level;
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
            SceneManager.LoadScene("Level" + level.ToString());
        }
        
    }

   

    public void UnlockAll()
    {
        PlayerPrefs.SetInt("ReachedIndex", 18);
        PlayerPrefs.SetInt("UnlockedLevel", 18);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
