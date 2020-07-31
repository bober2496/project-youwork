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
    }

    void Update()
    {
        timeineed = Time.time - spawnTime;
        float xPos = Mathf.Sin(timeineed * circleSpeed) * circleSize;
        float yPos = Mathf.Cos(timeineed *  circleSpeed) * circleSize;
        circleSize += circleGrowSpeed * Time.deltaTime;
        transform.position = new Vector3(-xPos , -yPos , mypos.z)+ mypos;
    }

}
