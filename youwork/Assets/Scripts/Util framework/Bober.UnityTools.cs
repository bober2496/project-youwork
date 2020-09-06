using TMPro;
using UnityEditor;
using UnityEngine;

namespace Bober.UnityTools
{
    public static class BobTools
    {
        public static Vector3 MovePoint(Vector3 startPosition, Vector3 endPosition, float startTime, float moveTime = 1)
        {            
            float timeSinceStart = Time.time - startTime;
            float completePercent = timeSinceStart / moveTime;
            Vector3 move = Vector3.Lerp(startPosition, endPosition, completePercent);
            return move;
        }
        public static Vector3 MoveVector(Vector3 startVector, Vector3 endVector, float startTime, float moveTime)
        {
            Vector3 center = (startVector + endVector) / 2f;

            Vector3 centerToStart = startVector - center;
            Vector3 centerToEnd = endVector - center;

            float timeSinceStart = Time.time - startTime;
            float completePercent = timeSinceStart / moveTime;

            Vector3 move = Vector3.Slerp(centerToStart, centerToEnd, completePercent);
            return move;

            /*
             * Example code
            // Animates the position in an arc between sunrise and sunset.

            using UnityEngine;
            using System.Collections;

        public class ExampleClass : MonoBehaviour
                {
                    public Transform sunrise;
                    public Transform sunset;

                    // Time to move from sunrise to sunset position, in seconds.
                    public float journeyTime = 1.0f;

                    // The time at which the animation started.
                    private float startTime;

                    void Start()
                    {
                        // Note the time at the start of the animation.
                        startTime = Time.time;
                    }

                    void Update()
                    {
                        // The center of the arc
                        Vector3 center = (sunrise.position + sunset.position) * 0.5F;

                        // move the center a bit downwards to make the arc vertical
                        center -= new Vector3(0, 1, 0);

                        // Interpolate over the arc relative to center
                        Vector3 riseRelCenter = sunrise.position - center;
                        Vector3 setRelCenter = sunset.position - center;

                        // The fraction of the animation that has happened so far is
                        // equal to the elapsed time divided by the desired time for
                        // the total journey.
                        float fracComplete = (Time.time - startTime) / journeyTime;

                        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
                        transform.position += center;
                    }
                }
            */



    }

        // Create TextMesh in the World
        public static TextMesh CreateTextMesh(string text, Vector3 position = default, Transform parent = null, int fontSize = 50, Color color = default, TextAnchor textAnchor = default, TextAlignment textAlignment = default, int sortingOrder = 3000)
        {
            GameObject gameObject = new GameObject("Created_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, true);
            transform.position = position;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        public static TextMeshPro CreateTextMeshPro(string text, Vector3 position = default, Transform parent = default, int fontSize = 30, Color color = default, TextAlignmentOptions alignment = TextAlignmentOptions.Center, Vector2 boxSize = default, int sortingOrder = 3000)
        {
            GameObject textObject = new GameObject("Created_Text", typeof(TextMeshPro));
            textObject.transform.SetParent(parent, true);
            textObject.transform.position = position;
            if (boxSize == default)
                boxSize = new Vector2(10f, 5f);
            else
                textObject.GetComponent<RectTransform>().sizeDelta = boxSize;
            TextMeshPro textMeshPro = textObject.GetComponent<TextMeshPro>();
            textMeshPro.text = text;
            textMeshPro.fontSize = fontSize;
            if (color.a == 0)
                textMeshPro.color = Color.white;
            else textMeshPro.color = color;
            textMeshPro.sortingOrder = sortingOrder;
            textMeshPro.alignment = alignment;
            return textMeshPro;
        }

        //Get the mouse position in the world
        public static Vector3 GetMouseWorldPosition2D()
        {
            Vector3 position = GetMouseWorldPosition3D();
            position.z = 0f;
            return position;
        }
        public static Vector3 GetMouseWorldPosition2D(Camera camera)
        {
            Vector3 position = GetMouseWorldPosition3D(camera);
            position.z = 0f;
            return position;
        }
        public static Vector3 GetMouseWorldPosition3D()
        {
            return GetMouseWorldPosition3D(Camera.main);
        }
        public static Vector3 GetMouseWorldPosition3D(Camera camera)
        {
            Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition);
            return position;
        }
    }//BobTools class end
}//Namespace end