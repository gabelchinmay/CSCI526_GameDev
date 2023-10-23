using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScqIicwiiIuDsO5TGK6yT5xSUBAgJ27RDnwecMRbEvIcEsNyQ/formResponse";

    public long sessionID;
    public int Chapter = 1;
    public int Level = 1;
    public float Count = 0.0f;
    public int TotalJump = 0;
    public static int Attempts = 1; // 将Attempts变为静态变量
    public int ArrowCount = 0; // 用于记录箭矢数量
    private bool arrowHitsEnemy = false;
    public int ArrowHitsEnemyCount = 0;
    public static SendToGoogle currentSendToGoogle;

    public void Start()
    {
        sessionID = DateTime.Now.Ticks;
        currentSendToGoogle = this;
    }

    public void SetParameters(int chapter, int level)
    {
        // 设置章节和关卡信息
        this.Level = level;
        if (this.Level < 3)
        {
            this.Chapter = 1;
            this.Level = level;
        }
        else if (this.Level > 3 && this.Level < 9)
        {
            this.Chapter = 2;
            this.Level = level - 3;
        }
        else if (this.Level == 9)
        {
            this.Chapter = 3;
            this.Level = 0;
        }
        else if (this.Level > 8 && this.Level < 13)
        {
            this.Chapter = 3;
            this.Level = level + 1;
        }
        else if (this.Level > 12)
        {
            this.Chapter = 4;
            this.Level = level - 11;
        }
        Debug.Log("parameter passed");

        Debug.Log(chapter.ToString() + " " + level.ToString());
    }

    public void addJump()
    {
        TotalJump++;
    }

    public void ShootArrow()
    {
        ArrowCount++;
    }

    public void Update()
    {
        Count += Time.deltaTime;
    }

    public void Send()
    {
        StartCoroutine(Post(sessionID.ToString(), Attempts.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString(), TotalJump.ToString(), ArrowCount.ToString(), ArrowHitsEnemyCount.ToString()));
        Debug.Log("ArrowHitsEnemyCount: " + ArrowHitsEnemyCount.ToString()); 
    }

    public void PlayerAttempted()
    {
        Attempts++;
        Debug.Log("Attempts: " + Attempts);
    }

    public void PlayerPassedLevel()
    {
        // 在玩家通过关卡时重置尝试次数
        Attempts = 1;
    }

    public void HitCount()
    {
        ArrowHitsEnemyCount++;
        Debug.Log(ArrowHitsEnemyCount);
    }

    public IEnumerator Post(string sessionID, string attempts, string Chapter, string Level, string Time, string Jump, string arrowShotted, string validShot)
    {

        
        WWWForm form = new WWWForm();
        form.AddField("entry.779211660", sessionID);
        form.AddField("entry.1494050405", attempts);
        form.AddField("entry.1037631838", Chapter);
        form.AddField("entry.583117995", Level);
        form.AddField("entry.726422087", Time);
        form.AddField("entry.24347338", Jump);
        
        form.AddField("entry.1161303837", arrowShotted);
        form.AddField("entry.1154553542", validShot);



        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            Debug.Log(www.result.ToString());
            yield return www.SendWebRequest();

            Debug.Log("Hi first?");
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Hello???");
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("good");
                Debug.Log("Form upload complete!");

                Count = 0.0f;
                TotalJump = 0;
                Attempts = 1; // 在提交后重置尝试次数

                www.Dispose();
            }
        }
    }
}
