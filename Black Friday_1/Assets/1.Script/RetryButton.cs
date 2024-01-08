using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{

    public GameObject LArmAnimationScript;
    public GameObject LArmHandGrabScript;

    public void OnClickedRePlayBtn()
    {
        LArmAnimationScript.SetActive(false);
        LArmHandGrabScript.SetActive(false);
        SceneManager.LoadScene("SampleScene");

    }
}
