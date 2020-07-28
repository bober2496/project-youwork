using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Movement : MonoBehaviour
{
    //Attributumoknak valtozok
    private Transform myTransform;
    private Rigidbody2D myRb;

    //Kezeleshez valtozok
    [SerializeField] private float shipSpeed, moveRadius, laserSpeed;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform laserContainer;

    //Script valtozok
    private float xInput, angle = 0f;

    private void Awake()
    {
        myTransform = transform;
        myRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        Debug.DrawLine(new Vector3(-10, 0, 0), new Vector3(10, 0, 0), Color.white);
        Debug.DrawLine(new Vector3(0, -10, 0), new Vector3(0, 10, 0), Color.white);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 lazorPosition = myTransform.position;
            lazorPosition.Normalize();
            lazorPosition = myTransform.position - lazorPosition * 1.4f;
            GameObject newlazor = Instantiate(laser, lazorPosition, myTransform.rotation, laserContainer);
            newlazor.GetComponent<Rigidbody2D>().AddForce(-lazorPosition/1.4f * laserSpeed);
        }
    }

    private void FixedUpdate()
    {        
        angle += shipSpeed * Time.fixedDeltaTime * -xInput;
        myTransform.position = - new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * moveRadius;
        float rotateAngle = Mathf.Atan2(-myTransform.position.y, -myTransform.position.x);
        myTransform.rotation = Quaternion.Euler(0, 0, rotateAngle * Mathf.Rad2Deg - 90);        
    }
}
