
using UnityEngine;

public class followCam : MonoBehaviour
{
    public Transform target;
    [SerializeField] float smoothSpeed = 10f;
    [SerializeField] Vector3 offset;
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
