using UnityEngine;

public class SpaceShipController_script : MonoBehaviour
{
    //GameObjects
    private SpaceShip_death deathScript;

    //Inspector
    [SerializeField] private float shipSpeed, moveRadius, laserSpeed;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform laserContainer;

    //Script valtozok
    private float xInput, angle = 0f;

    private void Awake()
    {
        deathScript = GetComponent<SpaceShip_death>();
    }

    void Update()
    {
        if (!deathScript._player_SpaceShip_died)
        {
            xInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {               
                Vector3 lazorPosition = transform.position - transform.position.normalized * 1.4f;
                GameObject newlazor = Instantiate(laser, lazorPosition, transform.rotation, laserContainer) as GameObject;
                newlazor.GetComponent<Rigidbody2D>().AddForce(-lazorPosition / 1.4f * laserSpeed * 100);
            }
        }
    }

    private void FixedUpdate()
    {        
        angle += shipSpeed * Time.fixedDeltaTime * -xInput;
        transform.position = - new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * moveRadius;
        float rotateAngle = Mathf.Atan2(-transform.position.y, -transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle * Mathf.Rad2Deg - 90);        
    }
}
