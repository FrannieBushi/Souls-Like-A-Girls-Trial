using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GameObject firstSelectedButton; // Botón inicial asignado desde el Inspector.
    public GameObject swordIcon; // Imagen de la espada, asignada desde el Inspector.

    public AudioClip buttonSelectSound; // Sonido al cambiar de botón.
    public AudioClip buttonClickSound; // Sonido al hacer clic en un botón.

    private AudioSource effectsAudioSource; // AudioSource exclusivo para los efectos de sonido.
    private GameObject currentSelectedButton; // Botón actualmente seleccionado.

    void Start()
    {
        // Crear un AudioSource solo para efectos de sonido, si no existe ya.
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource.playOnAwake = false; // Asegúrate de que no reproduzca automáticamente.
        effectsAudioSource.volume = 1.0f; // Ajusta el volumen según sea necesario.

        // Seleccionar automáticamente el botón inicial al iniciar el menú.
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        currentSelectedButton = firstSelectedButton;

        UpdateSwordIconPosition(currentSelectedButton.transform.position);
    }

    void Update()
    {
        // Asegurarse de que siempre haya un botón seleccionado.
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }

        // Detectar cambio de botón seleccionado.
        if (currentSelectedButton != EventSystem.current.currentSelectedGameObject)
        {
            currentSelectedButton = EventSystem.current.currentSelectedGameObject;
            PlaySound(buttonSelectSound); // Reproducir sonido al cambiar de botón.

            // Actualizar posición del icono de espada.
            if (currentSelectedButton != null)
            {
                UpdateSwordIconPosition(currentSelectedButton.transform.position);
            }
        }

        // Opcional: Detectar teclas rápidas como Escape para salir.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    private void UpdateSwordIconPosition(Vector3 buttonPosition)
    {
        swordIcon.transform.position = new Vector3(buttonPosition.x + 1.5f, buttonPosition.y, buttonPosition.z);
    }

    public void NewGame()
    {
        PlaySound(buttonClickSound); // Reproducir sonido al hacer clic en el botón.

        // Cargar la siguiente escena.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        PlaySound(buttonClickSound); // Reproducir sonido al hacer clic en el botón.

        // Salir del juego.
        Debug.Log("Leaving...");
        Application.Quit();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && effectsAudioSource != null)
        {
            effectsAudioSource.PlayOneShot(clip); // Reproduce el sonido por encima de la música de fondo.
        }
    }
}