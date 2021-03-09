
using System;
using UnityEngine;

/*
 * Simple Jump
 * */
public class MoveZombieLeft : MonoBehaviour
{

    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    float moveVelocity = 10f;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    Vector3 zombieScale, zombiePos;

    public int health = 10;
    bool once = false;

    bool istrue;
    bool ground = false;
    bool isLeftZombie = false;

    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        zombieScale = transform.localScale;
        zombiePos = transform.localPosition;
        if (IsCollideInWall() || IsGrounded() && !once)
        {
            turnLeft();
        }
        if (IsCollideInWall() || IsGrounded() && once)
        {
            once = true;
            if (isLeftZombie)
            {
                turnLeft();
            }
            if (!isLeftZombie)
            {
                turnRight();
            }
        }
        transform.localScale = zombieScale;
        transform.localPosition = zombiePos;

    }

    public void damageTaken(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Zombie") && ground)
        {

            if (zombieScale.x > 0)
            {
                //zombiePos.x = (zombiePos.x) -2;
                isLeftZombie = true;
            }
            else
            {
                //zombiePos.x = (zombiePos.x) +2;
                isLeftZombie = false;
            }
        }
    }

    private void turnRight()
    {
        rigidbody2d.velocity = Vector2.right * moveVelocity;
        zombieScale.x = Math.Abs(zombieScale.x);
    }

    private void turnLeft()
    {
        rigidbody2d.velocity = Vector2.left * moveVelocity;
        zombieScale.x = Math.Abs(zombieScale.x) * -1;
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        if (raycastHit2d.collider != null)
            ground = true;
        return raycastHit2d.collider != null;
    }

    private bool IsCollideInWall()
    {

        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, wallLayerMask);
        return raycastHit2d.collider != null;
    }




}
