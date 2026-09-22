using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework;


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
    float demotimer = 2.0f;

    //cup moving animation
    bool isCupMoving = false;
    bool isCupMoving2 = false;
    float targetX = 0f;
    float targetY = 27f;
    Vector2 targetPosition;

    [SerializeField]
    private RectTransform glassStuffRect;
    
    public float moveSpeed = 100f;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PourDemo();
        pourButton.gameObject.SetActive(false);
        targetPosition = new Vector2(targetX, targetY);
        targetX = 0;
        targetY = -200f;

        scoreText.gameObject.SetActive(false);
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
    }
    

    void PourDemo()
    {   
        amountFilled += (25 * Time.deltaTime);

        if (amountFilled >= maxFill)
        {
            liquidPouringImage.gameObject.SetActive(false);

            
            //pause after the demo before it is the players turn
            if(demotimer > 0)
            {
                demotimer -= Time.deltaTime;
                if( demotimer <= 0)
                {
                    amountFilled = 0;
                    
                    isCupMoving = true;
                    demo = false;
                }
            }
        }
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

        SetFillAmount();
        SetMaxFill();

        if(demo == true)
        {
            PourDemo();
        }
        
        //if iscupmoving = true and 
        if(isCupMoving == true)
        {
            MoveCupUp(false);
        }

        if(glassStuffRect.anchoredPosition.y <= -250)
        {
            isCupMoving = false;
            SetPourButtonActive(true);
        }

        if(isCupMoving2 == true)
        {
            MoveCupUp(true);
            SetPourButtonActive(false);
        }

        if(glassStuffRect.anchoredPosition.y >= 27)
        {
            isCupMoving2 = false;
            
        }

        if(demo == false && isCupMoving == false && isCupMoving2 == false && glassStuffRect.anchoredPosition.y >= 27)
        {
            scoreText.gameObject.SetActive(true);
        }
        

        Debug.Log(isCupMoving);
        Debug.Log(targetY);

        scoreText.text = "Score: " + amountFilled.ToString("F0"); 
    }
}
