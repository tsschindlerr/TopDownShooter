using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset = new Vector3(0, 30, 0);
    private Vector3 lastPosition;

    void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (player != null)
        {
            lastPosition = transform.position = player.transform.position + offset;
            transform.position = lastPosition;
        }
        else
        {
            transform.position = lastPosition;
        }
    }
}