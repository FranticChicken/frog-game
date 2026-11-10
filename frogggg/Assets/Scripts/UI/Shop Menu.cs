using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopMenu : MonoBehaviour
{
    public Button escapeButton;
    public Button shopItemButton1;
    public Button shopItemButton2;
    public Image shopMenu;
    public TextMeshProUGUI item1Text;
    public TextMeshProUGUI itemText2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escapeButton.onClick.AddListener(OnEscapeButtonClick);
        shopItemButton1.onClick.AddListener(OnShopItemButton1Click);
        shopItemButton2.onClick.AddListener(OnShopItemButton2Click);
    }

    void OnEscapeButtonClick()
    {
        shopMenu.gameObject.SetActive(false);
        //player movement resumed
        //cursor locked
    }

    void OnShopItemButton1Click()
    {
        //if enough money, buy (change text to sold)
    }

    void OnShopItemButton2Click()
    {
        //if enough money, buy (change text to sold)
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
