using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCam : MonoBehaviour
{
    [Header("Main Settings")] public PortalCam linkedPortal;

    [SerializeField] private GameObject _screen;
    private Camera portalСam;

    [Header("Advanced Settings")] public float nearClipOffset = 1f;

    // Private variables
    private float nearClipLimit = 0.2f; // для функции, которую я не использую
    private Camera _mainCamera;
    private Renderer _rendererScreen;
    private Material _screenMat;
    private bool _isMeshSkinnedMesh = false;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        portalСam = GetComponentInChildren<Camera>();
        _screenMat = new Material(Shader.Find("Unlit/NewUnlitShader2"));

        _skinnedMeshRenderer = _screen.GetComponent<SkinnedMeshRenderer>();
        _meshRenderer = _screen.GetComponent<MeshRenderer>();

        if (_skinnedMeshRenderer != null)
        {
            _isMeshSkinnedMesh = true;
        }
        else if (_meshRenderer != null)
        {
            _isMeshSkinnedMesh = false;
        }

        if (_isMeshSkinnedMesh)
        {
            _skinnedMeshRenderer.material = _screenMat;
        }
        else
        {
            _meshRenderer.material = _screenMat;
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        linkedPortal.portalСam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        if (_isMeshSkinnedMesh)
        {
            _rendererScreen = _skinnedMeshRenderer;
            _skinnedMeshRenderer.material.mainTexture = linkedPortal.portalСam.targetTexture;
        }
        else
        {
            _rendererScreen = _meshRenderer;
            _meshRenderer.GetComponent<MeshRenderer>().sharedMaterial.mainTexture =
                linkedPortal.portalСam.targetTexture;
        }
    }

    private void Update()
    {
        // Skip rendering the view from this portal if player is not looking at the linked portal
        if (!VisibleFromCamera(linkedPortal._rendererScreen, _mainCamera))
        {
            return;
        }

        // Position
        Vector3 lookerPosition =
            linkedPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(_mainCamera.transform.position);
        lookerPosition = new Vector3(lookerPosition.x, lookerPosition.y, lookerPosition.z);
        portalСam.transform.localPosition = lookerPosition;

        // Rotation
        Quaternion difference = transform.rotation *
                                Quaternion.Inverse(linkedPortal.transform.rotation * Quaternion.Euler(0, 0, 0));
        portalСam.transform.rotation = difference * _mainCamera.transform.rotation;

        // Clipping
        portalСam.nearClipPlane = lookerPosition.magnitude +nearClipOffset;
        //SetNearClipPlane();
    }


    //additional functions
    private bool VisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }

    // Use custom projection matrix to align portal camera's near clip plane with the surface of the portal
    // Note that this affects precision of the depth buffer, which can cause issues with effects like screenspace AO
    void SetNearClipPlane()
    {
        // Learning resource:
        // http://www.terathon.com/lengyel/Lengyel-Oblique.pdf
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalСam.transform.position));

        Vector3 camSpacePos = portalСam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalСam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal) + nearClipOffset;

        // Don't use oblique clip plane if very close to portal as it seems this can cause some visual artifacts
        if (Mathf.Abs(camSpaceDst) > nearClipLimit)
        {
            Vector4 clipPlaneCameraSpace =
                new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);

            // Update projection based on new clip plane
            // Calculate matrix with player cam so that player camera settings (fov, etc) are used
            portalСam.projectionMatrix = _mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        }
        else
        {
            portalСam.projectionMatrix = _mainCamera.projectionMatrix;
        }
    }

    void OnValidate()
    {
        if (linkedPortal != null)
        {
            linkedPortal.linkedPortal = this;
        }
    }
}