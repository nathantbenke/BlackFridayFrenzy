using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FadeToBlack () {
        image.gameObject.SetActive (true);
        image.canvasRenderer.SetAlpha(0.0001f);
        image.CrossFadeAlpha(1f, 5f, true);
    }

    public void UnfadeFromBlack()
    {
        image.CrossFadeAlpha(5f, 0f, true);
        image.gameObject.SetActive(true) ;
    }



}
