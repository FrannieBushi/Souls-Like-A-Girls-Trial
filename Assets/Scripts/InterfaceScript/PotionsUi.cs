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
        
        playerHealth.potionChange.AddListener(UpdatePotions);
    }

    private void Start()
    {
        
        CreatePotions(playerHealth.maxPotions);
        
        UpdatePotions(playerHealth.potions);
    }

    private void CreatePotions(int maxPotions)
    {
        for (int i = 0; i < maxPotions; i++)
        {
            GameObject potion = Instantiate(potionPrefab, transform);
            potionsList.Add(potion.GetComponent<Image>());
        }
    }

    private void UpdatePotions(int currentPotions)
    {
        for (int i = 0; i < potionsList.Count; i++)
        {
            potionsList[i].sprite = (i < currentPotions) ? fullPotion : emptyPotion;
        }
    }
}
