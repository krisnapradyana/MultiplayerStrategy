using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdentity : MonoBehaviour {

    public Renderer DefenderMaterial;
    public Renderer AttackerMaterial;

	// Use this for initialization
	void Start () {
        AttackerMaterial.material= GameManagerAll._instance.CharPrefabAttacker.GetComponent<MeshRenderer>().sharedMaterial;
        DefenderMaterial.material = GameManagerAll._instance.CharPrefabDefense.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
