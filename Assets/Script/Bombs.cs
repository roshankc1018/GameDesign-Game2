
using System;
using UnityEngine;

/*
 * Simple Jump
 * */
public class Bombs : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private AudioSource audioSource;
    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {


    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("hello");
            Destroy(gameObject);
            audioSource.Play();

        }
    }





}
