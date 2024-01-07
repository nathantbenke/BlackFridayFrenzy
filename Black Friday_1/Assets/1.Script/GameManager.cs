using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Stage[] stages;
    public int curStageIdx = 0;

    public bool playing; //status
    public float playTime; //playtime
    public float playTimer; //time passed

    public bool firstSet = true;

    private void Awake()
    {
        //Instance = this;
        //stages = GetComponentsInChildren<Stage>();
    }

    private void Start()
    {
        Instance = this;
        stages = GetComponentsInChildren<Stage>();
        StartGame();
    }

    public void StartGame()
    {
        playing = true;
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

       // Player.Instance.purchaseList = stages[curStageIdx].purchaseList;
        //FindObjectOfType<PurchaseListPanel>().SetPurchaseList(stages[curStageIdx].purchaseList);
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

    }
    public void Clear()
    {
        EndGame(true);
        curStageIdx++;
    }
}

