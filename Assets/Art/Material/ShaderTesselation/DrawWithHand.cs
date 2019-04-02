using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithHand : MonoBehaviour
{
    private Camera _camera;
    public Shader _drawShader;

    private RenderTexture _splatMap;

    private Material _snowMaterial, _drawMaterial;

    public GameObject tip;

    private RaycastHit _hit;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);


        _snowMaterial = Instantiate<Material>(GetComponent<MeshRenderer>().material);
        GetComponent<MeshRenderer>().material = _snowMaterial;
        _splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if ((Physics.Raycast(tip.transform.position, tip.transform.forward, out _hit, Mathf.Infinity)))
            {
                if (_hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("Hit " + _hit.textureCoord.x + " " + _hit.textureCoord.y);
                    _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                    RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    Graphics.Blit(_splatMap, temp);
                    Graphics.Blit(temp, _splatMap, _drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
                }

            }
        }
    }
}