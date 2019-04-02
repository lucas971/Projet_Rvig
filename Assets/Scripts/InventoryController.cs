using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform toFaceCamera;
    private bool _isFacingCamera = false;
    public GameObject inventory;
    public GameObject pink;
    private GameObject blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsFacingCamera(toFaceCamera, Camera.main, _isFacingCamera ? 0.77F : 0.82F) != _isFacingCamera)
        {
            _isFacingCamera = !_isFacingCamera;

            if (_isFacingCamera)
            {
                inventory.SetActive(true);
                if (pink != null)
                {
                    pink.SetActive(true);
                }
                if (blue != null)
                {
                    blue.SetActive(true);
                    
                }
            }
            else
            {
                inventory.SetActive(false);
                if (pink != null)
                {
                    pink.SetActive(false);
                }
                if (blue != null)
                {
                    blue.SetActive(false);
                }
            }
        }
    }

    public static bool GetIsFacingCamera(Transform facingTransform, Camera camera, float minAllowedDotProduct = 0.8F)
    {
        return Vector3.Dot((camera.transform.position - facingTransform.position).normalized, facingTransform.forward) > minAllowedDotProduct;
    }

    private void setPink(GameObject pink)
    {
        this.pink = pink;
    }
    private void removePink()
    {
        this.pink = null;
    }

    private void setBlue(GameObject blue)
    {
        this.blue = blue;
    }
    private void removeBlue()
    {
        this.blue = null;
    }
}
