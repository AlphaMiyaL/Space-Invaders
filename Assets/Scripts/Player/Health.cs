using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    private bool hit = false;
    private float currentTime;
    [HideInInspector]public bool inv { get; set; } = true;

    private void Start() {
        currentTime = Time.time;
    }

    public bool getHit() {
        return hit;
    }

    public void Hit() {
        if (!inv) {
            hit = true;
        }
    }
    void Update()
    {
        if (hit) {
            Destroy(this.gameObject);
        }
        Invincible();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        //Enemy touching player
        if (collider.gameObject.layer == 7) {
            Hit();
        }
    }

    void Invincible() {
        if (currentTime+0.25 < Time.time && inv) {
            this.gameObject.GetComponent<SpriteRenderer>().enabled= !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        }
    }
}
