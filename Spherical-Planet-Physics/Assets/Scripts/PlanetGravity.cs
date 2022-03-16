using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private GameObject[] everything;
    private LinkedList<GameObject> ListOfObjects;

    public float mass = 10000000f;
    public float gConst = 9.8f;

    void Start()
    {
        everything = GameObject.FindObjectsOfType<GameObject>();
        ListOfObjects = new LinkedList<GameObject>(everything);
        foreach (GameObject go in ListOfObjects)
        {
            if (go.CompareTag("Planet"))
            {
                ListOfObjects.Remove(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in ListOfObjects)
        {
            if (go.GetComponent<Rigidbody>() != null)
            {
                Rigidbody gorb = go.GetComponent<Rigidbody>();
                gorb.AddForce((transform.position - go.transform.position) * (gConst * ((mass * gorb.mass) / Mathf.Pow(Vector3.Distance(transform.position, go.GetComponent<Transform>().position) , 2f))));
            }
        }
    }
}
