using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TMP Text component for the timer
    public TMP_Text gameOverText; // Reference to the TMP Text component for the "Game Over" message
    public float countdownDuration = 60.0f; // Set the initial countdown duration in seconds
    private float currentTime;
    private bool isCountingDown = false;

    public PlayerController player;
    public GameOverScreen gameOverScreen;

    private void Start()
    {
        currentTime = countdownDuration;
        UpdateTimerUI();
        HideGameOverText(); // Initially hide the "Game Over" message
        StartCountdown();
    }

    private void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCountingDown = false;
                HandleTimerEnd();
            }
            UpdateTimerUI();
        }
    }

    public void StartCountdown()
    {
        isCountingDown = true;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
        }
    }

    private void HideGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    private void ShowGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    private void HandleTimerEnd()
    {
        //ShowGameOverText(); 
        player.freeze();
        Debug.Log("Game Over!");
        gameOverScreen.SetUp();
    }
}
