using UnityEngine;

public class LaserDeath : MonoBehaviour
{
    float deathCircle = 2000f;
    private void FixedUpdate()
    {
        if (transform.position.sqrMagnitude > deathCircle * deathCircle)
            Destroy(gameObject);
    }
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
