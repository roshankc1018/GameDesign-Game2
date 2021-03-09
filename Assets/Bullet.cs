using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        MoveZombieRight zombie_right = hitInfo.GetComponent<MoveZombieRight>();
        MoveZombieLeft zombie_left = hitInfo.GetComponent<MoveZombieLeft>();

        if(zombie_right != null)
        {
            zombie_right.damageTaken(10);
        }

        if(zombie_left != null)
        {
            zombie_left.damageTaken(10);
        }
        Destroy(gameObject);
    }

    
}
