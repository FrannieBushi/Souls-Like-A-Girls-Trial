using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    public Transform[] transforms;
    public GameObject flame;

    public float timeToShoot, countdown;

    void Start()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
        countdown = timeToShoot;    
    }

    void Update()
    {
        if(countdown < 0)
        {
            ShootPlayer();
            countdown = timeToShoot;
        }    
    }

    public void ShootPlayer()
    {
        
        GameObject spell = Instantiate(flame, transform.position, Quaternion.identity);
            
    }
}
