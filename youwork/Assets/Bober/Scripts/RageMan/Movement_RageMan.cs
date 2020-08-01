using UnityEngine;

public class Movement_RageMan : MonoBehaviour
{
    //GameObject references
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;

    //UI
    [SerializeField] private float baseMovementSpeed = 3f;
    [SerializeField] private float runningMovementSpeed = 5f;
    public float _RageMan_MovementSpeedModifier = 1f;
    [SerializeField] private float jumpLength = 5f;

    //Working variables
    private float f2u;
    private Vector2 lookDirection;
    private float horizontalMove, verticalMove;
        
    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        f2u = (100 * myRB.drag) / 2;        //Force to unit
        lookDirection = new Vector2(0, 1);
    }        

    void Update()
    {
        //Move
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (horizontalMove != 0 || verticalMove != 0)
        {
            lookDirection = new Vector2(horizontalMove, verticalMove);
            float speedModifier = _RageMan_MovementSpeedModifier > 0 ? _RageMan_MovementSpeedModifier : 0f;
            Vector3 actualMovement = lookDirection.normalized * speedModifier * Time.smoothDeltaTime 
                * ((Input.GetKey(KeyCode.LeftShift) ? runningMovementSpeed : baseMovementSpeed));
            transform.Translate(actualMovement);
            if(horizontalMove != 0) mySR.flipX = horizontalMove < 0 ? true : false;
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRB.AddForce(lookDirection.normalized * jumpLength * f2u, ForceMode2D.Force);
        }
    }
}
