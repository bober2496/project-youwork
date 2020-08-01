using UnityEngine;

public class SpaceShipController_script : MonoBehaviour
{
    //Attributumoknak valtozok
    private Transform myTransform;

    //Kezeleshez valtozok
    [SerializeField] private float shipSpeed, moveRadius, laserSpeed;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform laserContainer;

    //Script valtozok
    private float xInput, angle = 0f;

    private void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        if (!GetComponent<SpaceShip_death>().player_SpaceShip_died)
        {
            xInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {               
                Vector3 lazorPosition = myTransform.position - myTransform.position.normalized * 1.4f;
                GameObject newlazor = Instantiate(laser, lazorPosition, myTransform.rotation, laserContainer) as GameObject;
                newlazor.GetComponent<Rigidbody2D>().AddForce(-lazorPosition / 1.4f * laserSpeed * 100);
            }
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
