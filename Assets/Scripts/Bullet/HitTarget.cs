using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    private bool hitOnce = false;
    void OnTriggerEnter2D(Collider2D collider) {
        if (!hitOnce) {
            switch (collider.gameObject.layer) {
                case 6: //Player
                    hitOnce = true;
                    collider.gameObject.GetComponent<Health>().Hit();
                    Destroy(this.gameObject);
                    break;
                case 7: //Enemy
                    hitOnce = true;
                    collider.gameObject.GetComponent<EnemyHit>().Hit();
                    Destroy(this.gameObject);
                    break;
                case 8: //Border
                    hitOnce = true;
                    Destroy(this.gameObject);
                    break;
                case 9: //Barrier
                    hitOnce = true;
                    collider.gameObject.GetComponent<BarrierHp>().Hit();
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
