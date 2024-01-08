using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Stage[] stages;
    public int curStageIdx = 0;

    public bool playing; //status
    public float playTime; //playtime
    public float playTimer; //time passed
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
    private void Update()
    {
        if (!playing)
            return;

        if (playTimer >= playTime)
        {
            EndGame(false);
            return;
        }

        playTimer += Time.deltaTime;

    }
    public void EndGame(bool result)
    {
        if (!playing)
            return;

        playing = false;

        if (result)
        {
            curStageIdx++;
            if (curStageIdx ==3)
            {
                // 게임 끝 클리어
                return;
            }
            PlayerPrefs.SetInt("curStageIdx", curStageIdx);
            
            SceneManager.LoadScene("SampleScene");
        }
    }

}

