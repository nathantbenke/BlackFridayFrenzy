using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{



    public void OnClickedRePlayBtn()
    {
        // save 0 in CurStageIdx
        PlayerPrefs.SetInt("curStageIdx", 0);
        SceneManager.LoadScene("SampleScene");

    }
}
