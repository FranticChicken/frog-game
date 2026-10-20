using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CardRandomization : MonoBehaviour
{
    public Image [] placedCardHolders;
    public Image [] playerCardHolders;
    public Sprite [] cardSprites;

    public Button dealButton;

    [HideInInspector]
    public Sprite[] placedCardsSprites;
    [HideInInspector]
    public Sprite [] playerCardsSprites;
    public GameObject cardGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dealButton.onClick.AddListener(DealPlacedCards);

        
    }

    public void StartCardGame()
    {
        DealPlayerCards();
        DealPlacedCards();
        Cursor.lockState = CursorLockMode.None;

        cardGameObject.SetActive(true);
        for (int i = 0; i < playerCardHolders.Length; i++)
        {
            playerCardHolders[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < placedCardHolders.Length; i++)
        {
            placedCardHolders[i].gameObject.SetActive(true);
        }
        
    }
    
    void DealPlacedCards()
    {
        //choose 4 cards from deck with no repeats
        

        //InputActionReference[] requiredSequence = new InputActionReference[arrowImageHolders.Length];
        placedCardsSprites = new Sprite[placedCardHolders.Length];
        
        for (int i = 0; i < placedCardHolders.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSprites.Length);
            placedCardsSprites[i] = cardSprites[randomIndex]; 
        }

        for (int i = 0; i < placedCardHolders.Length; i++ )
        {
            placedCardHolders[i].sprite = placedCardsSprites[i];
            placedCardHolders[i].preserveAspect = true;
        }
        
        //send placed cards info (array) to dropping script

    }

    void DealPlayerCards()
    {
        //choose 2 cards from deck with no repeats
        playerCardsSprites = new Sprite[playerCardHolders.Length];
        
        for (int i = 0; i < playerCardHolders.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, cardSprites.Length);
            playerCardsSprites[i] = cardSprites[randomIndex]; 
        }

        for (int i = 0; i < playerCardHolders.Length; i++ )
        {
            playerCardHolders[i].sprite = playerCardsSprites[i];
            playerCardHolders[i].preserveAspect = true;
        }
    }
    

    public void UpdateCards(int placedCardIndex, int playerCardIndex)
    {
        //placed card turns into dropped card
        placedCardHolders[placedCardIndex].sprite = playerCardHolders[playerCardIndex].sprite;

        //dropped card is given random sprite
        int randomIndex = UnityEngine.Random.Range(0, cardSprites.Length);
        playerCardsSprites[playerCardIndex] = cardSprites[randomIndex];
        playerCardHolders[playerCardIndex].sprite = playerCardsSprites[playerCardIndex];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
