using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ManagerCard : MonoBehaviour {

    public List<CardParent> CardIdentityCollection = new List<CardParent>();
    public List<bool> CardIdentityChecked;

    public ManagerSelectCard selectCardMenu;

    public static ManagerCard instance;

    public enum TypeCard {
        nullstate = 0  , 
         Warrior , Tanker, Assasin
    }


    public enum StatsCard
    {
        nullstate = 0,
        Attack, Defender, Speed, health
    }


    [System.Serializable]
    public class StatsCardClass
    {
        public StatsCard StateIdStatsCard;
        public int randomStats ;
    }

    public List<StatsCardClass> statsCollection = new List<StatsCardClass>();

    public List<TypeCard> StateId;

    public GameObject MenuRandom;
    public GameObject MenuChooseCard;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        MenuRandom.SetActive(true);
        MenuChooseCard.SetActive(false);
    }

    public void RandomCard()
    {
        statsCollection.Clear();

        CardParent cards = ScriptableObject.CreateInstance("CardParent") as CardParent;
        CardParent Cards = new CardParent();

        for (int i = 0; i < 3; i++)
        {
            statsCollection.Add(new StatsCardClass());
            statsCollection[i].randomStats = (Random.Range(1, 9));
            statsCollection[i].StateIdStatsCard = (StatsCard)i + 1;
        }

        for (int i = 0; i < 3; i++)
        {
            cards.cardiden.Stats[i] = statsCollection[i].randomStats;
        }

        statsCollection.Sort(delegate (StatsCardClass a, StatsCardClass b) {
            return a.randomStats.CompareTo(b.randomStats);
        });

        for (int i = 0; i < 3; i++)
        {
            if (statsCollection[2].StateIdStatsCard == (StatsCard) i+1 )
            {
                cards.cardiden.cardparentName = ((TypeCard) i + 1).ToString();
                cards.NameCardType = ((TypeCard)i + 1).ToString();
            }
            cards.cardiden.StatsSort[i] = statsCollection[i].randomStats;
            cards.cardiden.BestStats = statsCollection[i].StateIdStatsCard.ToString();
        }

        statsCollection.Add(new StatsCardClass());
        statsCollection[3].randomStats = (Random.Range(3, 5));
        statsCollection[3].StateIdStatsCard = (StatsCard)4;
        cards.cardiden.Stats[3] = statsCollection[3].randomStats;

        cards.cardiden.StateID = (CardIdentity.ClassCard)cards.cardiden.IndeParentCard;
        cards.cardiden.NameTypeCard = cards.cardiden.StateID.ToString(); 
        cards.cardiden.cardParent = cards;

        CardIdentityCollection.Add(cards);
        CardIdentityChecked.Add(false);

        SaveCard(cards);

        selectCardMenu.InserData = true;

        MenuRandom.SetActive(false);
        MenuChooseCard.SetActive(true); 
    }


    public void SaveCard(CardParent card)
    {
        PlayerPrefs.SetString("SaveCard" + CardIdentityCollection.Count, card.NameCardType);
        PlayerPrefs.SetInt("SaveCardInt" + CardIdentityCollection.Count,card.IndexCard);

        CardIdentity[] car = new CardIdentity[CardIdentityCollection.Count];

        for (int i = 0; i < CardIdentityCollection.Count; i++)
        {
            car[i] = new CardIdentity();
            car[i].NameTypeCard = CardIdentityCollection[i].cardiden.NameTypeCard;
            car[i].IndeParentCard = CardIdentityCollection[i].cardiden.IndeParentCard;
            car[i].cardparentName = CardIdentityCollection[i].NameCardType;
            car[i].cardParent = CardIdentityCollection[i];

            for (int j = 0; j < 4; j++)
            {
                car[i].Stats[j] = CardIdentityCollection[i].cardiden.Stats[j];
            }
        }
        string json = JsonHelper.ToJson(car,true);
   
         File.WriteAllText(Application.dataPath + "/PlayerData.json", json + "\n");
    }

    public void LoadCard()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/PlayerData.json");

        CardIdentity[] player = JsonHelper.FromJson<CardIdentity>(jsonString);
        for (int i = 0; i < player.Length; i++)
        {
            CardParent curr = new CardParent();
            curr.name = player[i].cardparentName;
            CardIdentityCollection.Add(curr);
            Debug.Log(player[i].NameTypeCard);
        }
    }

    public void BackToRandom() {
        for (int i = 0; i < selectCardMenu.ParentCardPrefab.transform.childCount; i++)
        {
          
            Destroy(selectCardMenu.ParentCardPrefab.transform.GetChild(i).gameObject);
           
        }
        selectCardMenu.CardinMenu.Clear();
        selectCardMenu.CardinMenuGameobject.Clear();
        MenuRandom.SetActive(true);
        MenuChooseCard.SetActive(false);
    }
}
