using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PotionsUi : MonoBehaviour
{
    public List<Image> potionsList = new List<Image>();  
    public GameObject potionPrefab;
    public PlayerHealth playerHealth;

    public int indexPotion;

    public Sprite fullPotion;
    public Sprite emptyPotion;

    private void Awake()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth no está asignado en PotionsUi.");
            return;
        }

        playerHealth.potionChange.AddListener(ChangePotions);
    }

    private void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth no está asignado en PotionsUi.");
            return;
        }

        CreatePotions(playerHealth.maxPotions);
    }

    private void ChangePotions(int potionsAmount)
    {
        Debug.Log("Cambiando cantidad de pociones a: " + potionsAmount);

        if (potionsList.Count == 0)
        {
            CreatePotions(potionsAmount);
        }
        else
        {
            UpdatePotions(potionsAmount);
        }
    }

    private void CreatePotions(int potionsAmount)
    {
        Debug.Log("Creando " + potionsAmount + " pociones.");

        for (int i = 0; i < potionsAmount; i++)  
        {
            GameObject potion = Instantiate(potionPrefab, transform);

            Image potionImage = potion.GetComponent<Image>();
            if (potionImage == null)
            {
                Debug.LogError("El prefab de la poción no tiene un componente Image.");
                return;
            }

            potionsList.Add(potionImage);
            potionImage.sprite = fullPotion;  
        }

        indexPotion = potionsAmount - 1;
    }

    private void UpdatePotions(int potionsAmount)
    {
        Debug.Log("Actualizando pociones, cantidad actual: " + potionsAmount);

        for (int i = 0; i < potionsList.Count; i++)
        {
            potionsList[i].sprite = (i < potionsAmount) ? fullPotion : emptyPotion;
        }
    }
}