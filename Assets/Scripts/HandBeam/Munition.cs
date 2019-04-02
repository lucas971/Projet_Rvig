using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    private bool isDestroying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying) transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
        if (transform.localScale.x < 0.1) Destroy(gameObject);
    }

    public void Destroy()
    {
        isDestroying = true;
    }
}
