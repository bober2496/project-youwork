using Bober.UnityTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private enum CalculationMode
    {
        Standard,
        Fast,
        Precise
    }

    [SerializeField]
    private int width = 1, height = 1;
    [SerializeField]
    private float cellSize = 1;
    [SerializeField]
    private Vector3 gridBottomLeftCorner = Vector3.zero;
    [SerializeField]
    private Transform followTarget;
    [SerializeField]
    private float stopAtDistance = 0.1f;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float calculatePeriod = 0.6f;
    [SerializeField]
    private CalculationMode calculationMode = CalculationMode.Standard;
    [SerializeField, Tooltip("If there is no path still trying to go closer")]
    private bool scoutingMode = false;
    [SerializeField, Tooltip("Always calculate just the very next step.\nMight cause a weird trace.")]
    private bool stepByStepMode = false;
    [SerializeField, Tooltip("Draw path gizmos")]
    private bool debugMode = false;

    private NavigationGrid grid;
    private Dictionary<LayerMask, int> layers;
    private List<Vector3> path;
    private bool pathFound;

    private void Awake()
    {
        if (stepByStepMode)
            scoutingMode = false;

        layers = new Dictionary<LayerMask, int>
        {
            {8, 1},             //Walkable
            {9, int.MaxValue},  //Not walkable
            {10, 3 }            //Slow
        };
        grid = new NavigationGrid(width, height, cellSize, layers, gridBottomLeftCorner);
        if(debugMode)
            grid.DrawDebug(5, Color.white, 10000f, true);
    }
    private void OnEnable()
    {
        StartCoroutine(FindPathCicle());
    }
    private void OnDisable()
    {
        StopCoroutine(FindPathCicle());
    }

    private void Update()
    {
        if ((followTarget.position - transform.position).sqrMagnitude > (stopAtDistance * stopAtDistance))
        {
            MoveOnPath();
        }
    }

    IEnumerator FindPathCicle()
    {
        while (true)
        {
            if (stepByStepMode)
            {
                path = grid.FindNextStep(transform.position, followTarget.position);
            }
            else
            {
                switch (calculationMode)
                {
                    case CalculationMode.Standard:
                        path = grid.FindPath(transform.position, followTarget.position, out pathFound, true, 1, 1);
                        break;

                    case CalculationMode.Fast:
                        path = grid.FindPath(transform.position, followTarget.position, out pathFound, true, 1, 100);
                        break;
                    case CalculationMode.Precise:
                        path = grid.FindPath(transform.position, followTarget.position, out pathFound, true, 100, 1);
                        break;
                }
                if (!scoutingMode && !pathFound)
                {
                    path = null;
                }
                if (debugMode && path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(path[i], path[i + 1], Color.blue, calculatePeriod * 1.2f);
                    }
                }
            }

            yield return new WaitForSeconds(calculatePeriod);
        }
    }

    private void MoveOnPath()
    {
        if(path != null)
            if(path.Count > 0)
            {
                Vector3 directionVector = (path[0] - transform.position).normalized;
                transform.Translate(directionVector * moveSpeed * Time.deltaTime);
                if ((path[0] - transform.position).sqrMagnitude <= Mathf.Pow(cellSize * 0.15f, 2))
                    path.RemoveAt(0);
            }
    }

}
