using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChanger : MonoBehaviour {

    public Material solid;
    public Material Trans;
    private Material current;
    public Transform maincams;
    public float Distance;
	void Start ()
    {
        //current = GetComponent<Material>();
        current = GetComponent<Renderer>().material=solid;
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        float dist = maincams.transform.position.z - transform.position.z;
        
        //if (maincams.position.z>=transform.position.z-19)
        if (dist>=-Distance)
        {
            current = GetComponent<Renderer>().material = Trans;
        }
        else
        {
            current = GetComponent<Renderer>().material = solid;
        }
    }
}
