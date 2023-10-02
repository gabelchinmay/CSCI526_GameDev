using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
