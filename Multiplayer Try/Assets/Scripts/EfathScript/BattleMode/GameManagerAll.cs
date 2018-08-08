using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAll : MonoBehaviour {

    public static GameManagerAll _instance;

    private void Awake()
    {
        
        if (_instance == null)
        {
           _instance = this;
           DontDestroyOnLoad(gameObject);
        }
        else {
           Destroy(gameObject);
        }
   
    }

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
