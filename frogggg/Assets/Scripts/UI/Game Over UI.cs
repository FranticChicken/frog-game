using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public Button retryButton;
    public Button quitButton;
    public Image retryMenu;
    public TextMeshProUGUI retryText;
    public TextMeshProUGUI scoreText;
    int gameInt2;
    public PianoGame pianoGameScript;
    public PouringGame pouringGameScript;
    public CardRandomization cardGameScript;
    public PlayerInteraction playerInteractionScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        retryButton.onClick.AddListener(OnRetryButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);

        retryMenu.gameObject.SetActive(false);
    }

    void OnRetryButtonClick()
    {
        //0 = piano game
        //1 = pouring game
        //2 = card game
        //3 = burgeria game

        retryMenu.gameObject.SetActive(false);

        if(gameInt2 == 0)
        {
            pianoGameScript.StartGame();
        }
        if(gameInt2 == 1)
        {
            pouringGameScript.StartGame();
        }
        if(gameInt2 == 3)
        {
            cardGameScript.StartCardGame();
        }
        if(gameInt2 == 4)
        {
            
        }


    }

    void OnQuitButtonClick()
    {
        retryMenu.gameObject.SetActive(false);
        //return player movement
        playerInteractionScript.gaming = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MakeRetryMenuAppear(int gameInt, int score)
    {
        retryMenu.gameObject.SetActive(true);
        gameInt2 = gameInt;
        Cursor.lockState = CursorLockMode.None;
        //return player cursor

        //0 = piano game
        //1 = pouring game
        //2 = card game
        //3 = burgeria game


        if(gameInt == 0)
        {
            retryText.text = "You played off key! Try Again!";
            scoreText.text = "You got " + score + " points!";
        }
        else if(gameInt == 1)
        {
            retryText.text = "Nice pouring!";
            scoreText.text = "You got " + score + " points!";
        }
        else if(gameInt == 2)
        {
            retryText.text = "Game Over!";
            scoreText.text = "You got " + score + " points!";
        }
        else if(gameInt == 3)
        {
            retryText.text = "Game Over!";
            scoreText.text = "You got " + score + " points!";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
