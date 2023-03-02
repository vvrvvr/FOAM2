using UnityEngine;
using Cinemachine;
using StarterAssets;

public class PlayerSwitchViews : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera firstPersonCam;
    //[SerializeField] private CinemachineVirtualCamera thirdPersonCam;

    //Start sequence
    [SerializeField] private CinemachineVirtualCamera startSequenceCamera;
    [SerializeField] private CinemachineVirtualCamera flyCamera;

    //[SerializeField] private GameObject playerObject;
    private ThirdPersonController _playerController;

    private void OnEnable()
    {
        CameraSwitcher.Register(firstPersonCam);
        //CameraSwitcher.Register(thirdPersonCam);
        CameraSwitcher.Register(startSequenceCamera);
        CameraSwitcher.Register(flyCamera);
    }
    
    private void OnDisable()
    {
        CameraSwitcher.Unregister(firstPersonCam);
        // CameraSwitcher.Unregister(thirdPersonCam);
        CameraSwitcher.Unregister(startSequenceCamera);
        CameraSwitcher.Unregister(flyCamera);
    }


    void Start()
    {
        _playerController = ThirdPersonController.Instance;
        _playerController.enabled = false;
       // Debug.Log("turn off");
    }

    // void Update()
    // {
    //     SwitchView();
    // }
    //
    // void SwitchView()
    // {
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         StartStartSequence();
    //     }
    //     if (Input.GetKeyDown(KeyCode.G))
    //     {
    //         EndStartSequence();
    //     }
    //    
    // }

    public void StartStartSequence()
    {
        CameraSwitcher.SwitchCamera(startSequenceCamera);
    }

    public void EndStartSequence()
    {
        CameraSwitcher.SwitchCamera(firstPersonCam);
    }
    
    
    
}