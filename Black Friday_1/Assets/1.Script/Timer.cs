using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool timeActive = true; // Change later to once player passes through entry trigger
    public float timeSeconds = 120;
    public TextMeshProUGUI timer;

    // Update is called once per frame
    void Update()
    {
        if (timeActive)
        {
            timeSeconds -= Time.deltaTime;
            timer.text = convertTime();
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
