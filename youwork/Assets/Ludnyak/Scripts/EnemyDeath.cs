using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MapEdge"|| collision.gameObject.name=="Laser(Clone)")

            Destroy(gameObject, 1);
    }
}
