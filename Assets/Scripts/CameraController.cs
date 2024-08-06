using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject MainCharecter;
    private Vector3 xVelocity = Vector3.zero;
    private Vector3 yVelocity = Vector3.zero;
    public float smoothTimeX = 0.5f;
    public float smoothTimeY = 0.2f;
    private Vector3 target;
    private Vector3 newPos;

    // Update is called once per frame
    void Update()
    {
        newPos.x = Vector3.SmoothDamp(transform.position, MainCharecter.transform.position, ref xVelocity, smoothTimeX).x;
        newPos.y = Vector3.SmoothDamp(transform.position, MainCharecter.transform.position, ref yVelocity, smoothTimeY).y;

        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
