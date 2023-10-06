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
                // 获取关卡数据或其他参数
                int chapter = 1; // 你可以设置章节的值
                int level = currentSceneIndex ; // 假设关卡等级从1开始

                // 设置 SendToGoogle 的参数
                sendToGoogle.SetParameters(chapter, level);
            Debug.Log("set");

            // 发送数据
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
