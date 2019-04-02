using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceDestination : MonoBehaviour
{
    public TextMeshProUGUI text;
    private GameObject player;
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);
        RaycastHit hit;
        Debug.DrawRay(destination.transform.position, player.transform.position - destination.transform.position);
        if (Physics.Raycast(destination.transform.position,player.transform.position - destination.transform.position,out hit,Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Destination"))
            {
                text.text = hit.distance.ToString("0.00") + "m";
            }
            //else text.text = "";
        }
    }
}
