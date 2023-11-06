using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Diagnostics;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScqIicwiiIuDsO5TGK6yT5xSUBAgJ27RDnwecMRbEvIcEsNyQ/formResponse";

    public long sessionID;
    public int Chapter = 1;
    public int Level = 1;
    public float Count = 0.0f;
    public int TotalJump = 0;
    public static int Attempts = 1;
    public int ArrowCount = 0;
    private bool arrowHitsEnemy = false;
    public int ArrowHitsEnemyCount = 0;
    public static SendToGoogle currentSendToGoogle;
    public int KillCount = 0;
    public int KillSwordCount = 0;
    public int ValidSwordAttack = 0;
    public int SwordWaveCount = 0;
    public int hp = 0;
    public int SwordDamageCount;
    public int egg = 0;
    public float timeForEgg = 0.0f;
    public float eggDoneTime = 0.0f;
    public int wrongColorGate = 0;
    public int missSwordAttack = 0;
    public int missArrowAttack = 0;

    public int icePlatHit = 0;
    public int firePlatHit = 0;


    private System.Diagnostics.Stopwatch timer1; // 第一个计时器
    private System.Diagnostics.Stopwatch timer2; // 第二个计时器
    private bool isTimer1Paused;
    private bool isTimer2Paused;

    public void Start()
    {
        sessionID = DateTime.Now.Ticks;
        currentSendToGoogle = this;
        timer1 = new System.Diagnostics.Stopwatch();
        timer2 = new System.Diagnostics.Stopwatch();
        timer1.Start();
    }

    public void SetParameters(int chapter, int level)
    {
        this.Level = level;
        UnityEngine.Debug.Log("Parameter passed");
        UnityEngine.Debug.Log(chapter.ToString() + " " + level.ToString());
    }

    public void addJump()
    {
        TotalJump++;
        UnityEngine.Debug.Log(timer1.Elapsed.TotalSeconds);
        UnityEngine.Debug.Log(timer2.Elapsed.TotalSeconds);
    }

    public void Update()
    {
        Count += Time.deltaTime;
    }

    public void Send()
    {
        missArrowAttack = SwordWaveCount - ValidSwordAttack;
        missSwordAttack = ArrowCount - ArrowHitsEnemyCount;
        
        StartCoroutine(Post(sessionID.ToString(), Attempts.ToString(), Chapter.ToString(), Level.ToString(), Count.ToString(), TotalJump.ToString(), ArrowCount.ToString(), ArrowHitsEnemyCount.ToString(), KillCount.ToString(), KillSwordCount.ToString(), ValidSwordAttack.ToString(), SwordWaveCount.ToString(), hp.ToString(), eggDoneTime.ToString(), wrongColorGate.ToString(), missSwordAttack.ToString(), missArrowAttack.ToString(), timer1.Elapsed.TotalSeconds.ToString(), timer2.Elapsed.TotalSeconds.ToString(),
            icePlatHit.ToString(), firePlatHit.ToString()));
        UnityEngine.Debug.Log("ArrowHitsEnemyCount: " + ArrowHitsEnemyCount.ToString());
        UnityEngine.Debug.Log(KillCount.ToString());
    }

    public void PlayerAttempted()
    {
        Attempts++;
        UnityEngine.Debug.Log("Attempts: " + Attempts);
    }

    public void hitWrongGate()
    {
        wrongColorGate = wrongColorGate + 1;
    }

    public void PlayerPassedLevel()
    {
        Attempts = 1;
    }

    public void ShootArrow()
    {
        ArrowCount++;
    }

    public void HealthStatus(int hp)
    {
        this.hp += hp;
    }

    public void ValidSwordAttackCount()
    {
        ValidSwordAttack++;
    }

    public void killEnemy()
    {
        KillCount++;
    }

    public void killSwordEnemy()
    {
        KillSwordCount++;
    }

    public void pickUpEgg()
    {
        egg++;
        if (egg == 1)
        {
            timeForEgg = Time.time;
            UnityEngine.Debug.Log(eggDoneTime + " sec");
        }

        if (egg == 3)
        {
            eggDoneTime = Time.time - timeForEgg;
            UnityEngine.Debug.Log(eggDoneTime + " sec");
        }
    }

    public void HitCount()
    {
        ArrowHitsEnemyCount++;
        UnityEngine.Debug.Log(ArrowHitsEnemyCount);
    }

    public void SwordWavedCount()
    {
        SwordWaveCount++;
    }

    public void fireMode()
    {
        timer1.Start();
        timer2.Stop();
    }

    public void iceMode()
    {
        timer2.Start();
        timer1.Stop();
    }

    public void icePlatDamage()
    {
        icePlatHit = icePlatHit + 1;
    }

    public void firePlatDamage()
    {
        firePlatHit = firePlatHit + 10;
    }

    public IEnumerator Post(string sessionID, string attempts, string Chapter, string Level, string Time, string Jump, string arrowShotted, string validShot, string KillCount, string KillSwordCount, string ValidSwordAttack, string SwordWaveCount, string hp, string eggDoneTime, string wrongColorGate,
        string missSwordAttack, string missArrowAttack, string timer1, string timer2, string icePlatHit, string firePlatHit)
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
        form.AddField("entry.226612155", hp);
        form.AddField("entry.212953843", eggDoneTime);
        form.AddField("entry.59142740", wrongColorGate);
        form.AddField("entry.182701653", missSwordAttack);
        form.AddField("entry.936295988", missArrowAttack);
        form.AddField("entry.2106273834", timer1);
        form.AddField("entry.1565283428", timer2);
        form.AddField("entry.1906357093", icePlatHit);
        form.AddField("entry.1641710712", firePlatHit);

        UnityEngine.Debug.Log(form.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            UnityEngine.Debug.Log(www.result.ToString());
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError(www.error);
            }
            else
            {
                UnityEngine.Debug.Log("Form upload complete!");
                Count = 0.0f;
                TotalJump = 0;
                Attempts = 1;
                www.Dispose();
            }
        }
    }
}
