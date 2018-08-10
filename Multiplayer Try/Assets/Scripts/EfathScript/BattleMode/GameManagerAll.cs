using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAll : MonoBehaviour {

    public static GameManagerAll _instance;

    public GameObject CharPrefabAttacker;
    public GameObject CharPrefabDefense;

    public CharacterBehaviour CharBehaveAttacker;
    public CharacterBehaviour CharBehaveDefense;

    public DirectionControl CharDirectAttacker;
    public DirectionControl CharDirectDefense;

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
