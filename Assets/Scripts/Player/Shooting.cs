using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour{
    public Transform shootPoint;
    public GameObject bullet;
    public float delayBetweenShots = 2f;

    private float shootInput = 0f;
    private float timeSinceLastShot = 0f;

    void Update()
    {
        shootInput = Input.GetAxisRaw("Shoot");
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time * shootInput > timeSinceLastShot + delayBetweenShots)
        {
            GameObject.Instantiate(bullet, shootPoint.position, transform.rotation);
            timeSinceLastShot = Time.time;
        }
    }

}
