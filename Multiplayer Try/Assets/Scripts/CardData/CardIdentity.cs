using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardIdentity {

    public string NameTypeCard;
  //  public new string NameCard;
    public int IndeParentCard;

    [Space]
    public Sprite Icon;

    public int[] Stats = new int[4]; // Attack, Defense, Speed, Health
    public int[] StatsSort = new int[4]; // Sorting Stats
    public string BestStats;

    public int StateRarity;

    public enum ClassCard {
        Goblin, Human, Elf
    }

    public ClassCard StateID;

    public string cardparentName;
    public CardParent cardParent;
}
