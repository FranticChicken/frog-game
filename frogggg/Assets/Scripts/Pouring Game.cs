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
    float demotimer = 5.0f;

    //cup moving animation
    bool isCupMoving = false;
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
    }

    //sets the max fill height to the same as the glass height (important for when we change the glass later)
    public void SetMaxFill()
    {
        maxFill = glassRect.rect.height;
    }

    public void SetFillAmount()
    {
        //amountFilled = fill;

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

            
            
            if(demotimer > 0)
            {
                demotimer -= Time.deltaTime;
                if( demotimer <= 0)
                {
                    amountFilled = 0;
                    SetPourButtonActive(true);
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

    // Update is called once per frame
    void Update()
    {

        SetFillAmount();
        SetMaxFill();

        if(demo == true)
        {
            PourDemo();
        }
        

        if(isCupMoving == true)
        {
            MoveCupUp(false);
        }

        if(glassStuffRect.anchoredPosition.y >= 200 || glassStuffRect.anchoredPosition.y <= -200)
        {
            isCupMoving = false;
        }


            Debug.Log(isCupMoving);
        Debug.Log(targetY);
    }
}
