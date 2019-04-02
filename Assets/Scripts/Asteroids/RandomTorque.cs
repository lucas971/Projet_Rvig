using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTorque : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(Rand.randomV() * rb.mass * 50);
    }
}
