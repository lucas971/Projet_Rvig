using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ComposantLaser : MonoBehaviour
{
    public Transform target;
    public LineRenderer line;
    public float maxDistance = 1;
    private InteractionBehaviour iBehaviour;
    private AnchorableBehaviour aBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        iBehaviour = GetComponent<InteractionBehaviour>();
        aBehaviour = GetComponent<AnchorableBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        if (!iBehaviour.isGrasped && !aBehaviour.isAttached)
        {
            float distance = Mathf.Abs((target.position - this.transform.position).magnitude);
            if (distance < maxDistance)
            {
                line.SetPosition(1, target.position);
                line.endColor = new Color(0, 0, (255 * distance) / maxDistance, 255);
            }
            else line.SetPosition(1, transform.position);
        }
        
        else line.SetPosition(1, transform.position);
    }
}
