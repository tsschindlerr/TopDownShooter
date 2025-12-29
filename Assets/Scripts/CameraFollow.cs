using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(-3, 24, 0);
    private Vector3 lastPosition;

    void Start()
    {

    }


    void LateUpdate()
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
