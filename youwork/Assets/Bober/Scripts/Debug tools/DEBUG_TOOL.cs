using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:youwork/Assets/Ludnyak/Scripts/EnemyDeath.cs
public class EnemyDeath : MonoBehaviour
=======
public class DEBUG_TOOL : MonoBehaviour
>>>>>>> master:youwork/Assets/Bober/Scripts/Debug tools/DEBUG_TOOL.cs
{

    public float SetTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);
    }

<<<<<<< HEAD:youwork/Assets/Ludnyak/Scripts/EnemyDeath.cs
    
=======
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = SetTimeScale;
    }
>>>>>>> master:youwork/Assets/Bober/Scripts/Debug tools/DEBUG_TOOL.cs
}
