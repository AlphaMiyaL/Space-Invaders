using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private bool hit = false;

    public bool getHit() {
        return hit;
    }

    public void Hit() {
        hit = true;
    }
}
