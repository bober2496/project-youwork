using UnityEngine;
using UnityEngine.UI;

public class Scoreboard_SpaceShip_Game : MonoBehaviour
{
    //GameObject references
    private Text scoreboardText;

    //Working variables
    public int SpaceShip_Game_Score;

    private void Awake()
    {
        scoreboardText = GetComponent<Text>();
    }

    private void Update()
    {
        scoreboardText.text = "Score: " + SpaceShip_Game_Score;
    }

}
