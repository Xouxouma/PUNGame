using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public float normalSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float speed;
    public float jumpForce = 60.0f;
    public float gravity = -9.8f;

    private float verticalVelocity;
    private float gravityJump = 14.0f;
    private bool doubleJumpAvailable = true;

    private CharacterController _charController;

    //public PlayerAnimate playerAnimate;

    // Start is called before the first frame update
    void Start()
    {

        //Cursor.lockState = CursorLockMode.Locked;
        speed = normalSpeed;
        // isJumping = false;
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _charController.isGrounded)
        {
            speed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && _charController.isGrounded)
        {
            speed = normalSpeed;
        }



        /*float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        transform.Translate(straffe, 0, translation);
        */
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        Vector3 movement = new Vector3(straffe, 0, translation);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
        if (_charController.isGrounded)
        {
            // state handle
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                //if (speed == normalSpeed)
                    //playerAnimate.state = PlayerAnimate.State.Walk;
                //else playerAnimate.state = PlayerAnimate.State.Run;
            }
            else
            {
                //playerAnimate.state = PlayerAnimate.State.Idle;
            }
            ResetDoubleJump();
            verticalVelocity = -gravityJump * Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                //playerAnimate.state = PlayerAnimate.State.Jump;
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && doubleJumpAvailable)
            {
                //playerAnimate.state = PlayerAnimate.State.DoubleJump;
                doubleJumpAvailable = false;
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity -= gravityJump * Time.deltaTime;
            }
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        _charController.Move(jumpVector * Time.deltaTime);        

    }

    public void ResetDoubleJump()
    {
        doubleJumpAvailable = true;
    }

}
