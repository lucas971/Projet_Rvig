using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap.Unity.Interaction;

public class GameManager : MonoBehaviour
{
    public Anchor anchorBlue;
    public Anchor anchorRed;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Target");
            for (int i = 0; i<ennemies.Length; i++)
            {
                Destroy(ennemies[i]);
            }
            StartCoroutine(Fade(1));
            SceneManager.LoadScene(2);
        }

        if (anchorBlue.hasAnchoredObjects && anchorRed.hasAnchoredObjects)
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Target");
            for (int i = 0; i < ennemies.Length; i++)
            {
                Destroy(ennemies[i]);
            }
            StartCoroutine(Fade(1));
            SceneManager.LoadScene(2);
        }
    }


    IEnumerator Fade(float s)
    {
        yield return new WaitForSeconds(s);
    }

}
