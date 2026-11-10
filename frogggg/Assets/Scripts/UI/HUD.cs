using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUD : MonoBehaviour
{
    public Image coinImage;
    public TextMeshProUGUI moneyText;
    int moneyAmount;
    public PlayerInteraction playerInteractionScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyAmount = 0;
    }

    public void UpdatePlayerMoney(int amount)
    {
        moneyAmount += amount;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + moneyAmount;

        if(playerInteractionScript.gaming == true)
        {
            coinImage.gameObject.SetActive(false);
            moneyText.gameObject.SetActive(false);
        }
        else
        {
            coinImage.gameObject.SetActive(true);
            moneyText.gameObject.SetActive(true);
        }
    }
}
