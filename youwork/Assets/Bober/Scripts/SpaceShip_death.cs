using UnityEngine;

public class SpaceShip_death : MonoBehaviour
{
    [SerializeField] float blowUpTime = 1f;
    public bool player_SpaceShip_died;

    private void Start()
    {
        player_SpaceShip_died = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy_SpaceShip(Clone)")
        {
            player_SpaceShip_died = true;
            Destroy(gameObject, blowUpTime);
        }
    }
}
