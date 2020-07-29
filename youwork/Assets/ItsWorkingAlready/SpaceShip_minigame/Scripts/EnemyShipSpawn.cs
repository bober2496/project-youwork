using System.Collections;
using UnityEngine;

public class EnemyShipSpawn : MonoBehaviour
{
    
    //UI
    [SerializeField] float spawnRate;
    [SerializeField]private GameObject enemyShip;

    public int EnemyCount;

    //Working variables
    public bool _canSpawnEnemyShips = true;
    private Vector3 startingPosition;
       
    private void Start()
    {
        StartCoroutine(Spawnrate());
    }

    IEnumerator Spawnrate()
    {
        while (true) 
        {
            if (_canSpawnEnemyShips)
            {
                startingPosition.x = Mathf.Round(Random.Range(-4, 4));
                startingPosition.y = Mathf.Round(Random.Range(-4, 4));
                for(int i = 0; i < 10; i++)
                {
                    GameObject newEnemyShip = Instantiate(enemyShip, startingPosition, Quaternion.identity, transform) as GameObject;
                    EnemyCount++;
                    yield return new WaitForSecondsRealtime(spawnRate);
                }                
            }            
            yield return new WaitForSecondsRealtime(spawnRate);
        }
    }
}
