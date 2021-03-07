using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
  public Vector2 velocity;


      [SerializeField] private LayerMask platformsLayerMask;
      private Rigidbody2D rigidbody2d;
      private BoxCollider2D boxCollider2d;
      int healthnum = 2;


  private bool walk, walk_left, walk_right, jump;


    // Start is called before the first frame update
    void Start()
    {
      rigidbody2d = transform.GetComponent<Rigidbody2D>();
      boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     CheckPlayerInput();
    UpdatePlayerPosition();
    }

    void UpdatePlayerPosition(){

      Vector3 pos = transform.localPosition;
      Vector3 scale = transform.localScale;
      if (walk){

        if(walk_left){
          pos.x -=velocity.x * Time.deltaTime;
          scale.x =-1;
        }
        if (walk_right){
          pos.x +=velocity.x *Time.deltaTime;
          scale.x = 1;
        }

      }
      if (IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow)) {
          float jumpVelocity = 100f;
          rigidbody2d.velocity = Vector2.up * jumpVelocity;
      }

      HandleMovement_FullMidAirControl();

      transform.localPosition = pos;
      transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      if (col.gameObject.CompareTag("Zombie") ){
        healthnum -= healthnum;
        Debug.Log(healthnum);

      }
      }




    void CheckPlayerInput(){
      bool input_left = Input.GetKey(KeyCode.LeftArrow);
      bool input_right = Input.GetKey(KeyCode.RightArrow);
      bool input_jump = Input.GetKey(KeyCode.UpArrow);

      walk = input_left || input_right;
      walk_left = input_left && !input_right;
      walk_right = input_right && !input_left;
      jump = input_jump;
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void HandleMovement_FullMidAirControl() {
        float moveSpeed = 40f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        } else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            } else {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }



}
