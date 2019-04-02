using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
public class Thermonjoncteur : MonoBehaviour
{
    public Transform player;
    public Transform attractor;
    public float attractionDistance;
    public GameObject inventory;
    public Anchor inventoryAnchor;
    public Anchor spaceshipAnchor;
    private bool anchored;
    private InteractionBehaviour _intObj;
    private Vector3 baseScale;
    private AnchorableBehaviour behaviour;
    
    void Start()
    {
        _intObj = GetComponent<InteractionBehaviour>();
        behaviour = GetComponent<AnchorableBehaviour>();
        baseScale = transform.localScale;
        
        behaviour.OnDetachedFromAnchor = new System.Action(() =>
        {
            inventory.SendMessage("removeBlue");
            transform.localScale = baseScale;
        });
        behaviour.OnAttachedToAnchor = new System.Action(() =>
        {
            if (behaviour.anchor == inventoryAnchor)
            {
                inventory.SendMessage("setBlue", this.gameObject);
                transform.localScale = baseScale * .5f;
            }

            if (behaviour.anchor == spaceshipAnchor)
            {
                transform.localScale = baseScale * .5f;
            }

        });
    }

    private void Update()
    {
        if (!behaviour.isAttached && !_intObj.isGrasped && (-transform.position + attractor.position).magnitude < attractionDistance)
        {
            transform.position += (-transform.position + attractor.position) * Time.deltaTime;
        }

        else if (!behaviour.isAttached && !_intObj.isGrasped)
        {
            transform.position += (-transform.position + player.position) * Time.deltaTime;
        }
    }
}
