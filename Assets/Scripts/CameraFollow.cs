using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 offset = new(0f, 0f);

    public float dampTime = 0.4f;
    public Transform target;
    private Vector3 _velocity = Vector3.zero;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    private void Update()
    {
        var point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        var delta = target.position -
                    GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        var destination = transform.position + delta;

        destination = new Vector3(destination.x, offset.y, destination.z);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
    }


    public void ResetToStartPosition()
    {
        var point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        var delta = target.position -
                    GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        var destination = transform.position + delta;

        destination = new Vector3(destination.x, offset.y, destination.z);
        transform.position = destination;
    }
}