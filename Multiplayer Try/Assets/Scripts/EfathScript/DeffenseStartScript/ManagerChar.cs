using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerChar : MonoBehaviour {
    public static ManagerChar instance;
   public List<GameObject> CharIndex;
    
    public  int CharIndexGlobal;

    public GameObject BattleModeUI;
    public GameObject StrategyModeUI;
    // public Camera[] Camera
    // Use this for initialization
    public void Awake()
    {
        instance = this;
    }

    void Start () {
        BattleModeUI.SetActive(false);
        StrategyModeUI.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
          
    }

    public void SetIndexChar(int Index)
    {
        CharIndexGlobal = Index;
    }
}
