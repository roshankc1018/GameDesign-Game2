
using System;
using UnityEngine;

/*
 * Simple Jump
 * */
public class MoveBoats : MonoBehaviour
{



    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private GameObject target;

 Vector3 boatPos;


    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

}



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

          Vector2 newVelocity = rigidbody2d.velocity;
          newVelocity.x += Mathf.Sign(newVelocity.x) * 3;
          rigidbody2d.velocity = newVelocity;
          //boatPos = transform.localPosition;
          //rigidbody2d.velocity = Vector2.right * 100f;
          //transform.localPosition = boatPos;

        }
    }
    }
