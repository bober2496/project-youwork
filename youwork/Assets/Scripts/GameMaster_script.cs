using UnityEngine;

public class GameMaster_script : MonoBehaviour
{
    //Inspector
    [SerializeField] float TimeSpeed = 1;
    [SerializeField] int FPS = 100;
      

    void FixedUpdate()
    {
        Time.timeScale = TimeSpeed;
        Application.targetFrameRate = FPS;
    }
}
