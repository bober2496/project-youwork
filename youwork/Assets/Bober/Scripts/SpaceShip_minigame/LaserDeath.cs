using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "MapEdge" )
        {            
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Enemy_SpaceShip(Clone)")
        {            
            GameObject.Find("Scoreboard").GetComponent<Scoreboard_SpaceShip_Game>().SpaceShip_Game_Score++;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
