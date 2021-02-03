using Cinemachine;
using UnityEngine;

public class ToggleCinemachineCamera : MonoBehaviour
{

    bool cineMachineActive = true;
    [SerializeField] CinemachineBrain cineMachineBrain;
    [SerializeField] FollowPlayer fp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cineMachineActive = !cineMachineActive;
            cineMachineBrain.enabled = cineMachineActive;
            fp.enabled = !cineMachineActive;
        }
    }
}
