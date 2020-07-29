using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter_RageMan : MonoBehaviour
{
    //GameObjects
    private Transform myTransform;
    private Camera mainCamera;

    //UI
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

    IEnumerator DoShooting()
    {
        while (true)
        {
            if (autoShoot)
            {
                GameObject newBullet = Instantiate(bulletObj, myTransform.position + bulletSpawnOffset,
                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z + calcRotation +90), bulletParent) as GameObject;
                newBullet.GetComponent<Rigidbody2D>().AddForce(shootPosDiff * bulletSpeed);
            }
            yield return new WaitForSeconds(timeBetweenAutoShots);
        }
    }

    private void Awake()
    {
        myTransform = transform;
        mainCamera = Camera.main;
    }
    void Start()
    {
        shootPosDiff = mainCamera.ScreenToWorldPoint(Input.mousePosition) - myTransform.position;
        shootPosDiff.Normalize();
        calcRotation = Mathf.Atan2(shootPosDiff.y, shootPosDiff.x) * Mathf.Rad2Deg;
        StartCoroutine(DoShooting());
    }

    // Update is called once per frame
    void Update()
    {
        shootPosDiff = mainCamera.ScreenToWorldPoint(Input.mousePosition) - myTransform.position;
        shootPosDiff.Normalize();
        calcRotation = Mathf.Atan2(shootPosDiff.y, shootPosDiff.x) * Mathf.Rad2Deg;
        Debug.DrawRay(myTransform.position, shootPosDiff, Color.red);

        if(_rageMeter >= fullRageLimit)
        {   //FUll rage phase
            autoShoot = true;
            //timeBetweenAutoShots == 10;



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
}
