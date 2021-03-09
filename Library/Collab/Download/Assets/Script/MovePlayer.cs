using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MovePlayer : MonoBehaviour

{
    public Vector2 velocity;

    private Animator anim;
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    public static float healthAmount;

    private bool walk, walk_left, walk_right, jump;
    float moveSpeed = 40f;
    float boostTimer = 0;
    float healthUp = 0.1f;
    float bullets = 0;
    bool weaponized = false;
    bool boosting = false;

    bool keypressed = true;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        healthAmount = 0.2f;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckPlayerInput();
        UpdatePlayerPosition();
        GameEnd();

        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 5)
            {
                moveSpeed = 40f;
                boostTimer = 0;
                boosting = false;
            }
        }
        
        if (weaponized)
        {
            
            if(bullets == 0)
            {
                weaponized = false;
            }
        }
    }

    public void GetAudioClip(string clip)
    {
        audioSource.clip = Resources.Load<AudioClip>(clip);
        audioSource.Play();
    }
    public void GameEnd()
    {
        if (healthAmount <= 0)
        {



            keypressed = false;
            StartCoroutine(ChangeToScene("GameOver"));
        }
    }


    public IEnumerator ChangeToScene(string sceneToChangeTo)
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(sceneToChangeTo);
    }

    void UpdatePlayerPosition()
    {

        Vector3 pos = transform.localPosition;

        Vector3 scale = transform.localScale;
        if (keypressed)
        {
            if (walk)
            {

                if (walk_left)
                {
                    pos.x -= velocity.x * Time.deltaTime;
                    scale.x = Math.Abs(scale.x) * -1;
                }
                if (walk_right)
                {
                    pos.x += velocity.x * Time.deltaTime;
                    scale.x = Math.Abs(scale.x);
                }

            }
            if (IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
            {
                float jumpVelocity = 100f;
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
            }


            HandleMovement_FullMidAirControl();
        }
        transform.localPosition = pos;
        transform.localScale = scale;
    }

    void CheckAnimation()
    {
        if (keypressed)
        {
            if (walk)
            {
                anim.Play("Run");
                GetAudioClip("Running");
            }
            else if (jump)
            {
                anim.Play("Jump");
                GetAudioClip("Jump2");
            }

            else
            {
                anim.Play("Idle");
            }
        }
        if (healthAmount <= 0)
        {
            anim.Play("Death");
            GetAudioClip("Death");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Zombie") && keypressed)
        {
            healthAmount = healthAmount - 0.1f;
            Debug.Log(healthAmount);
            GetAudioClip("Speed Up");
        }
    }




    void CheckPlayerInput()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_jump = Input.GetKey(KeyCode.UpArrow);

        walk = input_left || input_right;
        walk_left = input_left && !input_right;
        walk_right = input_right && !input_left;
        jump = input_jump;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedBoost")
        {
            boosting = true;
            moveSpeed = 80f;
            Destroy(other.gameObject);
        }

        if (other.tag == "Health")
        {
            Destroy(other.gameObject);
            healthAmount = healthAmount + healthUp;
        }

        if(other.tag == "Gun")
        {
            weaponized = true;
            bullets = 1;
            Destroy(other.gameObject);
            
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);

        return raycastHit2d.collider != null;
    }


    private void HandleMovement_FullMidAirControl()
    {


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            }
            else
            {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }



}
