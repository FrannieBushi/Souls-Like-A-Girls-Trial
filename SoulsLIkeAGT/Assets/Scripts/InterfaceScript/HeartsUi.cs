using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HeartsUi : MonoBehaviour
{
    public List<Image> heartsList = new List<Image>();
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;

    // Sprites para los estados de los corazones
    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        playerHealth.healthChange.AddListener(UpdateHearts);
    }

    private void Start()
    {
        CreateHearts(playerHealth.maxHealth);
        UpdateHearts(playerHealth.health);
    }

    private void UpdateHearts(float health)
    {
        float healthPerHeart = playerHealth.maxHealth / heartsList.Count;

        for (int i = 0; i < heartsList.Count; i++)
        {
            float heartHealth = Mathf.Clamp(health - (i * healthPerHeart), 0, healthPerHeart);
            float percentage = heartHealth / healthPerHeart;

            if (percentage >= 1)
            {
                heartsList[i].sprite = fullHeart;
            }
            else if (percentage >= 0.75f)
            {
                heartsList[i].sprite = threeQuarterHeart;
            }
            else if (percentage >= 0.5f)
            {
                heartsList[i].sprite = halfHeart;
            }
            else if (percentage >= 0.25f)
            {
                heartsList[i].sprite = quarterHeart;
            }
            else
            {
                heartsList[i].sprite = emptyHeart;
            }
        }
    }

    private void CreateHearts(float maxHealth)
    {
        int numberOfHearts = Mathf.CeilToInt(maxHealth); // Redondeamos hacia arriba para cubrir toda la salud

        for (int i = 0; i < numberOfHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heartsList.Add(heart.GetComponent<Image>());
        }
    }
}
