using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

    private GameObject playerModel;
    private GameObject playerJetpackModel;
    private PlayerController controller;

    private List<Pickup> inventoryList = new List<Pickup>();
    private int currPickupIndex;

    void Awake()
    {
        playerModel = this.gameObject.transform.GetChild(0).gameObject;
        playerJetpackModel = this.gameObject.transform.GetChild(1).gameObject;
        controller = gameObject.GetComponent<PlayerController>();
        playerJetpackModel.SetActive(false);
    }

    //When the player collides with an object that has the "Pickup" tag, adds it to the inventory
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            AddPickup(other.gameObject.GetComponent<Pickup>());
        }
    }

    //Adds an item with the "Pickup" script to the list of pickups up to a maximum of 4
    /*When added to the inventory the item is deactivated, the models are swapped
    the item is equipped and the inventory is displayed on the UI*/
    private void AddPickup(Pickup pickup)
    {
        if (inventoryList.Count < 4)
        {
            inventoryList.Add(pickup);
            pickup.gameObject.SetActive(false);
            playerModel.SetActive(false);
            playerJetpackModel.SetActive(true);
            controller.EquipJetpack();
            DisplayInventory();
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    //Displays the pickup's sprite on the slot it is in
    /*Does this by going through the inventory list and
     setting the first UI slot equal to the sprite of the first item in the list.
    it then repeats this until it has goe through the entire inventory list*/
    private void DisplayInventory()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            switch (i)
            {
                case 0:
                    slot1.sprite = inventoryList[i].pickupSprite;
                    break;
                case 1:
                    slot2.sprite = inventoryList[i].pickupSprite;
                    break;
                case 2:
                    slot3.sprite = inventoryList[i].pickupSprite;
                    break;
                case 3:
                    slot4.sprite = inventoryList[i].pickupSprite;
                    break;
            }
        }
    }
}
