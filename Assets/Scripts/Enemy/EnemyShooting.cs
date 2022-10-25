using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bullet;

    public void Shoot() {
        GameObject.Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
}
