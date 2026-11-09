using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);

    }

    void OnQuitButtonClick()
    {
        Application.Quit();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
