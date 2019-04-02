using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
    
public class EndGame : MonoBehaviour
{
    public PostProcessProfile postProcessingBehaviour;
    public int isFading = 0;
    public Vignette v;
    void FadeOut()
    {
       isFading = 1;
    }
    void FadeIn()
    {
        if (postProcessingBehaviour.TryGetSettings<Vignette>(out v))
        {
            isFading = -5;
        }
    }

    void Update()
    {
        if(isFading != 0)
        {
            v.intensity.value += isFading * 0.01f;
            
            if(v.intensity.value >= 1 || v.intensity.value <= 0)
            {
                isFading = 0;
            }
        }
    }

    void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
