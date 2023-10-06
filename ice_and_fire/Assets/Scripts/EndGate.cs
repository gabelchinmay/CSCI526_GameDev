using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class EndGate : MonoBehaviour
{

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
                // ��ȡ�ؿ����ݻ���������
                int chapter = 1; // ����������½ڵ�ֵ
                int level = currentSceneIndex ; // ����ؿ��ȼ���1��ʼ

                // ���� SendToGoogle �Ĳ���
                sendToGoogle.SetParameters(chapter, level);
            Debug.Log("set");

            // ��������
            sendToGoogle.Send();
            Debug.Log("sent");
            
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
        
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
