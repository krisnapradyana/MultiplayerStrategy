using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdentity : MonoBehaviour {

    public MeshRenderer DefenderMaterial;
    public MeshRenderer AttackerMaterial;

	// Use this for initialization
	void Start () {
        AttackerMaterial.material= GameManagerAll._instance.CharPrefabAttacker.GetComponent<MeshRenderer>().material;
        DefenderMaterial.material = GameManagerAll._instance.CharPrefabDefense.transform.GetChild(1).GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
