using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageAnimBeam : MonoBehaviour
{
   public void onLoadFinished()
    {
        gameObject.SendMessageUpwards("LoadFinished");
    }
}
