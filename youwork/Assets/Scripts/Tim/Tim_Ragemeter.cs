using System.Collections;
using Unity.Collections;
using UnityEngine;

public class Tim_Ragemeter : MonoBehaviour
{
    public enum RageLevel
    {
        Calm,
        Frustrated,
        Rage
    }

    //GameObjects
    private Rigidbody2D myRB;
    private Tim_Movement movementScript;

    Transform bulletParent;

    //Inspector
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private int rageLimit, frustratedLimit, calmLimit;
    [SerializeField] private float bulletSpeed = 1f, timeBetweenAutoShots = 1.5f;
    [SerializeField] private float bulletPushBackForce = 20f, pushBackStopForce = 8f;
    [SerializeField] private Vector3 bulletSpawnOffset;

    //Working variables
    public int _rageMeter;
    [Tooltip("Ez csak állapotkijelzés. Változtasd a ragemeter értékét!")]
    public RageLevel rageLevel;
    //public int _rageType;         //Unused yet
    private bool autoShoot;
    private float calcRotation;
    private Vector2 shootDirection;

    private void Awake()
    {
        bulletParent = new GameObject("Bullet_Parent").transform;
        bulletParent.transform.position = Vector3.zero;
        myRB = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Tim_Movement>();
    }
    private void OnEnable()
    {
        StartCoroutine(DoShooting());
    }
    void Update()
    {
        SetRageLevel();
    }

    private void SetRageLevel()
    {
        if (_rageMeter >= rageLimit)
        {   //Rage phase
            rageLevel = RageLevel.Rage;
            autoShoot = true;
        }
        else if (_rageMeter >= frustratedLimit)
        {   //Frustrated phase
            rageLevel = RageLevel.Frustrated;
            autoShoot = false;
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot();
        }
        else
        {   //Calm phase
            rageLevel = RageLevel.Calm;
            autoShoot = false;

        }
    }
    
    private IEnumerator DoShooting()
    {
        while (true)
        {
            if (autoShoot)
            {
                Shoot();
            }
            yield return new WaitForSeconds(timeBetweenAutoShots);
        }
    }

    private void Shoot()
    {
        CalculateForShooting();
        GameObject newBullet = Instantiate(bulletObject, transform.position + bulletSpawnOffset,
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + calcRotation + 90), bulletParent) as GameObject;
        newBullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * bulletSpeed * 100);
        if(!movementScript.isDashing) PushBackPlayer(-shootDirection);
    }
    private void CalculateForShooting()
    {
        shootDirection = movementScript.GetDirection(movementScript.DefaultMovementInput(movementScript.defaultPlayerInput));
        calcRotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
    }

    private void PushBackPlayer(Vector2 direction)
    {
        Vector2 jumpVector = direction * bulletPushBackForce * ((100 * myRB.drag) / 2);
        myRB.AddForce(jumpVector, ForceMode2D.Force);
    }
}
