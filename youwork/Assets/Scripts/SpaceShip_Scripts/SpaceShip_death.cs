using UnityEngine;

public class SpaceShip_death : MonoBehaviour
{
    //Inspector
    [SerializeField] float blowUpTime = 1f;

    //Working variables
    [HideInInspector]public bool _player_SpaceShip_died;

    private void Start()
    {
        _player_SpaceShip_died = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy_SpaceShip(Clone)")
        {
            _player_SpaceShip_died = true;
            Destroy(gameObject, blowUpTime);
        }
    }
}
