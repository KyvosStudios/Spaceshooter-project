using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectMatChanger : MonoBehaviour {

    public float Distance;
    public Transform maincams;
    void Start ()
    {
       
        Material mat = GetComponent<Renderer>().material;
        mat.SetFloat("_Mode", 0);
        //Debug.Log(mat.GetFloat("_Mode"));



    }
	
	// Update is called once per frame
	void Update ()
    {
        float dist = maincams.transform.position.z - transform.position.z;

        if (dist >= -Distance)
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            this.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            
            //Debug.Log(mat.GetFloat("_Mode"));
        }
        else
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetFloat("_Mode", 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            this.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
    }
}

