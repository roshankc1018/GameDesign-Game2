using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Rigidbody2D rb;
    float jumpSpeed = 20f;
    float dirX, dirY;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool  ground = true;
        if ( Input.GetKey(KeyCode.UpArrow)){
            jumpSpeed = 20f;

          }
        else if ((Input.GetKey(KeyCode.DownArrow))){
          jumpSpeed = -20f;
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(20f, jumpSpeed );
    }

}
