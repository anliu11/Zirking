using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //gun force
    public float shootForce;

    //gun stats
    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    public bool shooting, readyToShoot, reloading;

    //public reference
    public Camera cam;
    public Transform bulletSpawn;

    //bug fixing
    public bool allowInvoke = true;

    //runs when scene starts
    private void Awake()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //myInput();
    }

    public void myInput()
    {
        //check if allowed to hold down fire button
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
        {
            Reload();
        }
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        //Instanciate Bullet
        Transform currentBullet = Instantiate(bullet.transform, bulletSpawn.transform.position, Quaternion.identity);
        currentBullet.rotation = bulletSpawn.transform.rotation;

        /*
        bulletsLeft--;
        bulletsShot++;
        */

        //Invoke resetShot
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

    }

    public void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("Reload Finished", reloadTime);
    }

    public void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }
}
