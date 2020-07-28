using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawn : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField]private GameObject enemyShip;
    [SerializeField] private float speed;
    [SerializeField] private Transform parent;


    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            GameObject newEnemyShip = Instantiate(enemyShip, myTransform.position,
                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z )) as GameObject;

          
            newEnemyShip.transform.parent = parent;
        }
    }
}
