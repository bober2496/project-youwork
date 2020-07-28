using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float circleSpeed = 1f;
    // Assuming negative Z is towards the camera
    [SerializeField] float circleSize = 0.01f;
    [SerializeField] float circleGrowSpeed = 0.005f;
     Vector3 mypos;
    float spawnTime;
    float timeineed;


    private void Start()
    {
        mypos = transform.position;
        spawnTime = Time.time;
    }

    void Update()
    {
        timeineed = Time.time - spawnTime;
        var xPos = Mathf.Sin(timeineed * circleSpeed) * circleSize;
        var yPos = Mathf.Cos(timeineed *  circleSpeed) * circleSize;
        circleSize += circleGrowSpeed;
        transform.position = new Vector3(-xPos , -yPos , mypos.z)+ mypos;
    }

}
