using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{    
    void Update()
    {
        SkipCredits();    
    }


    public void SkipCredits()
    {
        if(Input.GetButton("Jump"))
        {
            ExitCredits();
        }    
    }

    public void ExitCredits()
    {
        SceneManager.LoadScene(0);
    }
}
