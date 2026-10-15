using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCards : MonoBehaviour, IDropHandler
{
    //this script is placed Canvas -> Card Holders -> Placed Cards -> Card Slot 1, 2, 3, & 4

    public CardRandomization cardRandomizationScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped on Slot");

        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            //check which slot we are   
            Match foundSlotNumber = Regex.Match(gameObject.name, @"\d");
            //check which dragged card we are 
            Match foundPlayerCardNumber = Regex.Match(draggedObject.name, @"\d");

            if (foundSlotNumber.Success && foundPlayerCardNumber.Success)
            {
                int parsedSlotNumber = int.Parse(foundSlotNumber.Value);
                int parsedPlayerCardNumber = int.Parse(foundPlayerCardNumber.Value);

                //check which number is on the cards 1-13
                Match foundPlacedCardValue = Regex.Match(gameObject.GetComponent<Image>().sprite.texture.name, @"\d+");
                Match foundPlayerCardValue = Regex.Match(draggedObject.GetComponent<Image>().sprite.texture.name, @"\d+");
                int parsedPlacedCardValue = int.Parse(foundPlacedCardValue.Value);
                int parsedPlayerCardValue = int.Parse(foundPlayerCardValue.Value);
                Debug.Log($"ACTUAL SPRITE NAMES -> Placed Sprite: '{gameObject.GetComponent<Image>().sprite?.texture?.name}' | Player Sprite: '{draggedObject.GetComponent<Image>().sprite?.texture?.name}'");
                Debug.Log($"Placed: {parsedPlacedCardValue} | Player: {parsedPlayerCardValue} | Diff: {Mathf.Abs(parsedPlacedCardValue - parsedPlayerCardValue)}");
                Debug.Log(Mathf.Abs(parsedPlacedCardValue - parsedPlayerCardValue));

                //if number is +1 or -1 or +12 or -12 than other number, can place
                if(Mathf.Abs(parsedPlacedCardValue - parsedPlayerCardValue) == 1 ||  Mathf.Abs(parsedPlacedCardValue - parsedPlayerCardValue) == 12)
                {
                    //send that info to card randomization script to be placed
                    cardRandomizationScript.UpdateCards(parsedSlotNumber - 1, parsedPlayerCardNumber - 1);
                }
                
            }
            else
            {
                Debug.Log("no number found in name");
            }
           
            
            

    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
