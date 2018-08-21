using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ManagerCard : MonoBehaviour {


    public List<CardParent> CardIdentityCollection = new List<CardParent>();
   
    // Use this for initialization

 

    public enum TypeCard {
        nullstate = 0 
            
        , Mage , Asssasin, Warrior

       }

    public enum StatsCard
    {
        nullstate = 0,
        Attack, Defender, Speed
    }

    [System.Serializable]
    public class StatsCardClass
    {
        public StatsCard StateIdStatsCard;
        public int randomStats ;
    }


    public List<StatsCardClass> statsCollection = new List<StatsCardClass>();

    public List<TypeCard> StateId;



	void Start () {
        //RandomCard();

      //  LoadCard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RandomCard()
    {
        statsCollection.Clear();
        // int random = Random.Range(0,5);
        int randomIndex = Random.Range(1,4);
        int randomIndexIden = Random.Range(0,3);

      // dmg, health, speed, agility, intelligence

        
        //StateId = (TypeCard)randomIndex;

        
        CardParent cards = ScriptableObject.CreateInstance("CardParent") as CardParent;

        CardParent Cards = new CardParent();

        for (int i = 0; i < 3; i++)
        {
            statsCollection.Add(new StatsCardClass());
            statsCollection[i].randomStats = (Random.Range(1, 9));
            statsCollection[i].StateIdStatsCard = (StatsCard)i + 1;

            // randomStats.Add(Random.Range(1, 9));
            // randomStats[i] = Random.Range(1, 9);
            //   cards.cardiden.Stats[i] = randomStats[i];

        }

        statsCollection.Sort(delegate (StatsCardClass a, StatsCardClass b) {
            return a.randomStats.CompareTo(b.randomStats);
        });

        for (int i = 0; i < 3; i++)
        {
            if (statsCollection[0].StateIdStatsCard == (StatsCard) i+1 )
            {
                cards.cardiden.cardparentName = ((TypeCard) i + 1).ToString();
                cards.NameCardType = ((TypeCard)i + 1).ToString();
                Debug.Log(cards.cardiden.cardparentName);
            }

            cards.cardiden.Stats[i] = statsCollection[i].randomStats;
            Debug.Log(cards.cardiden.Stats[i]);
        }

        cards.cardiden.StateID = (CardIdentity.ClassCard)cards.cardiden.IndeParentCard;

        cards.cardiden.NameTypeCard = cards.cardiden.StateID.ToString();
      
        cards.cardiden.cardParent = cards;

        cards.cardiden.IndeParentCard = randomIndexIden;

        CardIdentityCollection.Add(cards);

        SaveCard(cards);

    }

    public void SaveCard(CardParent card)
    {
   
        //  PlayerPrefs.SetInt("TotalCard",TotalCard);
        PlayerPrefs.SetString("SaveCard" + CardIdentityCollection.Count, card.NameCardType);
        PlayerPrefs.SetInt("SaveCardInt" + CardIdentityCollection.Count,card.IndexCard);


        //  List<int> a = new List<int>();

        CardIdentity[] car = new CardIdentity[CardIdentityCollection.Count];

        for (int i = 0; i < CardIdentityCollection.Count; i++)
        {
            car[i] = new CardIdentity();
            car[i].NameTypeCard = CardIdentityCollection[i].cardiden.NameTypeCard;
            car[i].IndeParentCard = CardIdentityCollection[i].cardiden.IndeParentCard;
            car[i].cardparentName = CardIdentityCollection[i].NameCardType;
            car[i].cardParent = CardIdentityCollection[i];

            for (int j = 0; j < 3; j++)
            {
                car[i].Stats[j] = CardIdentityCollection[i].cardiden.Stats[j];
            }
         
        }
        

        string json = JsonHelper.ToJson(car,true);

        Debug.Log(json);
      
            File.WriteAllText(Application.dataPath + "/PlayerData.json", json + "\n");

       // TopLevelIntArray topLevelArray = new TopLevelIntArray() { array = array };

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


}
