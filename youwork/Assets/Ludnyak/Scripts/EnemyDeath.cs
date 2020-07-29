using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:youwork/Assets/Bober/Scripts/Debug tools/DEBUG_TOOL.cs
public class DEBUG_TOOL : MonoBehaviour
=======
public class EnemyDeath : MonoBehaviour
>>>>>>> origin/ray:youwork/Assets/Ludnyak/Scripts/EnemyDeath.cs
{

    public float SetTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);
    }

<<<<<<< HEAD:youwork/Assets/Bober/Scripts/Debug tools/DEBUG_TOOL.cs
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = SetTimeScale;
    }
=======
    
>>>>>>> origin/ray:youwork/Assets/Ludnyak/Scripts/EnemyDeath.cs
}
