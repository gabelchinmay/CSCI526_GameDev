using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenLevels : MonoBehaviour
{
   public void ChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
}
