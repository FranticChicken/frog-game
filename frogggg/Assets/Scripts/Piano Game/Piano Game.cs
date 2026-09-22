using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework;
using UnityEngine.InputSystem;
using System;


public class PianoGame : MonoBehaviour
{
    
    public Image [] arrowImageHolders;
    public Sprite arrowUpSprite;
    public Sprite arrowDownSprite;
    public Sprite arrowRightSprite;
    public Sprite arrowLeftSprite;
    
    

    public TextMeshProUGUI correctInputText;

    float inputTimer;
    float timeLeft =5;

    public InputActionReference rightArrowInput;
    public InputActionReference leftArrowInput;
    public InputActionReference downArrowInput;
    public InputActionReference upArrowInput;

    public AudioSource pianoAudioSource;
    public AudioClip pianoSong;
    public AudioClip pianoTang;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       pianoAudioSource.clip = pianoSong;
        pianoAudioSource.loop = true;
        pianoAudioSource.Play();

        PlayPiano();
    }

    void PlayPiano()
    {
       inputTimer = 0f;
       StartCoroutine(CheckInputs());
    }

    IEnumerator CheckInputs()
    {
        inputTimer = 0;

        InputActionReference[] inputPool = new InputActionReference[] 
        { 
            rightArrowInput, 
            leftArrowInput, 
            downArrowInput, 
            upArrowInput 
        };

        InputActionReference[] requiredSequence = new InputActionReference[arrowImageHolders.Length];

        
        for (int i = 0; i < requiredSequence.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, inputPool.Length);
            requiredSequence[i] = inputPool[randomIndex]; 
        }

        SetArrowImagePositions(requiredSequence);

        
       
       //go through the array of actions and make sure player has pressed those keys in that order before time runs out
       for(int i = 0; i < requiredSequence.Length; i++)
        {
            InputAction currentRequiredInput = requiredSequence[i].action;
            bool inputCompleted = false;
            

            while(inputTimer < timeLeft && inputCompleted == false)
            {
                if (currentRequiredInput.WasPressedThisFrame())
                {
                    inputCompleted = true;

                    
                    
                    Color tempColour = arrowImageHolders[i].color;
                    tempColour.a = 0.5f;
                    arrowImageHolders[i].color = tempColour;
                }
                else if(upArrowInput.action.WasPressedThisFrame() || downArrowInput.action.WasPressedThisFrame() 
                || rightArrowInput.action.WasPressedThisFrame() || leftArrowInput.action.WasPressedThisFrame())
                {
                    correctInputText.text = "OFFF KEEEY";
                    pianoAudioSource.Stop();
                    pianoAudioSource.PlayOneShot(pianoTang);
                    PlayPiano();
                    yield break;
                }
                
                inputTimer += Time.deltaTime;
                yield return null;
            }

            //time check
            if(inputTimer >= timeLeft)
            {
                correctInputText.text = "OFFF KEEEY";
                pianoAudioSource.Stop();
                pianoAudioSource.PlayOneShot(pianoTang);
                PlayPiano();
                yield break;
            }
        }
        
        correctInputText.text = "so melodic";
        PlayPiano();
       
    }
    
    void SetArrowImagePositions(InputActionReference[] newArrowInputs)
    {
        for (int i = 0; i < newArrowInputs.Length; i++)
        {
            string actionName = newArrowInputs[i].action.name;

            //set sprite based on action name in randomized action array
            if (actionName.Contains("Right")) arrowImageHolders[i].sprite = arrowRightSprite;
            else if (actionName.Contains("Left")) arrowImageHolders[i].sprite = arrowLeftSprite;
            else if (actionName.Contains("Down")) arrowImageHolders[i].sprite = arrowDownSprite;
            else if (actionName.Contains("Up")) arrowImageHolders[i].sprite = arrowUpSprite;
            
            // make fully visable
            Color tempColour = arrowImageHolders[i].color;
            tempColour.a = 1f;
            arrowImageHolders[i].color = tempColour;
            
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inputTimer);
    }
    
}
