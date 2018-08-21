using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardType", menuName = "Card/Player", order = 1)]
public class CardParent : ScriptableObject {

    public new string NameCardType = "Name";
    public int QualityPlayer;

    public int IndexCard;

    public CardIdentity cardiden =  new CardIdentity();

    // Use this for initialization
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
