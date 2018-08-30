using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ManagerSelectCard : MonoBehaviour {

    public bool InserData;
    public List<CardParent> CardinMenu;
    public List<GameObject> CardinMenuGameobject;
    public List<CardParent> CardinChar = new List<CardParent>();

    public GameObject CardPrefab;
    public GameObject ParentCardPrefab;
    public GameObject ParentCardChar;

    public Text[] DetailMenuStats;

    public GameObject[] CharButton;
    public int IndexCardtoChar;
    public int IndexCardInMenu;
    public int IndexCardAll;

    public int[] IndexCardAllCollection;
    public int[] IndexCardAllCollectionMenu;

    [System.Serializable]
    public class UndoSystem {
        public int IndexChar;
        public CardParent cardparent;
    }

    public UndoSystem[] undosystem =  new UndoSystem[4];
    public List<CardParent> saveundoSystem;

    public int CountCardinChar;

    // Use this for initialization

    void Start() {
        for (int i = 0; i < CharButton.Length; i++)
        {
            CharButton[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        ButtonForChar();
    }

    // Update is called once per frame
    void Update() {
        if (InserData)
        {
            ImportDataCard();
        }
    }

    

    void ImportDataCard() {
        int temp = 0;
        for (int i = 0; i < ManagerCard.instance.CardIdentityCollection.Count; i++)
        {
            GameObject build = null;
            CardinMenuGameobject.Add(build);
            CardinMenu.Add(ManagerCard.instance.CardIdentityCollection[i]);
            if (!ManagerCard.instance.CardIdentityChecked[i])
            {
                int a = i;
                int b = temp;

                build = Instantiate(CardPrefab, transform.position, transform.rotation) as GameObject;

                CardinMenuGameobject[i] = build;
                build.transform.parent = ParentCardPrefab.transform;
                build.transform.GetChild(0).GetComponent<Text>().text = "" + ManagerCard.instance.CardIdentityCollection[i].cardiden.BestStats;
                build.transform.GetChild(1).GetComponent<Text>().text = "" + ManagerCard.instance.CardIdentityCollection[i].cardiden.StatsSort[2];
                build.GetComponent<Button>().onClick.AddListener(delegate { DetailMenu(a, a); });

                temp++;
            }
        }
        InserData = false;
    }

    void ButtonForChar()
    {
        for (int i = 0; i < CharButton.Length; i++)
        {
            int temp = i;
            CharButton[i].GetComponent<Button>().onClick.AddListener(delegate { PressChar(temp, -1); });
        }
    }

    public void DetailMenu(int Index, int indexInMenu)
    {
        IndexCardtoChar = Index;
        IndexCardInMenu = indexInMenu;
        Debug.Log(Index);
        Debug.Log(indexInMenu);
        DetailMenuStats[0].text = "" + ManagerCard.instance.CardIdentityCollection[Index].cardiden.cardparentName;
        for (int i = 1; i < DetailMenuStats.Length; i++)
        {
            DetailMenuStats[i].text = "" + ManagerCard.instance.CardIdentityCollection[Index].cardiden.Stats[i - 1];
        }
    }

    public void PressChar(int index, int indexFull)
    {
        if (IndexCardtoChar == -1)
        {
            
            return;
        }
        else if (CardinMenu.Count > 0)
        {
            if (CardinChar[index] != null)
            {
                CardParent tempCard = new CardParent();
                CardParent tempCardMenu = new CardParent();


                Destroy(CardinMenuGameobject[IndexCardInMenu].gameObject);
                GameObject build;
                build = Instantiate(CardPrefab, transform.position, transform.rotation) as GameObject;
                build.transform.parent = ParentCardPrefab.transform;
                build.transform.GetChild(0).GetComponent<Text>().text = "" + CardinMenu[IndexCardAllCollectionMenu[index]].cardiden.BestStats;
                build.transform.GetChild(1).GetComponent<Text>().text = "" + CardinMenu[IndexCardAllCollectionMenu[index]].cardiden.StatsSort[2];

                int TempBuild = IndexCardAllCollection[index];
                int TempBuild2 = IndexCardAllCollectionMenu[index];
                build.GetComponent<Button>().onClick.AddListener(delegate { DetailMenu(TempBuild, TempBuild2); });

                CardinMenuGameobject[IndexCardAllCollectionMenu[index]] = build;
                CardinChar[index] = CardinMenu[IndexCardInMenu];
                CharButton[index].GetComponentInChildren<Text>().text = "" + CardinChar[index].cardiden.cardparentName;

                ManagerCard.instance.CardIdentityChecked[TempBuild] = false;
                ManagerCard.instance.CardIdentityChecked[IndexCardtoChar] = true;

                IndexCardAllCollection[index] = IndexCardtoChar;
                IndexCardAllCollectionMenu[index] = IndexCardInMenu;
                IndexCardtoChar = -1;
                Debug.Log(CardinMenu[IndexCardAllCollectionMenu[index]].cardiden.BestStats);

                undosystem[index].cardparent = CardinChar[index];
                return;
            }
            CharButton[index].GetComponentInChildren<Text>().text = "" + ManagerCard.instance.CardIdentityCollection[IndexCardtoChar].cardiden.cardparentName;
            CardinChar[index] = CardinMenu[IndexCardInMenu];

            Destroy(CardinMenuGameobject[IndexCardInMenu].gameObject);
            ManagerCard.instance.CardIdentityChecked[IndexCardtoChar] = true;

            IndexCardAllCollection[index] = IndexCardtoChar;
            IndexCardAllCollectionMenu[index] = IndexCardInMenu;
            IndexCardtoChar = -1;

            indexFull = IndexCardInMenu;
            CountCardinChar += 1;

            undosystem[index].cardparent = CardinChar[index];
            CharButton[index].transform.GetChild(1).GetComponent<Button>().interactable = true;
        }

    }

    public void SubmitComplete(string nama)
    {
        if (CountCardinChar == 4)
        {
            for (int i = 0; i < SingletonCardData.instance.datacharacther.Length; i++)
            {
                SingletonCardData.instance.datacharacther[i].CardCharacter = CardinChar[i];
                for (int j = 0; j < SingletonCardData.instance.datacharacther[i].CardDataCollection.Length; j++)
                {
                    SingletonCardData.instance.datacharacther[i].CardDataCollection[j] = CardinChar[i].cardiden.Stats[j];
                }
            }
            SceneManager.LoadScene(nama);
        }
    }
    

    public void CancelCard(int index)
    {
        GameObject build;
        build = Instantiate(CardPrefab, transform.position, transform.rotation) as GameObject;
        build.transform.parent = ParentCardPrefab.transform;


        build.transform.GetChild(0).GetComponent<Text>().text = "" + CardinChar[index].cardiden.BestStats;
        build.transform.GetChild(1).GetComponent<Text>().text = "" + CardinChar[index].cardiden.StatsSort[2];

        int TempBuild = IndexCardAllCollection[index];
        int TempBuild2 = IndexCardAllCollectionMenu[index];

        build.GetComponent<Button>().onClick.AddListener(delegate { DetailMenu(TempBuild, TempBuild2); });

        CardinMenuGameobject[IndexCardAllCollectionMenu[index]] = build;

        CharButton[index].transform.GetChild(1).GetComponent<Button>().interactable = false;
        CharButton[index].GetComponentInChildren<Text>().text = "" ;

        CardinChar[index] = null;

        IndexCardtoChar = -1;
        CountCardinChar -= 1;
    }
}
