using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int direction = 1;
    public int bulletSpeed = 5;
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate(){
        rb.velocity = new Vector2(0, direction*bulletSpeed);
        
    }
}
