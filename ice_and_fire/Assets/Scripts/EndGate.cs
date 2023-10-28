using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGate : MonoBehaviour
{
    public string nextLevel;
    [SerializeField] Animator transitionAnime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Load the next level when the player enters the end gate
            UnlockNewLevel();
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
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
            transitionAnime.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(nextLevel);
            transitionAnime.SetTrigger("Start");
        }

        else
        {
            transitionAnime.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(currentSceneIndex + 1);
            transitionAnime.SetTrigger("Start");
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

        if(SceneManager.GetActiveScene().buildIndex >= 10)
        {
            PlayerPrefs.SetInt("ReachedIndex", 13);
            PlayerPrefs.SetInt("UnlockedLevel", 13);
            PlayerPrefs.Save();
        }

    }

}
