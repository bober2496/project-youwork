using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Movement : MonoBehaviour
{
    //Attributumoknak valtozok
    private Transform myTransform;

    //Kezeleshez valtozok
    [SerializeField] private float shipSpeed;

    //Script valtozok


    private void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
