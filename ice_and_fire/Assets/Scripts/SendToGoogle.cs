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
    public int KillCount = 0;
    public int SwordWaveCount = 0;
    public int KillSwordCount = 0;
    public int ValidSwordAttack = 0;
    public int SwordDamageCount;



    public void Start()
    {
        sessionID = DateTime.Now.Ticks;
        currentSendToGoogle = this;
    }

    public void SetParameters(int chapter, int level)
    {
        
        this.Level = level;
        if (this.Level <3 )
        {
            this.Chapter = 1;
            this.Level = level;
        }
        else if (this.Level > 3 && this.Level <9)
        {
            this.Chapter=2;
            this.Level = level-3;
        }
        else if (this.Level ==9)
        {
            this.Chapter = 3;
            this.Level = 0;
        }
        else if (this.Level > 8 && this.Level < 13)
        {
            this.Chapter=3;
            this.Level = level+1;
        }
        else if (this.Level > 12)
        {
            this.Chapter=4;
            this.Level = level -11;
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
        StartCoroutine(Post(sessionID.ToString(), Attempts.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString(), TotalJump.ToString(), ArrowCount.ToString(), ArrowHitsEnemyCount.ToString(), KillCount.ToString(), KillSwordCount.ToString(), ValidSwordAttack.ToString(), SwordWaveCount.ToString()));
        Debug.Log("ArrowHitsEnemyCount: " + ArrowHitsEnemyCount.ToString());
        Debug.Log(KillCount.ToString());
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

    public void ShootArrow()
    {
        ArrowCount++;
    }

    

    public void ValidSwordAttackCount(int count)
    {
        ValidSwordAttack = +count;
    }

    public void killEnemy()
    {
        KillCount++;
        

    }

    public void killSwordEnemy()
    {
        KillSwordCount++;


    }

    public void HitCount()
    {
        ArrowHitsEnemyCount++;
        Debug.Log(ArrowHitsEnemyCount);
    }


    public void SwordWavedCount()
    {
        SwordWaveCount++;
    }

    public IEnumerator Post(string sessionID, string attempts, string Chapter, string Level, string Time, string Jump, string arrowShotted, string validShot, string KillCount, string KillSwordCount, string ValidSwordAttack, string SwordWaveCount)
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
        form.AddField("entry.1963625633", KillCount);
        form.AddField("entry.311655864", KillSwordCount);
        form.AddField("entry.1641108659", ValidSwordAttack);
        form.AddField("entry.2027457291", SwordWaveCount);

        Debug.Log(form.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            Debug.Log(www.result.ToString());
            yield return www.SendWebRequest();

            
            if (www.result != UnityWebRequest.Result.Success)
            {
               
                Debug.LogError(www.error);
            }
            else
            {
                
                Debug.Log("Form upload complete!");

                Count = 0.0f;
                TotalJump = 0;
                Attempts = 1; // 在提交后重置尝试次数

                www.Dispose();
            }
        }
    }
}
