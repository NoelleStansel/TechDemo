using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject playerModel;
    private GameObject playerJetpackModel;

    void Awake()
    {
        playerModel = this.gameObject.transform.GetChild(0).gameObject;
        playerJetpackModel = this.gameObject.transform.GetChild(1).gameObject;

        playerJetpackModel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            Destroy(other.gameObject);
            playerModel.SetActive(false);
            playerJetpackModel.SetActive(true);
        }
    }
}
