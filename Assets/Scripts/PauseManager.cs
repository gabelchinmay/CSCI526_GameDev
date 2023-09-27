using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    PauseAction action;
    bool paused = false;

    private void Awake()
    {
        action = new PauseAction();
    }

    public void OnEnable()
    {
        action.Enable();
    }

    public void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    void DeterminePause()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame(); 
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
    }
}
