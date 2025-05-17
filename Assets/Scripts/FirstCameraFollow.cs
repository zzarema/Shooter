using UnityEngine;

public class FirstCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 1.6f, 0f);

    void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.rotation = player.rotation;
    }
}
