using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;



public class PouringGame : MonoBehaviour
{
    //images
    public Image glassCupImage;
    public Image liquidFill;
    public Image bottleImage;
    public Image liquidPouringImage;

    //text
    public TextMeshProUGUI scoreText;

    //fill mechanic
    public float amountFilled, maxFill;

    [SerializeField]
    private RectTransform liquidFillRect;

    [SerializeField]
    private RectTransform glassRect;

    //buttons
    public Button pourButton;

    //demo 
    bool demo = true;
    

    //cup moving animation
    bool isCupMoving = false;
    bool isCupMoving2 = false;
    float targetX = 0f;
    float targetY = 27f;
    Vector2 targetPosition;

    [SerializeField]
    private RectTransform glassStuffRect;
    
    public float moveSpeed = 100f;

    public GameObject pouringGameObject;

    public PlayerInteraction playerInteractionScript;

    Coroutine pouringCoroutine;

    public GameOverUI gameOverUIScript; 
    public GameObject mainPouringGameObject;
    int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    public void StartGame()
    {
        if(pouringCoroutine != null)
        {
            StopCoroutine(pouringCoroutine);
            pouringCoroutine = null;
        }
       
        
        pouringCoroutine = StartCoroutine(PlayPouringGame());
    }

    public void ExitGame()
    {
        if(pouringCoroutine != null)
        {
            StopCoroutine(pouringCoroutine);
            pouringCoroutine = null;
            gameOverUIScript.MakeRetryMenuAppear(1, score);
        }

        mainPouringGameObject.SetActive(false);
    }

    public IEnumerator PlayPouringGame()
    {
        pouringGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None; 
        scoreText.gameObject.SetActive(false);
        score = 0;
        pourButton.gameObject.SetActive(false);
        liquidPouringImage.gameObject.SetActive(true);

        amountFilled = 0;
        SetFillAmount();
        SetMaxFill();

        demo = true;
        float demotimer = 2.0f;

        //targetPosition = new Vector2(targetX, targetY);
        //targetX = 0;
        //targetY = -200f;
        
        while(demo)
        {
            amountFilled += (25 * Time.deltaTime);
            SetFillAmount();

            if (amountFilled >= maxFill)
            {
                liquidPouringImage.gameObject.SetActive(false);
            
                //pause after the demo before it is the players turn
                demotimer -= Time.deltaTime;
                if( demotimer <= 0)
                {
                    amountFilled = 0;
                    SetFillAmount();
                    //isCupMoving = true;
                    demo = false;
                }
                
            }

            //PourDemo();
            //break;
            yield return null;
        }

        isCupMoving = true; 

        while(isCupMoving)
        {
            MoveCupUp(false);

            if(glassStuffRect.anchoredPosition.y <= -250)
            {
                isCupMoving = false;
                SetPourButtonActive(true);
            }
            yield return null;
        }

        while(isCupMoving2 == false)
        {
            yield return null;
        }

        while(isCupMoving2 == true)
        {
            MoveCupUp(true);
            SetPourButtonActive(false);

            if(glassStuffRect.anchoredPosition.y >= 27)
            {
                isCupMoving2 = false;
                
            }
            yield return null;
        }
        
        if(amountFilled > maxFill)
        {
            score = 0;
        }
        if(amountFilled == maxFill)
        {
            score = 100;
        }
        if(amountFilled <= maxFill / 4)
        {
            score = 1;
        }
        if(amountFilled <= maxFill/2 && amountFilled > maxFill / 4)
        {
            score = 5;
        }
        if(amountFilled <= maxFill/4 * 3 && amountFilled > maxFill/2)
        {
            score = 10;
        }
        if(amountFilled < maxFill && amountFilled > maxFill/ 4 * 3)
        {
            score = 35;
        }



        yield return new WaitForSeconds(3.0f);


        
        ExitGame();
    }

    //sets the max fill height to the same as the glass height (important for when we change the glass later)
    public void SetMaxFill()
    {
        maxFill = glassRect.rect.height;
    }

    //visually shows how filled the glass is
    public void SetFillAmount()
    {
        float newHeight = (amountFilled / maxFill) * maxFill;

        if(amountFilled > maxFill)
        {
            //if the amount filled is more than the glass height, do not overfill)
            liquidFillRect.sizeDelta = new Vector2(glassRect.rect.width, maxFill);
        }
        else
        {
            //otherwise just show normal filled amount visually
            liquidFillRect.sizeDelta = new Vector2(glassRect.rect.width, newHeight);
        }
    }

    //makes the amount filled based off time the player poured
    //function called in the Pour Button Controller script
    public void Pour(float timePoured) 
    {
        amountFilled = timePoured * 25;
        SetFillAmount();
    }
    

    

    public void MoveCupUp(bool moveUp)
    {
        if(moveUp == true)
        {

            glassStuffRect.anchoredPosition += new Vector2(0, moveSpeed * Time.deltaTime);
        }
        else if(moveUp == false)
        {
            
            glassStuffRect.anchoredPosition -= new Vector2(0, moveSpeed * Time.deltaTime);
        }
    }


    
    public void SetPourImageActive(bool active)
    {
        liquidPouringImage.gameObject.SetActive(active);
    }

    public void SetPourButtonActive(bool active)
    {
        pourButton.gameObject.SetActive(active);
    }

    public void SetIsCupMoving2(bool isIt)
    {
        isCupMoving2 = isIt;
    }
    // Update is called once per frame
    void Update()
    {
        
            
        

    }
    
}
