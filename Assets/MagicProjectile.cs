using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MagicProjectile : MonoBehaviour
{

    public float damage = 10f;
    public float range = 75f;
    public float fireRate = 15f;

    public Playercontroller plyscript;
    public ParticleSystem lightning;
    public Rig playerRig;
    public Animator anim;
    public GameObject lightningHit;

    public Camera cam;

    float nextTimeToFire = 0f;

    void Update()
    {
        if(Input.GetKey("z") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if(Input.GetKeyUp("z"))
        {
            lightning.Stop();
            playerRig.weight = 0;
            anim.SetLayerWeight(3, 0);
        }
    }

    public void Shoot()
    {

        plyscript.LaunchAttack(plyscript.hitboxes[2], 4);
        lightning.Play();
        playerRig.weight = 1;
        anim.SetLayerWeight(3, 1);

        //RaycastHit hit;
        //Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range);



        //lightningHit.transform.position = hit.point;
        
    }

}
