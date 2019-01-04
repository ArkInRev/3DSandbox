using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    //Input settings
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    //character controller
    public CharacterController player;

    //movement characteristics
    public float playerSpeed;
    private Vector3 movePlayer;
    private Vector3 moveDirection;
    public float jumpForce;
    public float gravityScale;
    public float jumpDrag;

    //world characteristics


    //camera settings
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;


    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);


        //        moveDirection = new Vector3(horizontalMove * playerSpeed, moveDirection.y, verticalMove * playerSpeed);
        moveDirection = new Vector3(0f, moveDirection.y, 0f);



        if (Input.GetAxisRaw("Vertical")>0)
        {

  
            //looks diagonal and does not reset
            //player.transform.LookAt(player.transform.position + movePlayer);
        }
        
        if (player.isGrounded)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                //playerInput.y = jumpForce;
                moveDirection.y = jumpForce;
            }

        }

        camDirection();
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        player.transform.LookAt(player.transform.position + movePlayer);

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        player.Move(movePlayer *playerSpeed* Time.deltaTime);
        player.Move(moveDirection * jumpDrag * Time.deltaTime);
        //        player.transform.LookAt(player.transform.position + moveDirection);        
        //        player.Move(movePlayer * playerSpeed * Time.deltaTime);


        //Debug.Log(player.velocity.magnitude);


    }

    private void FixedUpdate()
    {
        
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;


    }
}
