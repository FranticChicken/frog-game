using UnityEngine;
using TMPro;
using UnityEngine.UI;

using Random = UnityEngine.Random;
using System.Collections;

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
    Coroutine cardCoroutine;
    public GameOverUI gameOverUIScript;

    int score;

    float timer;

    public TextMeshProUGUI timerText;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dealButton.onClick.AddListener(DealPlacedCards);
        //Sprite mySprite = new Sprite();
        //System.Object myObject = (System.Object)mySprite;
    }

    public void StartCardGame()
    {
        if(cardCoroutine != null)
        {
            StopCoroutine(cardCoroutine);
            cardCoroutine = null;
        }
       
        cardCoroutine = StartCoroutine(PlayCardGame());
    }

    public void ExitGame()
    {
        if(cardCoroutine!= null)
        {
            StopCoroutine(cardCoroutine);
            cardCoroutine = null;
            gameOverUIScript.MakeRetryMenuAppear(2, score);
            
        }

        cardGameObject.SetActive(false);
    }

    public IEnumerator PlayCardGame()
    {
        DealPlayerCards();
        DealPlacedCards();
        score = 0;
        Cursor.lockState = CursorLockMode.None;
        timer = 30;
        cardGameObject.SetActive(true);
        for (int i = 0; i < playerCardHolders.Length; i++)
        {
            playerCardHolders[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < placedCardHolders.Length; i++)
        {
            placedCardHolders[i].gameObject.SetActive(true);
        }
        
        while(timer > 0f)
        {

            timer -= Time.deltaTime;
            timerText.text = "0:" + Mathf.CeilToInt(timer).ToString();
            yield return null;
        }
        
        ExitGame();
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
        score +=5;

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
