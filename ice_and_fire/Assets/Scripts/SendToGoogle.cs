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

    public static SendToGoogle currentSendToGoogle;

    public void Start()
    {
        sessionID = DateTime.Now.Ticks;
        currentSendToGoogle = this;
    }

    public void SetParameters(int chapter, int level)
    {
        this.Chapter = chapter;
        this.Level = level;
        if (this.Chapter == 1 && this.Level > 3)
        {
            this.Chapter++;
            this.Level = 1;
        }
        else if (this.Chapter == 2 && this.Level > 3)
        {
            this.Chapter++;
            this.Level = 1;
        }
        else if (this.Chapter == 3 && this.Level > 6)
        {
            this.Chapter++;
            this.Level = 1;
        }
        Debug.Log("parameter passed");

        Debug.Log(chapter.ToString() + " " + level.ToString());

        // 当关卡被设置时，重置尝试次数
    }

    public void addJump()
    {
        TotalJump++;
    }

    public void Update()
    {
        Count += Time.deltaTime;
    }

    public void Send()
    {
        StartCoroutine(Post(sessionID.ToString(), Attempts.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString(), TotalJump.ToString()));
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

    public IEnumerator Post(string sessionID, string attempts, string Chapter, string Level, string Time, string Jump)
    {
        Debug.Log("begin to send");
        Debug.Log("Attempts: " + Attempts);
        WWWForm form = new WWWForm();
        form.AddField("entry.779211660", sessionID);
        form.AddField("entry.1494050405", attempts);
        form.AddField("entry.1037631838", Chapter);
        form.AddField("entry.583117995", Level);
        form.AddField("entry.726422087", Time);
        form.AddField("entry.24347338", Jump);

        // 添加玩家尝试次数到表单中
        form.AddField("entry.123456789", Attempts.ToString());

        Debug.Log(form.ToString());

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
