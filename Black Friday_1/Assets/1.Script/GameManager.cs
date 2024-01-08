using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Stage[] stages;
    public int curStageIdx = 0;

    public bool playing; //status
    public float playTime; //playtime
    public float playTimer; //time passed

    public TMP_Text timeText;
    private void Awake()
    {
        Instance = this;
        stages = GetComponentsInChildren<Stage>();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        SetTimeText(playTime);

        curStageIdx = PlayerPrefs.GetInt("curStageIdx", 0);

        playing = true;
        playTimer = 0;
        for (int i = 0; i < stages.Length; i++)
        {
            if (i == curStageIdx)
            {
                stages[i].gameObject.SetActive(true);
            }
            else
            {
                stages[i].gameObject.SetActive(false);
            }

        }

        Player.Instance.purchaseList = stages[curStageIdx].purchaseList;
        FindObjectOfType<PurchaseListPanel>().SetPurchaseList(stages[curStageIdx].purchaseList);
    }
    void SetTimeText(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string playTimeStr = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");

        timeText.text = playTimeStr;
    }
    private void Update()
    {
        if (!playing)
            return;

        //  over time
        if (playTimer >= playTime)
        {
            EndGame(false);
            return;
        }

        playTimer += Time.deltaTime;
        SetTimeText(playTime - playTimer);

    }

    // quit the stages
    public void EndGame(bool result)
    {
        if (!playing)
            return;

        playing = false;

        if (result) // stage clear and go next stage
        {
            curStageIdx++;
            if (curStageIdx >= 3)
            {
                SceneManager.LoadScene("GameClear");
                return;
            }
            PlayerPrefs.SetInt("curStageIdx", curStageIdx);

            FindObjectOfType<FadeCanvas>(true).FadeOut(() =>
            {
                SceneManager.LoadScene("SampleScene");
            });
                

        }
        else
        {
            SceneManager.LoadScene("GameOver");
        } // result- false
    }

}

