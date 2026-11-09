using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button mainMenuButton;
    public Button resumeButton;
    public Image pauseMenu;
    public PlayerInteraction playerInteractionScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    void OnResumeButtonClick()
    {
        pauseMenu.gameObject.SetActive(false);
        playerInteractionScript.isInPauseMenu = false;
    }

    void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
