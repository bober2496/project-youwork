using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster_script : MonoBehaviour
{
    [SerializeField] float TimeSpeed = 1;
    [SerializeField] int FPS = 100;
      

    // Update is called once per frame
    void FixedUpdate()
    {
        Time.timeScale = TimeSpeed;
        Application.targetFrameRate = FPS;
    }
}
