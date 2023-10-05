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
<<<<<<< HEAD
        SceneManager.LoadScene("ChoosingLevel");
=======
        SceneManager.LoadScene("ChooseLevel");
>>>>>>> 7496ea6d6ecfa40f44891ed20b42823fdb680644
    }
}
