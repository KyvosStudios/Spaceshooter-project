using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public LineRenderer lineRenderer;
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }
	
	void Update ()
    {
		lineRenderer.enabled = Controls.LeftTrigger;
    }
}
