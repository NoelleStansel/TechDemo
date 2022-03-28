using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private GameObject[] everything;
    private LinkedList<GameObject> ListOfObjects;

    public float mass = 500f;
    public float gConst = 9.8f;

    void Start()
    {
        //Put every game object in a linked list
        everything = GameObject.FindObjectsOfType<GameObject>();
        ListOfObjects = new LinkedList<GameObject>(everything);
        
        //Remove "Planet"s and no rigidbody objects
        foreach (GameObject go in ListOfObjects)
        {
            if (go.CompareTag("Planet") || go.GetComponent<Rigidbody>() == null)
            {
                ListOfObjects.Remove(go);
            }
        }
    }

    void FixedUpdate()
    {
        //Apply force to everything with a rigidbody
        foreach (GameObject go in ListOfObjects)
        {
            if (go.GetComponent<Rigidbody>() != null)
            {
                Rigidbody gorb = go.GetComponent<Rigidbody>();
                //Gravity formula F = G(m1 * m2) / r^2
                float appliedForce = (gConst * ((mass * gorb.mass) / Mathf.Pow(Vector3.Distance(transform.position, go.GetComponent<Transform>().position), 2f)));
                gorb.AddForce((transform.position - go.transform.position) * appliedForce);


                // Track strongest planet acting on player
                if (go.tag == "Player" )
                {
                    PlayerController pc = go.GetComponent<PlayerController>();
                    if (appliedForce > pc.strongestPlanetForce + offsetConstant)
                    {
                        pc.strongestPlanetForce = appliedForce;
                        pc.StrongestPlanet = this;
                    }
                }
            }
        }
    }
}
