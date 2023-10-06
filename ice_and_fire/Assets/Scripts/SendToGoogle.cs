using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScqIicwiiIuDsO5TGK6yT5xSUBAgJ27RDnwecMRbEvIcEsNyQ/formResponse"; // �뽫 Your_Form_ID �滻Ϊʵ�ʵ� Google �� ID

    public long sessionID;
    public int Chapter = 1;
    public int Level = 1;
    public float Count = 0.0f; // �� Time ��Ϊ Count

    public static SendToGoogle currentSendToGoogle; // ��ǰ�� SendToGoogle ʵ��

    public void Start()
    {
        // Assign sessionID to identify playtests
        sessionID = DateTime.Now.Ticks;

        // ���õ�ǰ SendToGoogle ʵ��Ϊ�Լ�
        currentSendToGoogle = this;
    }

    public void SetParameters(int chapter, int level)
    {
        this.Chapter = chapter;
        this.Level = level;
        Debug.Log("parameter paased");
        Debug.Log(chapter.ToString()+" "+ level.ToString());
    }

    

    public void Update()
    {
        Count += Time.deltaTime;
    }

    public void Send()
    {
        // Assign variables
        StartCoroutine(Post(sessionID.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString()));
    }

    public IEnumerator Post(string sessionID, string Chapter, string Level, string Time)
    {

        Debug.Log("begin to send");
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.779211660", sessionID);
        form.AddField("entry.1037631838", Chapter);
        form.AddField("entry.583117995", Level);
        form.AddField("entry.726422087", Time);

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
            }
        }
    }

    
}
