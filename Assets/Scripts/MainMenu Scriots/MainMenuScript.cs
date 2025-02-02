using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GameObject firstSelectedButton; 
    public GameObject swordIcon; 

    public AudioClip buttonSelectSound; 
    public AudioClip buttonClickSound; 

    private AudioSource effectsAudioSource; 
    private GameObject currentSelectedButton; 

    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource.playOnAwake = false; 
        effectsAudioSource.volume = 1.0f; 

        
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        currentSelectedButton = firstSelectedButton;

        UpdateSwordIconPosition(currentSelectedButton.transform.position);
    }

    void Update()
    {
        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }

        
        if (currentSelectedButton != EventSystem.current.currentSelectedGameObject)
        {
            currentSelectedButton = EventSystem.current.currentSelectedGameObject;
            PlaySound(buttonSelectSound); 

            if (currentSelectedButton != null)
            {
                UpdateSwordIconPosition(currentSelectedButton.transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    private void UpdateSwordIconPosition(Vector3 buttonPosition)
    {
        swordIcon.transform.position = new Vector3(buttonPosition.x + 2.7f, buttonPosition.y, buttonPosition.z);
    }

    public void NewGame()
    {
        PlaySound(buttonClickSound); 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FinalBoss()
    {
        PlaySound(buttonClickSound); 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        PlaySound(buttonClickSound); 

        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        PlaySound(buttonClickSound); 

        Debug.Log("Leaving...");
        Application.Quit();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && effectsAudioSource != null)
        {
            effectsAudioSource.PlayOneShot(clip); 
        }
    }
}