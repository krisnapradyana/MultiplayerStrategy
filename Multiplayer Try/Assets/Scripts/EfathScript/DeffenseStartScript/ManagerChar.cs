using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerChar : MonoBehaviour {
    public static ManagerChar instance;
   public List<GameObject> CharIndex;
    
    public  int CharIndexGlobal;
    // Use this for initialization
    public void Awake()
    {
        instance = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIndexChar(int Index)
    {
        CharIndexGlobal = Index;
    }
}
