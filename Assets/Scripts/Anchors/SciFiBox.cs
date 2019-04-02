using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class SciFiBox: MonoBehaviour
{
    public Transform player;
    public Transform attractor;
    private Rigidbody rb;
    public float attractionDistance;
    private InteractionBehaviour iBehaviour;
    public GameObject inventory;
    private bool anchored;
    public Anchor inventoryAnchor;
    private AnchorableBehaviour behaviour;

    private void Start()
    {
        iBehaviour = GetComponent<InteractionBehaviour>();
        behaviour = GetComponent<AnchorableBehaviour>();
        behaviour.OnDetachedFromAnchor = new System.Action(()=>
        {
            inventory.SendMessage("removePink");
        });
        behaviour.OnAttachedToAnchor = new System.Action(() =>
        {
            if (behaviour.anchor == inventoryAnchor)
            {
                inventory.SendMessage("setPink", this.gameObject);
            }
            
        });
    }

    private void FixedUpdate()
    {
        if (!behaviour.isAttached && !iBehaviour.isGrasped && (transform.position - attractor.position).magnitude < attractionDistance)
        {
            Debug.Log("hello");
            transform.parent.position += (- transform.position + attractor.position) * Time.deltaTime;
        }

        else if (!behaviour.isAttached && !iBehaviour.isGrasped)
        {
            transform.position += (-transform.position + player.position) * Time.deltaTime;
        }
    }
}
