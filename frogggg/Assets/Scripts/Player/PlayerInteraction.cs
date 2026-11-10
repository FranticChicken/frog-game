using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public InputActionReference interactAction;
    public InputActionReference escapeAction; 
    public InputActionReference tabAction;
    public PianoGame pianoGameScript;
    public PouringGame pouringGameScript;
    public CardRandomization cardGameScript;

    public GameObject cardGameObject;
    public GameObject pianoGameObject;
    public GameObject pouringGameObject;

    bool canGame;
    float rayDistance = 10.0f;
    [HideInInspector]
    public bool gaming;
    public bool playingPouringGame;

    //audio
    public AudioSource audioSrc;
    public AudioSource pianoAudioSource;
    public AudioClip jazz;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public Image pauseMenu; 

    public Image shopMenu;

    public bool isInShopMenu;
   

   public bool isInPauseMenu;
   public CameraController camScript; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInPauseMenu = false;
        isInShopMenu = false;
        canGame = false;
        gaming = false;
        playingPouringGame = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameTrigger"))
        {
           canGame = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameTrigger"))
        {
            canGame = false;
        }
        
    }

    //if player is in trigger collider and looking at correct object while pressing E

    // Update is called once per frame
    void Update()
    {

        if (canGame)
        {
            
        
            RaycastHit hit;

            
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, ~LayerMask.GetMask("Player")))
            {
                // Check if the hit object has the specific tag
                if (hit.collider.CompareTag("PourGame"))
                {
                    if (interactAction.action.WasPressedThisFrame())
                    {
                        Debug.Log("Playing Game");
                        gaming = true;
                        playingPouringGame = true;
                        pouringGameScript.StartGame();
                        camScript.SwitchCam(1,15);
                        
                        
                    }
                    
                }

                if (hit.collider.CompareTag("PianoGame"))
                {   
                    if (interactAction.action.WasPressedThisFrame())
                    {
                        gaming = true;
                        Debug.Log("Playing Game");
                        pianoGameScript.StartGame();
                        camScript.SwitchCam(0,15);
                    }
                    
                }

                if (hit.collider.CompareTag("CardGame"))
                {
                    if (interactAction.action.WasPressedThisFrame())
                    {
                        Debug.Log("Playing Game");
                        gaming = true;
                        cardGameScript.StartCardGame();
                        camScript.SwitchCam(2,15);
                        
                        //start game in proper script
                    }
                    
                }
            }
        }

        // Draw the ray in the scene
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        if (escapeAction.action.WasPressedThisFrame())
        {
            Cursor.lockState = CursorLockMode.None;
            cardGameObject.SetActive(false);
            cardGameScript.ExitGame();

            pouringGameScript.ExitGame();

            pianoGameObject.SetActive(false);
            pianoGameScript.ExitGame();
            pianoAudioSource.Stop();
            //gaming = false;
            //playingPouringGame =false;
            
            if(gaming == false && isInShopMenu == false)
            {
                isInPauseMenu = true;
                pauseMenu.gameObject.SetActive(true);
            }
        }


        if(gaming == true)
        {
            audioSrc.Pause();
            
        }
        else
        {
            audioSrc.UnPause();
        }

        if (tabAction.action.WasPressedThisFrame())
        {
            if(gaming == false && isInPauseMenu == false)
            {
                shopMenu.gameObject.SetActive(true);
                isInShopMenu = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
