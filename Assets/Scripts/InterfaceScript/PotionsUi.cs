using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsUi : MonoBehaviour
{
    public List<Image> potionsList = new List<Image>();  
    public GameObject potionPrefab;
    public PlayerHealth playerHealth;

    public Sprite fullPotion;
    public Sprite emptyPotion;

    private void Awake()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth no está asignado en PotionsUi.");
            return;
        }

        playerHealth.potionChange.AddListener(ChangePotions);  // Escucha los cambios en las pociones.
    }

    private void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth no está asignado en PotionsUi.");
            return;
        }

        CreatePotions(playerHealth.maxPotions); // Crea las pociones iniciales
        UpdatePotions(playerHealth.potions); // Asegúrate de actualizar la UI con la cantidad de pociones actual.
    }

    // Cambia el número de pociones cuando se modifique la cantidad de pociones del jugador.
    private void ChangePotions(int potionsAmount)
    {
        Debug.Log("Cambiando cantidad de pociones a: " + potionsAmount);
        UpdatePotions(potionsAmount);
    }

    // Crea las pociones iniciales en la UI, basándonos en el número máximo de pociones.
    private void CreatePotions(int potionsAmount)
    {
        // Limpiar las pociones existentes antes de crear nuevas.
        foreach (var potion in potionsList)
        {
            Destroy(potion.gameObject);
        }
        potionsList.Clear();

        Debug.Log("Creando " + potionsAmount + " pociones.");

        // Crear las pociones visualmente en la UI.
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
            potionImage.sprite = emptyPotion; // Inicialmente todas las pociones están vacías.
        }
    }

    // Actualiza las pociones visualmente en la UI.
    private void UpdatePotions(int potionsAmount)
    {
        Debug.Log("Actualizando pociones, cantidad actual: " + potionsAmount);

        // Asegúrate de que las imágenes de las pociones reflejen el estado correcto.
        for (int i = 0; i < potionsList.Count; i++)
        {
            // Si el índice de la poción es menor que la cantidad de pociones, debe estar llena.
            // Si es mayor, debe estar vacía.
            potionsList[i].sprite = (i < potionsAmount) ? fullPotion : emptyPotion;
        }
    }
}