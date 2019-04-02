using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
public class FingerSelect : MonoBehaviour
{
    private Leap.Controller controller;
    private Vector3 orientation;
    public GameObject attachedDock;
    private int attachedDockId = 0;
    public Transform leftPointer;
    public Transform rightPointer;
    public float selectionTime;
    public GameObject laserAim;
    float clock;
    float loadingTime;
    bool destinationSelected;
    public float speed = 7f;
    private bool handSelection;
    private bool isMoving;

    void Start()
    {
        controller = new Controller();
        clock = Time.fixedTime;
        loadingTime = Time.fixedTime;
        isMoving = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > clock)
        {
            foreach (Hand hand in controller.Frame().Hands)
            {
                if (hand.IsLeft)
                {

                    bool indexExtended = false;
                    bool otherFingerExtended = false;
                    Transform pointer = null;

                    foreach (Finger f in hand.Fingers)
                    {
                        if (f.Type == Finger.FingerType.TYPE_INDEX && f.IsExtended)
                        {
                            if (hand.IsLeft)
                                pointer = leftPointer;
                            else
                                pointer = rightPointer;

                            indexExtended = true;
                        }

                        if (f.Type != Finger.FingerType.TYPE_INDEX && f.IsExtended)
                        {
                            otherFingerExtended = true;
                        }
                    }
                    
                    if (indexExtended && !otherFingerExtended)
                    {
                        SendMessage("setActionInProgress"); 
                        Vector3 fingerDirection = pointer.forward;
                        int layerMask = 1 << 10;
                        RaycastHit hit;

                        if (Time.fixedTime > clock && isMoving)
                            isMoving = false;

                        if (!isMoving)
                        {
                            laserAim.SetActive(true);
                        }

                        if (Physics.Raycast(transform.position, fingerDirection, out hit, layerMask) && hit.transform.tag == "Destination" && !hit.transform.Find("Ennemy"))
                        {
                            laserAim.SendMessage("locked", hit.transform.Find("Dock").gameObject);
                            if (destinationSelected == false)
                            {
                                loadingTime = Time.fixedTime + selectionTime;
                                destinationSelected = true;
                            }
                            else if (loadingTime < Time.fixedTime)
                            {
                                destinationSelected = false;
                                attachedDock.layer = 10;
                                attachedDock = hit.transform.gameObject;
                                hit.transform.gameObject.layer = 0;
                                clock = Time.fixedTime + speed;
                                goTo(int.Parse(hit.transform.name.Substring(Mathf.Max(0, hit.transform.name.Length - 2))));
                            }

                        }
                    }
                    else
                    {
                        laserAim.SetActive(false);
                        loadingTime = Time.fixedTime + selectionTime;
                        destinationSelected = false;
                    }
                }
            }
        }
    }

    void goTo(int destination)
    {
        isMoving = true;
        laserAim.SetActive(false);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(attachedDockId.ToString() + "_" + destination.ToString()), "time", speed));
        attachedDockId = destination;
    }
}
