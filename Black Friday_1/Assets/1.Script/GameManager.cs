using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Stage[] stages;
    public int curStageIdx = 0;

    private void Awake()
    {
        stages = GetComponentsInChildren<Stage>();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
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
    }


    public void Clear()
    {
        curStageIdx++;
    }
}

