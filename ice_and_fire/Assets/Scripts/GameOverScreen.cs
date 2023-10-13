using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    

    public void SetUp()
    {
        gameObject.SetActive(true);
    }

    public void Replay()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerAttempted();

    }

    public void Exit()
    {

        
        SceneManager.LoadScene(0);

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();

    }

    public void MainMenu()
    {

        
        SceneManager.LoadScene(0);

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();
    }

    public void ChooseLevel()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ChooseLevel");
        asyncLoad.completed += OnSceneLoaded;

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();


    }

    private void OnSceneLoaded(AsyncOperation asyncLoad)
    {

        Debug.Log("Scene loaded!");
    }
}
