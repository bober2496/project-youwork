using System;
using UnityEngine;

public class Enemy_SpaceShip_movement : MonoBehaviour
{
    [SerializeField] float circleSpeed = 1f;
    [SerializeField] float circleSize = 0.5f;
    [SerializeField] float circleGrowSpeed = 0.005f;
     Vector3 mypos;
    float spawnTime;
    float timeineed;

    private void Start()
    {
        mypos = transform.position;
        spawnTime = Time.time;
        bool type = Convert.ToBoolean(UnityEngine.Random.Range(0, 1));
        circleSpeed = type ? 0.8f : 2.2f;
        circleSize = type ? 1.2f : 0.5f;
        circleGrowSpeed = type ? 1f : 0.5f;
    }

    void Update()
    {
        timeineed = Time.time - spawnTime;        
        float xPos = Mathf.Sin(timeineed * circleSpeed) * circleSize;
        float yPos = Mathf.Cos(timeineed *  circleSpeed) * circleSize;
        circleSize += circleGrowSpeed * Time.smoothDeltaTime;
        transform.position = new Vector3(-xPos , -yPos , mypos.z)+ mypos;
    }

}
