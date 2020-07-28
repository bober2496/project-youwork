using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawn : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField]private GameObject enemyShip;
    
    [SerializeField] private Transform parent;
    private float canSpawn=0;
    [SerializeField] float spawnRate;
    int db;


    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
    }
    private void Start()
    {
        StartCoroutine(Spawnrate());
        
        
    }

    IEnumerator Spawnrate()
    {
        while (true) {
            yield return new WaitForSecondsRealtime(spawnRate);
            canSpawn = 1;
        }
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
       
        
        if (canSpawn==1) {
            if (db<10)
            {
                GameObject newEnemyShip = Instantiate(enemyShip, myTransform.position,
                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z)) as GameObject;


                newEnemyShip.transform.parent = parent;

                canSpawn = 0;
                db++;
            }
            else if (db<20 && db>=10)
            {
                GameObject newEnemyShip = Instantiate(enemyShip, myTransform.position+new Vector3 (-6,-2,0),
                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z)) as GameObject;


                newEnemyShip.transform.parent = parent;

                canSpawn = 0;
                db++;
            }
            else
            {
                GameObject newEnemyShip = Instantiate(enemyShip, myTransform.position + new Vector3(-4, 4, 0),
                                Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z)) as GameObject;


                newEnemyShip.transform.parent = parent;

                canSpawn = 0;
                db++;
            }
                
            
        }
    }
}
