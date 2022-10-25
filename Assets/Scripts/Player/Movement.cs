using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    public float speed = 10f;
    
    private float horizontalInput = 0f;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate(){
        rb.velocity = new Vector2(horizontalInput * speed, 0);
    }
}
