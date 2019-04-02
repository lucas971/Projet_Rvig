using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject spawn;
    public GameObject munition;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Instantiate(munition, spawn.transform);
        }
    }
}
