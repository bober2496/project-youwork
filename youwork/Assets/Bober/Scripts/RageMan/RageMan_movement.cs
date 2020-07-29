using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RageMan_movement : MonoBehaviour
{
    //GameObject references
    private Transform myTransform;
    private Rigidbody2D myRB;

    //UI
    public float _RageMan_MovementSpeed = 3;

    //Working variables
    private Vector2 lookDirection;
        
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            myTransform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f));
            lookDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }        
        if(Input.GetKeyDown(KeyCode.Space)) myRB.
    }
}
