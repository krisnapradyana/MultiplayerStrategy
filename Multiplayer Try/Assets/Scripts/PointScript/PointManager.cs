using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointManager : MonoBehaviour {

    public enum PlacedCharacter
    {
        First = 0, Second = 1, Third = 2, Fourth = 3
    }

    public PlacedCharacter placedCharacter;

    CharacterSelectLogic ManagerCharSelectLogic;

    public int IndexLimit;
    public List<int> PrevIndexLimitCollect;
    public int PrevLimit;
    public int TotalPlaced;
    public bool[] CheckPlaced;
    // Use this for initialization

    void Start()
    {
        InitConstructor();
    }

    void InitConstructor()
    {
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
    }

    // Update is called once per frame
    void Update() {

    }
}
