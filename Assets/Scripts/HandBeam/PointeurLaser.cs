using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointeurLaser : MonoBehaviour
{
    public GameObject tip;
    private LineRenderer line;
    public GameObject lockedObject;
    private float timeBeforeStart;
    public float timer;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        timeBeforeStart = Time.fixedTime + timer;
        if (line)
            line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeStart < Time.fixedTime)
        {
            line.enabled = true;
            if (lockedObject)
            {
                line.SetPosition(0, tip.transform.position);
                line.SetPosition(1, lockedObject.transform.position);
            }
            else
            {
                line.SetPosition(0, tip.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(tip.transform.position, tip.transform.forward, out hit, Mathf.Infinity))
                {
                    line.SetPosition(1, hit.point);
                }
                else line.SetPosition(1, tip.transform.position + tip.transform.forward * 10000);
            }
            lockedObject = null;
        }
    }

    void locked(GameObject locked)
    {
        lockedObject = locked;
    }
}
