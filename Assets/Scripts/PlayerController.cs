using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    //Input settings
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    //character controller
    public CharacterController player;

    //player abilities
    public bool canSprint;
    public bool canGlide;
    public bool hasWings;
    public int airJumps;

    //player conditions
    private bool isFalling;
    private bool isGliding;
    private float currentGravityScale;
    private float timeUntilNextFlap;
    private float timeUntilNextJump;
    private bool isJumpInUse;
    private bool isJumpStarted;
    private int airJumpsLeft;
    private float currentJumpForce;

    //movement characteristics
    public float playerSpeed;
    private Vector3 movePlayer;
    private Vector3 moveDirection;
    public float jumpForce;
    public float gravityScale;
    public float jumpDrag;
    private float sprintModifier;
    public float walkModifier;
    public float runModifier;
    public float glidingDescent;
    public float wingFlapFrequency;
    public float jumpFrequency;
    public float wingPower;
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
        currentGravityScale = gravityScale;
        isGliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);


        //        moveDirection = new Vector3(horizontalMove * playerSpeed, moveDirection.y, verticalMove * playerSpeed);





        if (Input.GetAxisRaw("Vertical")>0)
        {

  
            //looks diagonal and does not reset
            //player.transform.LookAt(player.transform.position + movePlayer);
        }
        



        // Move in the update method movement. Trying to move to fixed update to normalize the move. 
        //player.Move(movePlayer *(playerSpeed * sprintModifier)* Time.deltaTime);
        //player.Move(moveDirection * jumpDrag * Time.deltaTime);
     
        
        
        //        player.transform.LookAt(player.transform.position + moveDirection);        
        //        player.Move(movePlayer * playerSpeed * Time.deltaTime);


        //Debug.Log(player.velocity.magnitude);
        //interacting with the environment
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.Log("pressed LMB");
            //if the ray hits
            if (Physics.Raycast(ray, out hit, 100 ))
            {
                //Debug.Log("raycast hit something");
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable!= null)
                {
                    //Debug.Log("You just hit an interactable: " + interactable.name);
                    interactable.Interact(this);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(0f, moveDirection.y, 0f);
        if (Input.GetAxisRaw("Jump") == 0)
        {
            // the jump button is not being held or pressed, reset isJumpInUse to false
            // also ensure that the current jump force isn't incrementing
            isJumpInUse = false;
            isJumpStarted = false;
            currentJumpForce = 0;
        }
        if (player.isGrounded)
        {
            isJumpStarted = false;
            isGliding = false;
            moveDirection.y = 0f;
            timeUntilNextFlap = wingFlapFrequency;
            airJumpsLeft = airJumps;
            if (Input.GetAxis("Jump") != 0)
            {
                //used the jump button
                isJumpInUse = true;
                //playerInput.y = jumpForce;

                // this is the working normal jump line
                // if you don't use the "hold button for more power jumps" then the line below jumps the player. 
                //////////// FIX BELOW IF NOT CONTROLLED JUMP BUTTON PRESSES
                //moveDirection.y = jumpForce;
                isJumpStarted = true;
                //need to increment the jump force until max while the button is held and the jump is started until 
            }

            if ((Input.GetAxis("Sprint") != 0) && canSprint)
            {
                //playerInput.y = jumpForce;
                sprintModifier = runModifier;
            }
            else
            {
                sprintModifier = walkModifier;
            }
        }
        else //player is not grounded
        {
            timeUntilNextFlap -= Time.deltaTime;
            if (moveDirection.y < (Physics.gravity.y - .5)) //if the y movement is .5 (offset) faster than gravity, then the player must be falling when not grounded.
            {
                isFalling = true;
            }
            else
            {
                isFalling = false;
            }
            if (canGlide && isFalling) // can only glide while falling. 
            {
                if (Input.GetAxis("Jump") != 0)
                {
                    /// this is not quite right, we need to identify the gliding state and have constant deceleration. 
                    //currentGravityScale = gravityScale/2; //lower the gravity scale if the jump button is held. 
                    isGliding = true;
                    //the jump button is in use again gliding should not trigger this as it isn;t a vertical jump or flap. 
                    //isJumpInUse = true;


                }
                else
                {
                    //currentGravityScale = gravityScale;
                    isGliding = false;
                }
            }
            else
            {
                //currentGravityScale = gravityScale;
                isGliding = false;
            }
            if (hasWings)
            {
                if ((timeUntilNextFlap <= 0) && (Input.GetAxis("Jump") != 0) && (!isJumpInUse))
                {

                    isJumpInUse = true;
                    isFalling = false;
                    // wing flaps are bursts (different than jumps) so they are sudden full force upward pushes. 
                    moveDirection.y = wingPower;
                    timeUntilNextFlap = wingFlapFrequency;
                }
            }
            if ((airJumpsLeft > 0) && (!isJumpInUse) && (Input.GetAxis("Jump") != 0))
            {
                // if you don't use the "hold button for more power jumps" then the line below jumps the player. 
                //////////// FIX BELOW IF NOT CONTROLLED JUMP BUTTON PRESSES
                //                moveDirection.y = jumpForce;
                isJumpInUse = true;
                airJumpsLeft -= 1;
                isFalling = false;
                isJumpStarted = true;
            }
        }

        // handle the jump/button press amounts. 
        if (isJumpStarted)
        {
            if ((currentJumpForce < jumpForce) && (isJumpInUse))
            {
                currentJumpForce = (jumpForce);
            }
            moveDirection.y = currentJumpForce;
            isJumpStarted = false;
        }


        camDirection();
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        player.transform.LookAt(player.transform.position + movePlayer);

        //handle the movedirection.y based on the player state. When gliding, this is a steady downward pull, otherwise, gravity accellerates. 
        if (isGliding && isFalling)
        {
            moveDirection.y = gravityScale * Physics.gravity.y * glidingDescent;
        }
        else
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * currentGravityScale);
        }

        player.Move(movePlayer * (playerSpeed * sprintModifier) * Time.deltaTime);
        player.Move(moveDirection * jumpDrag * Time.deltaTime);


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
