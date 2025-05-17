using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public GameObject fpsCamera;
    public GameObject thirdPersonCamera;
    public GameObject cameraChoicePanel;

    public void SelectFirstPerson()
    {
        fpsCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
        cameraChoicePanel.SetActive(false);
    }

    public void SelectThirdPerson()
    {
        fpsCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        cameraChoicePanel.SetActive(false);
    }
}
