using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{

    public GameObject p;

    public static GameObject GameIns;
	// Use this for initialization
	void Start () {
	    if (null == GameIns)
	    {
	        GameIns = GameObject.Instantiate(p);
	    }
        GameIns.GetComponent<Game>().Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
