using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private bool timeActive = true; // Change later to once player passes through entry trigger
    public float timeSeconds = 10;
    public TextMeshProUGUI timer;

    public GameObject LArmAnimationScript;
    public GameObject LArmHandGrabScript;
    // Update is called once per frame
    void Update()
    {
        if (timeActive)
        {
            timeSeconds -= Time.deltaTime;
            timer.text = convertTime();

            //When the time become 0, then "GameOver"
            if(timeSeconds <= 0 )
            {
                LArmAnimationScript.SetActive(false);
                LArmHandGrabScript.SetActive(false);
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void startTimer()
    {
        timeActive = true;
    }

    public void stopTimer()
    {
        timeActive = false;
    }

    public float getTime()
    {
        return timeSeconds;
    }

    public string convertTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(timeSeconds);
        return time.ToString(@"mm\:ss");
    }

}
