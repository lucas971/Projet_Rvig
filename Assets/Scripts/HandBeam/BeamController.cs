using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class BeamController : MonoBehaviour
{
    public float errorAngle;
    private GameObject lockedTarget;
    private Leap.Controller controller;
    private float clock;
    private bool isLoading;
    public float loadingTime;
    public Transform aim;
    public GameObject laserAim;
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        clock = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        Hand h = null;
        bool shotStance = false;

        foreach(Hand hand in controller.Frame().Hands)
        {
            if (hand.IsRight)
            {
                h = hand;
            }
        }

        if (h != null)
        {
            shotStance = true;
            foreach(Finger f in h.Fingers)
            {
                if ((!f.IsExtended && f.Type == Finger.FingerType.TYPE_INDEX) || (f.IsExtended && f.Type != Finger.FingerType.TYPE_INDEX))
                    shotStance = false;
            }
        }


        if (shotStance)
        {
            laserAim.SetActive(true);
            SendMessage("setActionInProgress");

            lockedTarget = null;
            float distance = -1;
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Target");

            for (int i = 0; i < ennemies.Length; i++)
            {
                float iDistance = Vector3.Cross(aim.forward, ennemies[i].transform.position - aim.position).magnitude;
                float iArcta = Mathf.Atan2(iDistance, (ennemies[i].transform.position - aim.position).magnitude);
                if ((iDistance < distance || distance == -1) && errorAngle > iArcta * 180 / Mathf.PI)
                {
                    distance = iDistance;
                    lockedTarget = ennemies[i];
                }
            }
            if (lockedTarget && lockedTarget.transform.Find("CibleParent").gameObject.GetComponent<TargetController>())
            {
                lockedTarget.transform.Find("CibleParent").gameObject.SendMessage("setIsAimed");
            }

            if (lockedTarget)
            {
                laserAim.SendMessage("locked", lockedTarget);
            }
            
        }

        else
        {
            laserAim.SetActive(false);
        }
            
        if (shotStance && Time.fixedTime >= clock)
        {
            if (!isLoading)
            {
                isLoading = true;
                gameObject.SendMessage("LoadShot");
                clock = Time.fixedTime + loadingTime;
            }

            else
            {
                isLoading = false;
                gameObject.SendMessage("ReleaseShot");
                clock = Time.fixedTime + loadingTime;
            }
        }

        else if (!shotStance)
        {
            isLoading = false;
            gameObject.SendMessage("CancelShot");
        }
    }
}
