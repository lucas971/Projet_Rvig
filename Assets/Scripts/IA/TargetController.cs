using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private bool isAimed;
    public GameObject cible;
    private bool locked;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cible.SetActive(false);
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (!locked)
        {
            if (cible.activeSelf == false && isAimed)
            {
                cible.SetActive(true);
            }
            else if (cible.activeSelf == true && !isAimed)
            {
                cible.SetActive(false);
            }
            isAimed = false;
        }
    }

    private void setIsAimed()
    {
        isAimed = true;
    }

    private void onLocked()
    {
        isAimed = false;
        locked = true;
    }

    private void onUnlocked()
    {
        locked = false;
        isAimed = false;
        cible.SetActive(false);
    }
}
