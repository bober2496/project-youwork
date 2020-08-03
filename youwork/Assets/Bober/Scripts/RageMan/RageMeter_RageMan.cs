using System.Collections;
using UnityEngine;

public class RageMeter_RageMan : MonoBehaviour
{
    //GameObjects
    private Camera mainCamera;

    //Inspector
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private int fullRageLimit, frustratedLimit, depressionLimit;
    [SerializeField] private float bulletSpeed, timeBetweenAutoShots;
    [SerializeField] private Vector3 bulletSpawnOffset;

    //Working variables
    public int _rageMeter;
    public int _rageType;
    private bool autoShoot;
    private float calcRotation;
    private Vector2 shootPosDiff;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    void Start()
    {
        CalculateForShooting();
        StartCoroutine(DoShooting());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateForShooting();

        if(_rageMeter >= fullRageLimit)
        {   //FUll rage phase
            autoShoot = true;
            //timeBetweenAutoShots == 0.7f;

        }
        else if(_rageMeter >= frustratedLimit)
        {   //Frustrated phase

        }
        else if(_rageMeter > depressionLimit)
        {   //Depression phase

        }
        else
        {   //Calm phase

        }
    }

    private void CalculateForShooting()
    {
        shootPosDiff = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        shootPosDiff.Normalize();
        calcRotation = Mathf.Atan2(shootPosDiff.y, shootPosDiff.x) * Mathf.Rad2Deg;
    }

    IEnumerator DoShooting()
    {
        while (true)
        {
            if (autoShoot)
            {
                GameObject newBullet = Instantiate(bulletObj, transform.position + bulletSpawnOffset,
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + calcRotation + 90), bulletParent) as GameObject;
                newBullet.GetComponent<Rigidbody2D>().AddForce(shootPosDiff * bulletSpeed * 100);
            }
            yield return new WaitForSeconds(timeBetweenAutoShots);
        }
    }
}
