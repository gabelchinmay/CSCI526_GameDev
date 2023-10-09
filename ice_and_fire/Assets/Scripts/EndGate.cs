using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGate : MonoBehaviour
{
    public string nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Load the next level when the player enters the end gate
            UnlockNewLevel();
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SendToGoogle sendToGoogle = FindObjectOfType<SendToGoogle>();

        if (sendToGoogle != null)
        {

            int chapter = 1;
            int level = currentSceneIndex;
            sendToGoogle.SetParameters(chapter, level);
            Debug.Log("set");
            sendToGoogle.Send();
            Debug.Log("sent");

        }

        if(!nextLevel.Equals(""))
        {
            SceneManager.LoadScene(nextLevel);
        }

        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
