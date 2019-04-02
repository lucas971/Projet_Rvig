using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollider : MonoBehaviour
{
    public GameObject beamExplo;
    // Start is called before the first frame update
    void Start()
    {
        beamExplo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        beamExplo.SetActive(true);
    }


    private void OnCollisionExit(Collision collision)
    {
        beamExplo.SetActive(false);
    }
}
