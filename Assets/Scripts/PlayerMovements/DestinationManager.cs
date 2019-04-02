using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    public GameObject[] destinations;
    public GameObject[] ennemies;
    public GameObject arrow;
    public Material arrowNoEnnemy;
    public Material arrowHasEnnemy;

    public Material circleNoEnnemy;
    public Material circleHasEnnemy;

    private void Start()
    {
        if (ennemies.Length > 0)
        {
            arrow.transform.Find("Flèche").GetComponent<ParticleSystemRenderer>().material = arrowHasEnnemy;
            arrow.transform.Find("Circle").GetComponent<ParticleSystemRenderer>().material = circleHasEnnemy;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                destinations[i].layer = 10;
                destinations[i].tag = "Destination";
                destinations[i].transform.Find("Dock").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                destinations[i].layer = 0;
                destinations[i].tag = "Untagged";
                destinations[i].transform.Find("Dock").gameObject.SetActive(false);
            }
        }
    }

    void CheckEnnemies()
    {
        bool stillHasEnnemies = false;
        for (int i = 0; i < ennemies.Length; i++)
        {
            if (ennemies[i] && ennemies[i].activeSelf)
            {
                stillHasEnnemies = true;
            }
        }

        if (!stillHasEnnemies)
        {
            arrow.transform.Find("Flèche").GetComponent<ParticleSystemRenderer>().material = arrowNoEnnemy;
            arrow.transform.Find("Circle").GetComponent<ParticleSystemRenderer>().material = circleNoEnnemy;
        }
    }
}
