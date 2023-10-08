using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScqIicwiiIuDsO5TGK6yT5xSUBAgJ27RDnwecMRbEvIcEsNyQ/formResponse"; // 请将 Your_Form_ID 替换为实际的 Google 表单 ID

    public long sessionID;
    public int Chapter = 1;
    public int Level = 1;
    public float Count = 0.0f; // time count
    public int TotalJump = 0;

    public static SendToGoogle currentSendToGoogle; // SendToGoogle 

    public void Start()
    {
        // Assign sessionID to identify playtests
        sessionID = DateTime.Now.Ticks;

        // set SendToGoogle
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
        // Assign variables
        StartCoroutine(Post(sessionID.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString(), TotalJump.ToString()));
    }

    public IEnumerator Post(string sessionID, string Chapter, string Level, string Time, string Jump)
    {
        Debug.Log("begin to send");
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.779211660", sessionID);
        form.AddField("entry.1037631838", Chapter);
        form.AddField("entry.583117995", Level);
        form.AddField("entry.726422087", Time);
        form.AddField("entry.24347338", Jump);

        Debug.Log(form.ToString());
        // Send responses and verify result
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

                // 重置时间和跳跃次数
                Count = 0.0f;
                TotalJump = 0;

                // 根据关卡和章节条件增加 Chapter
                www.Dispose();
            }
        }
    }
}
