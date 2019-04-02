using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithCollision : MonoBehaviour
{
    private Camera _camera;
    public Shader _drawShader;

    private RenderTexture _splatMap;

    private Material _snowMaterial, _drawMaterial;
    List<ParticleCollisionEvent> collisionEvents;
    private RaycastHit _hit;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        collisionEvents = new List<ParticleCollisionEvent>();

        _snowMaterial = Instantiate<Material>(GetComponent<MeshRenderer>().material);
        GetComponent<MeshRenderer>().material = _snowMaterial;
        _splatMap = new RenderTexture(1024, 1024,0,RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatMap);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit))
            {
                if (_hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("Hit " +_hit.textureCoord.x + " " + _hit.textureCoord.y);
                    _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                    RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    Graphics.Blit(_splatMap, temp);
                    Graphics.Blit(temp, _splatMap, _drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
                }
                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        RaycastHit _hit = new RaycastHit();
        Ray ray = new Ray(collision.contacts[0].point - collision.contacts[0].normal, collision.contacts[0].normal);
        if (Physics.Raycast(ray, out _hit))
        {
           
            _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
            RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatMap, temp);
            Graphics.Blit(temp, _splatMap, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
        
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatMap, ScaleMode.ScaleToFit,false,1);
    }
}
