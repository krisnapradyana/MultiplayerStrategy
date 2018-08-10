using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBattleMode : MonoBehaviour {

    public GameObject CobaPrefab;

    public int IndexTurn;
    public int SaveSuitAttacker;
    public int SaveSuitDefender;
    // Use this for initialization
    void Start() {
        Debug.Log("Masuk Battle Mode");
        Instantiate(CobaPrefab, transform.position, transform.rotation);


    }

    // Update is called once per frame
    void Update() {

    }

    public void ButtonSuit(int index)
    {
  
        if (IndexTurn == 0)
        {
            SaveSuitAttacker = index;
        } else if (IndexTurn ==1)
        {
            SaveSuitDefender = index;
        }

        IndexTurn += 1;
        if (IndexTurn == 2) {
            IndexTurn = 0;
        }
     }
}
