using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class PlayerBeam : MonoBehaviour
{
    //Si le tir est en train de charger 
    private bool isLoading;
    private GameObject lockedTarget;


    //Particle représentant le chargement 
    public ParticleSystem loadParticleVortex;
    //Particle du projectile
    public ParticleSystem loadParticle;

    private ParticleSystem.EmissionModule emitVortex;
    private ParticleSystem.EmissionModule emitLoad;

    public Transform spawnShot;
    public Transform spawnDir;
    public GameObject projectile;
    public float munitionScale;
    public float munitionSpeed;

    public GameObject exploMunition;


    // Start is called before the first frame update
    void Start()
    {
        emitVortex = loadParticleVortex.emission;
        emitLoad = loadParticle.emission;
        emitVortex.rateOverTime = 0;
        var m = loadParticle.main;
        m.startSize = 0;
        exploMunition.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLoading)
        {
            emitVortex.rateOverTime = emitVortex.rateOverTime.constant + 4;
            var m = loadParticle.main;
            m.startSize= m.startSize.constant + 0.1f;
        }
    }

    //Démarre le chargement du tir
    public void LoadShot()
    {
        isLoading = true;   
        loadParticle.Play();
    }

    public void CancelShot()
    {
        isLoading = false;
        emitVortex.rateOverTime = 0;
        var m = loadParticle.main;
        m.startSize = 0;
        loadParticle.Stop();
        loadParticle.SetParticles(null);
    }

    //Tir
    public void ReleaseShot()
    {
        isLoading = false;
        emitVortex.rateOverTime = 0;
        var m = loadParticle.main;
        m.startSize = 0;
        loadParticle.Stop();
        loadParticle.SetParticles(null);
        var tmp = Instantiate(projectile, spawnShot.position, spawnDir.rotation);
        tmp.transform.localScale = new Vector3(munitionScale , munitionScale , munitionScale );
        tmp.GetComponent<Rigidbody>().AddForce((spawnShot.position-spawnDir.position).normalized * munitionSpeed);
        Destroy(tmp, 10f);
    }

    //Pour la détection du rechargement
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("munition"))
        {
            var tmp = Instantiate(exploMunition, other.gameObject.transform.position, Quaternion.identity);
            tmp.transform.localScale = other.gameObject.transform.localScale / 2;
            other.gameObject.SendMessage("Destroy");
        }
    }
}
