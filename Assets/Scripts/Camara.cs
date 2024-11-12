using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {

        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(smoothedPosition.x, minX, maxX),
            Mathf.Clamp(smoothedPosition.y, minY, maxY),
            transform.position.z
        );
        transform.position = clampedPosition;
    }
}
