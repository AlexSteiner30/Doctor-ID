using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun")] 
    public Transform whereToShootFrom;
    public LayerMask toShootOn;
    public float damage;
    public float recoil;
    public float maxShootDistence;
    private bool canShoot;

    [Header("Properties")] 
    public float maxBullets;
    private float currentBullets;
    public float speed;
    
    [Header("Effects")] 
    //public ParticleSystem muzzle;
    
    public AudioSource shootAudio;
    public AudioSource reloadAudio;

    [Header("References")] 
    public PlayerController controller;

    private void Start()
    {
        canShoot = true;
        currentBullets = maxBullets;
    }

    private void Update()
    {
        Inputs();
        ShootCheck();
    }

    void Inputs()
    {
        if (controller.ShootPressed() && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit targetRaycastHit;

        Physics.Raycast(whereToShootFrom.position, transform.forward, out targetRaycastHit, maxShootDistence, toShootOn);
        shootAudio.Play();
        currentBullets--;
        
        try
        {
            targetRaycastHit.transform.gameObject.GetComponent<Enemy>().ShootOn(damage);
        }
        catch
        {
            Debug.LogWarning("No target to shoot on");
        }
    }

    void ShootCheck()
    {
        if (currentBullets > 0)
        {
            canShoot = true;
        }
        
        else
        {
            canShoot = false;
        }
    }
}
