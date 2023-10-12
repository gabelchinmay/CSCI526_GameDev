using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public void Help()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
