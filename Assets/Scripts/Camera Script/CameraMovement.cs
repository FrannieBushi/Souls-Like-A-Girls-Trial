using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float FollowSpeed = 2f;  
    [SerializeField] private float yOffset = -1f;     
    [SerializeField] private Transform target;

    void Update()
    {
        
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        
        float verticalDifference = newPos.y - transform.position.y;

        float adjustedSpeed = FollowSpeed;

        if (verticalDifference > 0) 
        {
            adjustedSpeed *= 0.5f;  
        }
        else if (verticalDifference < 0) 
        {
            adjustedSpeed *= 1.8f;  
        }

        transform.position = Vector3.Slerp(transform.position, newPos, adjustedSpeed * Time.deltaTime);
    }
}
