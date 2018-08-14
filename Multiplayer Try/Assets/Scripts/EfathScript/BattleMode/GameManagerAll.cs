using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAll : MonoBehaviour {

    public static GameManagerAll _instance;

    public TurnBaseController TurnManager;

    [Space]
    public GameObject CharPrefabAttacker;
    public GameObject CharPrefabDefense;
    public int[] CharPrefabIndex;


    [Space]
    public int[] AttackerData; //speed,damage,defense,health
    public int[] DefenderData; //speed,damage,defense,health

    public bool HoldMovingChar;


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
        TurnManager = FindObjectOfType<TurnBaseController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
