using UnityEngine;

public class MainCamera_script : MonoBehaviour
{
    //GameObject references
    private Transform mainCamTransform;
    public Transform _mainCameraFollowing;

    //Inspector
    [SerializeField] private float Smoothness = 0.2f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 0f, 10f);

    private void Awake()
    {
        mainCamTransform = GetComponent<Transform>();
    }
    private void Start()
    {
        _mainCameraFollowing = GameObject.Find("Timothy Torpido").GetComponent<Transform>();
    }

    void Update()
    {
        mainCamTransform.position = Vector3.Lerp(mainCamTransform.position, _mainCameraFollowing.position + cameraOffset, Smoothness);
    }
}
