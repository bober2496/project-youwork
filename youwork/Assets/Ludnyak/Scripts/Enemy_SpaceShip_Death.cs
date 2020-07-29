using UnityEngine;

public class Enemy_SpaceShip_Death : MonoBehaviour
{
    [SerializeField] private bool canDie;
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.name == "MapEdge" && canDie)
        {
            GameObject.Find("Text").GetComponent<Scoreboard_SpaceShip_Game>().SpaceShip_Game_Score--;
            Destroy(gameObject);
        }
            
        
    }
}
