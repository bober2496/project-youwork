using UnityEngine;

[System.Serializable]
public class Movement
{
    //Inspector
    public float walkingSpeed, runningSpeed;
    public float speedModifier = 1f;
    public float jumpLength;
    //Working
    [HideInInspector]
    public Vector2 lookDirection = new Vector2(0, 1);
    [HideInInspector]
    public float horizontalInput, verticalInput;

    public Movement()       //Standard
    {

    }

    public Movement(float walking_Speed, float running_Speed, float jump_Length)
    {
        this.walkingSpeed = walking_Speed;
        this.runningSpeed = running_Speed;
        this.jumpLength = jump_Length;
    }
}

public class Movement_RageMan : MonoBehaviour
{
    //GameObject references
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;

    //Inspector
    public Movement playerMovement = new Movement(3f, 4.5f, 1.5f);


    //Base methods
    private float Force2Unit()
    {
        float f2u = (100 * myRB.drag) / 2;        //Convert force to unit with current drag
        return f2u;
    }
    public Vector2 DefaultInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 inputs = new Vector2(x, y);
        return inputs;
    }
    
    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Move
        if (DefaultInput().sqrMagnitude != 0)
        {
            MovePlayer(GetDirection());
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer(GetDirection());
        }
    }

    public Vector2 GetDirection()      //Get the player input and return the facing direction with magnitude of 1
    {
        playerMovement.horizontalInput = Input.GetAxisRaw("Horizontal");
        playerMovement.verticalInput = Input.GetAxisRaw("Vertical");
        if (playerMovement.horizontalInput != 0 || playerMovement.verticalInput != 0)     //Only change for input
        {
            playerMovement.lookDirection = new Vector2(playerMovement.horizontalInput, playerMovement.verticalInput).normalized;
            if (playerMovement.horizontalInput != 0) mySR.flipX = playerMovement.horizontalInput < 0;
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
        Vector2 jumpVector = direction * playerMovement.jumpLength * Force2Unit();
        myRB.AddForce(jumpVector, ForceMode2D.Force);
    }
}