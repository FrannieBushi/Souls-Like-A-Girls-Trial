using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDestroyer : MonoBehaviour
{

    public float secondsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
