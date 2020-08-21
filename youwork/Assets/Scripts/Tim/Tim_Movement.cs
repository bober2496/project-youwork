using UnityEngine;
using Bober.UnityTools;
using Unity.Mathematics;

public class Tim_Movement : MonoBehaviour
{
    //GameObject references
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    private CapsuleCollider2D myCollider;
    private Tim_Ragemeter ragemeter;
    private Camera mainCamera;

    //Inspector
    public Movement playerMovement = new Movement(3f, 4.5f, 1.5f);
    [SerializeField]
    public PlayerInput defaultPlayerInput;

    //Working variables
    public bool isDashing = false;

    //Enums
    public enum PlayerInput
    {
        None,
        Mouse,
        Keyboard,
        Axis
    }

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        myCollider = GetComponent<CapsuleCollider2D>();
        ragemeter = GetComponent<Tim_Ragemeter>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        //Move
        playerMovement.input = DefaultMovementInput(defaultPlayerInput);
        if(ragemeter.rageLevel != Tim_Ragemeter.RageLevel.Rage && playerMovement.input.sqrMagnitude > (myCollider.size.x * myCollider.size.x) * .85f)
            MovePlayer(GetDirection(playerMovement.input));

        //Jump
        if (ragemeter.rageLevel != Tim_Ragemeter.RageLevel.Rage && Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
            JumpPlayer(GetDirection(playerMovement.input));
            isDashing = false;
        }
    }

    public float Force2Unit()
    {
        float f2u = (100 * myRB.drag) / 2;        //Convert force to unit with current drag
        return f2u;
    }
    public Vector2 DefaultMovementInput(PlayerInput playerInput)    //Return the input vector with the selected input type
    {
        Vector2 inputs = new Vector2();

        switch (playerInput)
        {
            case PlayerInput.Mouse:
                inputs = BobTools.GetMouseWorldPosition2D(mainCamera) - transform.position;
                break;

            case PlayerInput.Keyboard:
                if (Input.anyKey)
                {
                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                        inputs.x = 0;
                    else if (Input.GetKey(KeyCode.D))
                        inputs.x = 1;
                    else if (Input.GetKey(KeyCode.A))
                        inputs.x = -1;
                    else
                        inputs.x = 0;

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
                        inputs.y = 0;
                    else if (Input.GetKey(KeyCode.W))
                        inputs.y = 1;
                    else if (Input.GetKey(KeyCode.S))
                        inputs.y = -1;
                    else
                        inputs.y = 0;
                }
                else
                    inputs = Vector2.zero;
                break;

            case PlayerInput.Axis:
                inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                break;

            case PlayerInput.None:
                inputs = Vector2.zero;
                Debug.Log("Input type set to none.");
                break;
        }

        return inputs;
    }

    public Vector2 GetDirection(Vector2 input, bool doFlip = false)      //Get the player input and return the facing direction with magnitude of 1
    {        
        if (input.sqrMagnitude != 0)     //Only change for input
        {
            playerMovement.lookDirection = input.normalized;
            if (doFlip && input.x != 0) mySR.flipX = input.x < 0;
        }
        return playerMovement.lookDirection;
    }

    public void MovePlayer(Vector2 direction)       //Moving the player in specific direction with its calculated speed
    {
        float speedModifier = playerMovement.speedModifier > 0 ? playerMovement.speedModifier : 0f;
        Vector3 moveVector = direction * speedModifier * Time.smoothDeltaTime
            * ((Input.GetKey(KeyCode.LeftShift) ? playerMovement.runningSpeed : playerMovement.walkingSpeed));
        transform.Translate(moveVector);
    }

    public void JumpPlayer(Vector2 direction)
    {
        myRB.drag = playerMovement.jumpSpeed;
        Vector2 jumpVector = direction * playerMovement.jumpLength * Force2Unit();
        myRB.AddForce(jumpVector, ForceMode2D.Force);
    }
}