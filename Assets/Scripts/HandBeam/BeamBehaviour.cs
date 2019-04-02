using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour
{
    private GameObject lockedTarget;
    private Rigidbody homingMissile;
    public float missileVelocity;
    public float turn;

    void Start()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < ennemies.Length; i++)
        {
            if (ennemies[i].transform.Find("CibleParent").Find("Cible").gameObject.activeSelf)
            {
                lockedTarget = ennemies[i];
                lockedTarget.transform.Find("CibleParent").gameObject.SendMessage("onLocked");
                break;
            }
        }

        if (lockedTarget == null)
        {
            Destroy(this);
        }

        
        homingMissile = GetComponent<Rigidbody>();

    }
    
    void FixedUpdate()
    {
        if (lockedTarget != null)
        {
            homingMissile.velocity = transform.forward * missileVelocity;

            Quaternion targetRotation = Quaternion.LookRotation(lockedTarget.transform.position - transform.position);

            homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
        }
    }

    public GameObject explosion;
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Target")
        {
            var tmp = Instantiate(explosion, c.gameObject.transform.position, Quaternion.identity);
            Destroy(tmp, 4f);
            Destroy(c.gameObject, .1f);
            Destroy(gameObject);
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        Destroy(gameObject);
        Destroy(this);
        if (lockedTarget.transform.Find("CibleParent").gameObject.activeSelf)
        {
            lockedTarget.transform.Find("CibleParent").gameObject.SendMessage("onUnlocked");
        }
        
    }
}
