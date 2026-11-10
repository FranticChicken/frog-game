using UnityEngine;
using Unity.Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineCamera[] cameras;
    public MeshRenderer playerMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cameras[0].Priority = 5;
        cameras[1].Priority = 5;
        cameras[2].Priority = 5;
        cameras[3].Priority = 10;
    }

    public void SwitchCam(int camNumber, int prio)
    {
        cameras[camNumber].Priority = prio;
    
    }

    
    // Update is called once per frame
    void Update()
    {
        if(cameras[3].Priority < cameras[0].Priority || cameras[3].Priority < cameras[1].Priority || cameras[3].Priority < cameras[2].Priority)
        {
            playerMesh.enabled = false;
        }
        else
        {
            playerMesh.enabled = true;
        }
    }
}
