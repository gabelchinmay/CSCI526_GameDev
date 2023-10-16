using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
   public void RestartGame()
    {
        SceneManager.LoadScene(0);
        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerAttempted();
        
    }
}
