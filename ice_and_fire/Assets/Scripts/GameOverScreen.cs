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
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChooseLevel()
    {
        //SceneManager.LoadScene("ChooseLevel");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ChooseLevel");
        asyncLoad.completed += OnSceneLoaded;

    }

    private void OnSceneLoaded(AsyncOperation asyncLoad)
    {

        Debug.Log("Scene loaded!");
    }
}
