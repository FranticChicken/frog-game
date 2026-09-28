using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardRandomization : MonoBehaviour
{
    public Image [] placedCardHolders;
    public Image [] playerCardHolders;
    public Sprite [] cardSprites;

    public Button dealButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dealButton.onClick.AddListener(DealPlacedCards);
        DealPlayerCards();
    }

    
    void DealPlacedCards()
    {
        //choose 4 cards from deck with no repeats
        

        //InputActionReference[] requiredSequence = new InputActionReference[arrowImageHolders.Length];
        Sprite[] placedCardsSprites = new Sprite[placedCardHolders.Length];
        
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
        
        

    }

    void DealPlayerCards()
    {
        //choose 2 cards from deck with no repeats
        Sprite[] playerCardsSprites = new Sprite[playerCardHolders.Length];
        
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
