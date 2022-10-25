using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierHp : MonoBehaviour
{
    public int hp = 5;

    public void Hit() {
        hp--;
        if (hp <= 0) {
            Destroy(this.gameObject);
        }
    }
}
