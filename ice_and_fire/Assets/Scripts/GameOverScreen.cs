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
        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerAttempted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }

    public void Exit()
    {

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();
        SceneManager.LoadScene(0);

    }

    public void MainMenu()
    {

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();
        SceneManager.LoadScene(0);
    }

    public void ChooseLevel()
    {
        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();
        sendToGoogle.PlayerPassedLevel();

        //SceneManager.LoadScene("ChooseLevel");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ChooseLevel");
        asyncLoad.completed += OnSceneLoaded;

       
    }

    private void OnSceneLoaded(AsyncOperation asyncLoad)
    {

        Debug.Log("Scene loaded!");
    }
}
