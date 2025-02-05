using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathSceneScript : MonoBehaviour
{
    [SerializeField] private Text gameOverTextComponent;  
    [SerializeField] private Text continueTextComponent;  
    [SerializeField] private string fullText = "Game Over";     
    [SerializeField] private string continueText = "Press space to continue"; //asdasd
    [SerializeField] private float fadeDuration = 2f;  

    private bool isMessageVisible = false;  

    void Start()
    {
        
        StartCoroutine(FadeInTextEffect());
    }

    IEnumerator FadeInTextEffect()
    {
        gameOverTextComponent.text = fullText;  
        Color textColor = gameOverTextComponent.color;  
        textColor.a = 0f;  
        gameOverTextComponent.color = textColor; 

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;  
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);  
            gameOverTextComponent.color = textColor;  
            yield return null;  
        }

        textColor.a = 1f;  
        gameOverTextComponent.color = textColor;

        yield return new WaitForSeconds(1f);  

        ShowContinueMessage();
    }

    void ShowContinueMessage()
    {
        continueTextComponent.text = continueText;  
        continueTextComponent.color = Color.white;  
        continueTextComponent.fontSize = 24;  

        isMessageVisible = true;  
    }

    void Update()
    {
        if (isMessageVisible && Input.GetKeyDown(KeyCode.Space)) 
        {
            ContinueGame();
        }
    }

    void ContinueGame()
    {
        SceneManager.LoadScene("Menu");  
    }
}
