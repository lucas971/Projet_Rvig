using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Arme : MonoBehaviour
{
    //Nombre de munition restante
    private int munition = 1000000;
    //Si le tir est en train de charger 
    private bool isLoading;
    //Charge en cours -> ne peut pas dépasser les munition restante
    private int load = 0;

    //Particle représentant le chargement 
    public ParticleSystem loadParticleVortex;
    //Particle du projectile
    public ParticleSystem loadParticle;

    private ParticleSystem.EmissionModule emitVortex;
    private ParticleSystem.EmissionModule emitLoad;

    public Transform spawnShot;
    public Transform spawnDir;
    public GameObject projectile;

    public GameObject exploMunition;
    public ParticleSystem burst;


    // Start is called before the first frame update
    void Start()
    {
        emitVortex = loadParticleVortex.emission;
        emitLoad = loadParticle.emission;
        emitVortex.rateOverTime = 0;
        var m = loadParticle.main;
        m.startSize = 0;
        exploMunition.GetComponent<ParticleSystem>().Stop();
        burst.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            if (isLoading) ReleaseShot();
            else LoadShot();
        }
        if(isLoading)
        {
            if (load >= munition || load >= 150) return;
            load += 1;
            emitVortex.rateOverTime = emitVortex.rateOverTime.constant + 4;
            var m = loadParticle.main;
            m.startSize= m.startSize.constant + 0.1f;

        }
    }

    //Démarre le chargement du tir
    public void LoadShot()
    {
        load = 0;
        isLoading = true;   
        loadParticle.Play();
    }

    //Tir
    public void ReleaseShot()
    {
        if (load == 0)
            return;

        burst.Play();
        isLoading = false;
        emitVortex.rateOverTime = 0;
        var m = loadParticle.main;
        m.startSize = 0;
        loadParticle.Stop();
        loadParticle.SetParticles(null);
        var tmp = Instantiate(projectile, spawnShot.position, Quaternion.identity);
        tmp.transform.localScale = new Vector3(load/10 , load/10 , load/10 );
        tmp.GetComponent<Rigidbody>().AddForce((spawnShot.position-spawnDir.position).normalized *load * 30);
        munition -= load;
        Destroy(tmp, 5f);

    }

    //Pour la détection du rechargement
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("munition"))
        {
            var tmp = Instantiate(exploMunition, other.gameObject.transform.position, Quaternion.identity);
            tmp.transform.localScale = other.gameObject.transform.localScale / 2;
            other.gameObject.SendMessage("Destroy");
            munition += 20;
        }
    }
    
    //Grappin
    public void ShotGrappin()
    {

    }

}
