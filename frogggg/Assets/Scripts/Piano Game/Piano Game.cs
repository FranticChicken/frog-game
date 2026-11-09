using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using NUnit.Framework;


public class PianoGame : MonoBehaviour
{
    
    public Image [] arrowImageHolders;
    public Sprite arrowUpSprite;
    public Sprite arrowDownSprite;
    public Sprite arrowRightSprite;
    public Sprite arrowLeftSprite;
    
    public GameObject pianoGameObject;
    

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

    public PlayerInteraction playerInteractionScript;

    [HideInInspector]
    public bool isPlayingPianoGame;

    Coroutine pianoCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame()
    {
        if(pianoCoroutine != null)
        {
            StopCoroutine(pianoCoroutine);
        }
        pianoCoroutine = StartCoroutine(PlayPianoGame());
    }

    public void ExitGame()
    {
       if(pianoCoroutine != null)
        {
            StopCoroutine(pianoCoroutine);
            pianoCoroutine = null;
        }

        //prepare for out of minigame experience lol
        //music
        //ui
        pianoGameObject.SetActive(false);
    }

    public IEnumerator PlayPianoGame()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.None;

        //make UI elements appear
        pianoGameObject.SetActive(true);

        //music start (MAY WANT THIS tO START LATER)
        pianoAudioSource.clip = pianoSong;
        pianoAudioSource.loop = true;
        pianoAudioSource.Play();
        

        isPlayingPianoGame = true;

        //set array of possible inputs
        InputActionReference[] inputPool = new InputActionReference[] 
        { 
            rightArrowInput, 
            leftArrowInput, 
            downArrowInput, 
            upArrowInput 
        };

        while (isPlayingPianoGame)
        {
            
        
            //set array that will change the required inputs each interaction
            InputActionReference[] requiredSequence = new InputActionReference[arrowImageHolders.Length];

            //choose random input required
            for (int i = 0; i < requiredSequence.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, inputPool.Length);
                requiredSequence[i] = inputPool[randomIndex]; 
            }

            //show the input required through UI images
            SetArrowImagePositions(requiredSequence);

            //start timer at 0
            inputTimer = 0;
            bool failedInputs = false;

            //go through the array of actions and make sure player has pressed those keys in that order before time runs out
            for(int i = 0; i < requiredSequence.Length; i++)
            {
                InputAction currentRequiredInput = requiredSequence[i].action;
                bool inputCompleted = false;
                

                
                while(inputTimer < timeLeft && inputCompleted == false && failedInputs == false)
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
                        failedInputs = true;
                        break;
                    }
                    
                    inputTimer += Time.deltaTime;
                    yield return null;
                }

                //time check
                if(failedInputs || inputTimer >= timeLeft)
                {
                    correctInputText.text = "OFFF KEEEY";
                    pianoAudioSource.Stop();
                    pianoAudioSource.PlayOneShot(pianoTang);
                    failedInputs = true;
                    break;
                }
            }

            if (failedInputs)
            {
                isPlayingPianoGame = false;
            }
        }
        
        ExitGame();
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
        
    }
    
}
