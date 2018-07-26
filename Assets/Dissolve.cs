using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    public float step;
    public float thick;
    public float dissolve;
    private Renderer rende;
	void Start ()
    {
        rende = GetComponent<Renderer>();
        rende.material.SetFloat("Vector1_554C424B", thick);
        rende.material.SetFloat("Vector1_53AA2237", dissolve);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.A)&&thick<1&&dissolve<1)
        {
            thick +=step*Time.deltaTime;
            dissolve += step*Time.deltaTime;
            print("thick :" + thick);
            print("dissolve :" + dissolve);
            print("step :" + step);

        }
        rende.material.SetFloat("Vector1_554C424B", thick);
        rende.material.SetFloat("Vector1_53AA2237", dissolve);
    }
}
