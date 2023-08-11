using UnityEngine;
using UnityEngine.Serialization;

public class CameraAutoResize : MonoBehaviour
{
    public Camera Camera;
    public GameObject[] boundaryObjects = new GameObject[2];
    public float zoomSpeed = 1.0f;
    public float zoomMultiplier = 1.2f;
    public float padding = 30f; // Отступ для расширения границ объектов

    private float initialOrthographicSize;
    private float targetOrthographicSize;

    private void Start()
    {
        initialOrthographicSize = Camera.orthographicSize;
        targetOrthographicSize = initialOrthographicSize;
    }

    private void Update()
    {
        if (!AllObjectsVisible())
        {
            targetOrthographicSize *= zoomMultiplier;
            Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        }
        else
        {
            this.enabled = false;
        }
    }

    private bool AllObjectsVisible()
    {
        foreach (GameObject obj in boundaryObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                Bounds expandedBounds = renderer.bounds;
                expandedBounds.Expand(padding); // Расширяем границы объекта с отступом

                if (!IsVisibleForCamera(expandedBounds, Camera))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsVisibleForCamera(Bounds bounds, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}