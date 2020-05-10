using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPos;
    public float moveSpeed;

    //public Vector3 offset;

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void FixedUpdate()
    {

        
        if (target != null)
        {
           
            targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 velocity = (targetPos - transform.position) * moveSpeed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
            
        }
        
        //Vector3 desiredPos = target.position + offset;
        //Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, moveSpeed);
        //transform.position = smoothedPos;
        //transform.LookAt(target);
    }
}
