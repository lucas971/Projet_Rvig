using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
public class cameraController : MonoBehaviour
{
    private Leap.Controller controller;
    public float rotationSpeed = 20f;
    private bool actionInProgress = false;
    

    void Start()
    {
        controller = new Controller();
    }
    

    void Update()
    {
        if (!actionInProgress)
        {
            foreach (Hand hand in controller.Frame().Hands)
            {
                if (hand.GrabStrength >.99f)
                {
                    if (hand.PalmPosition.x > 225)
                    {
                        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    }
                    else if (hand.PalmPosition.x < -225)
                    {
                        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
                    }
                    /*
                    else if (hand.PalmPosition.y < 175 )
                    {
                        transform.Rotate(transform.right, rotationSpeed * Time.deltaTime);

                    }

                    else if (hand.PalmPosition.y > 325 )
                    {
                        transform.Rotate(transform.right, -rotationSpeed * Time.deltaTime);
                    }
                    */
                }
            }
        }

        actionInProgress = false;
    }

    void setActionInProgress()
    {
        actionInProgress = true;
    }
}
