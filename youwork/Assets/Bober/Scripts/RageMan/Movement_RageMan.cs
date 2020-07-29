using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Movement_RageMan : MonoBehaviour
{
    //GameObject references
    private Transform myTransform;
    private Rigidbody2D myRB;

    //UI
    public float _RageMan_MovementSpeed = 3;

    //Working variables
    private Vector2 lookDirection;
    private float horizontalMove, verticalMove;
        
    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        myRB = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        lookDirection = new Vector2(0, 1);
    }        

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (horizontalMove != 0 || verticalMove != 0)
        {
            Vector3 movementDirection = new Vector3(horizontalMove, verticalMove, 0f);
            movementDirection.Normalize();
            movementDirection *= (_RageMan_MovementSpeed * Time.fixedDeltaTime);
            myTransform.Translate(movementDirection);
            if(!(horizontalMove == 0 && verticalMove == 0))
            lookDirection = new Vector2(horizontalMove, verticalMove);
        }
    }
}
