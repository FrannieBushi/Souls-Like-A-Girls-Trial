using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PjSoundManager : MonoBehaviour
{
    public AudioSource attackAudio; 

    public void PlayAttackSound()
    {
        if (attackAudio != null)
            attackAudio.Play();
    }
}
