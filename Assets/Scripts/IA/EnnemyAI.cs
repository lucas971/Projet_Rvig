using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyAI : MonoBehaviour
{
    private bool quit;
    public Transform player;
    public Transform center;
    private Vector3 axis;
    private float radius;
    public float radiusSpeed;

    void Start()
    {
        axis = Vector3.Cross(center.position - transform.position, Vector3.up).normalized;
        radius = (center.position - transform.position).magnitude;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    void Update()
    {
        transform.RotateAround(center.position, axis, radiusSpeed * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        transform.LookAt(player);
    }


    //A partir d'ici : tir du rayon
    
    private bool isLoading = false;
    private bool isShooting = false;
    public Animator anim;
    public GameObject beamTarget;
    public float speedBeam;
    public LineRenderer beam;
    private Vector3 directionBeam;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Loadshot();
        }
        if(isShooting)
        {
            beamTarget.GetComponent<Rigidbody>().velocity = ((player.transform.position - beamTarget.transform.position).normalized * speedBeam);
        }

        beam.SetPosition(1, beamTarget.transform.localPosition);
    }
    private void Loadshot()
    {
        isLoading = true;
        anim.SetTrigger("Load");
        
    }

    public void LoadFinished()
    {
        isLoading = false;
        isShooting = true;
    }
    
    private void OnApplicationQuit()
    {
        quit = true;
    }

    
    private void OnDestroy()
    {
        if (!quit)
            transform.parent.gameObject.SendMessage("CheckEnnemies", SendMessageOptions.RequireReceiver);
    }
}
