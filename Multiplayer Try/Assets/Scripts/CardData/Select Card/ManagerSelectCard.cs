using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerSelectCard : MonoBehaviour {

    public bool InserData;

    [System.Serializable]
    public class CardProperties
    {
        public List<CardParent> cardinMenu;
        public List<GameObject> cardinMenuGameobject;
        public List<CardParent> cardinChar = new List<CardParent>();

        public GameObject cardPrefab;
        public GameObject parentCardPrefab;
        public GameObject parentCardChar;

    }

    [System.Serializable]
    public class CardManagerVariables
    {
        public Text[] detailMenuStats;

        public GameObject[] charButton;
        public int indexCardtoChar;
        public int indexCardInMenu;
        public int indexCardAll;

        public int[] indexCardAllCollection;
        public int[] indexCardAllCollectionMenu;
    }

    [System.Serializable]
    public class UndoSystem
    {
        public int indexChar;
        public CardParent cardparent;
        //public UndoSystem[] undo = new UndoSystem[4];
        public List<CardParent> saveuUndoSystem;
    }

    public int countCardinChar;

    public CardProperties cardProperties = new CardProperties();
    public CardManagerVariables cardManagerVariables = new CardManagerVariables();
    public UndoSystem[] undoSystem = new UndoSystem[4];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < cardManagerVariables.charButton.Length; i++)
        {
            cardManagerVariables.charButton[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        ButtonForChar();
    }

    // Update is called once per frame
    void Update()
    {
        if (InserData)
        {
            ImportDataCard();
        }
    }

    void InitConstructor()
    {

    }

    void ImportDataCard()
    { 
        int temp = 0;
        for (int i = 0; i < ManagerCard.instance.CardIdentityCollection.Count; i++)
        {
            GameObject build = null;
            cardProperties.cardinMenuGameobject.Add(build);
            cardProperties.cardinMenu.Add(ManagerCard.instance.CardIdentityCollection[i]);
            if (!ManagerCard.instance.CardIdentityChecked[i])
            {
                int a = i;
                int b = temp;

                build = Instantiate(cardProperties.cardPrefab, transform.position, transform.rotation) as GameObject;

                cardProperties.cardinMenuGameobject[i] = build;
                build.transform.parent = cardProperties.parentCardPrefab.transform;
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
        for (int i = 0; i < cardManagerVariables.charButton.Length; i++)
        {
            int temp = i;
            cardManagerVariables.charButton[i].GetComponent<Button>().onClick.AddListener(delegate { PressChar(temp, -1); });
        }
    }

    public void DetailMenu(int Index, int indexInMenu)
    {
        cardManagerVariables.indexCardtoChar = Index;
        cardManagerVariables.indexCardInMenu = indexInMenu;
        Debug.Log(Index);
        Debug.Log(indexInMenu);
        cardManagerVariables.detailMenuStats[0].text = "" + ManagerCard.instance.CardIdentityCollection[Index].cardiden.cardparentName;
        for (int i = 1; i < cardManagerVariables.detailMenuStats.Length; i++)
        {
            cardManagerVariables.detailMenuStats[i].text = "" + ManagerCard.instance.CardIdentityCollection[Index].cardiden.Stats[i - 1];
        }
    }

    public void PressChar(int index, int indexFull)
    {
        if (cardManagerVariables.indexCardtoChar == -1)
        {
            return;
        }
        else if (cardProperties.cardinMenu.Count > 0)
        {
            if (cardProperties.cardinChar[index] != null)
            {
                CardParent tempCard = new CardParent();
                CardParent tempCardMenu = new CardParent();
                int tempBuild1, tempBuild2;
                GameObject build;

                Destroy(cardProperties.cardinMenuGameobject[cardManagerVariables.indexCardInMenu].gameObject);   
                build = Instantiate(cardProperties.cardPrefab, transform.position, transform.rotation) as GameObject;
                build.transform.parent = cardProperties.parentCardPrefab.transform;
                build.transform.GetChild(0).GetComponent<Text>().text = "" + cardProperties.cardinMenu[cardManagerVariables.indexCardAllCollectionMenu[index]].cardiden.BestStats;
                build.transform.GetChild(1).GetComponent<Text>().text = "" + cardProperties.cardinMenu[cardManagerVariables.indexCardAllCollectionMenu[index]].cardiden.StatsSort[2];

                tempBuild1 = cardManagerVariables.indexCardAllCollectionMenu[index];
                tempBuild2 = cardManagerVariables.indexCardAllCollectionMenu[index];
                build.GetComponent<Button>().onClick.AddListener(delegate { DetailMenu(tempBuild1, tempBuild2); });

                cardProperties.cardinMenuGameobject[cardManagerVariables.indexCardAllCollectionMenu[index]] = build;
                cardProperties.cardinChar[index] = cardProperties.cardinMenu[cardManagerVariables.indexCardInMenu];
                cardManagerVariables.charButton[index].GetComponentInChildren<Text>().text = "" + cardProperties.cardinChar[index].cardiden.cardparentName;

                ManagerCard.instance.CardIdentityChecked[tempBuild1] = false;
                ManagerCard.instance.CardIdentityChecked[cardManagerVariables.indexCardtoChar] = true;

                cardManagerVariables.indexCardAllCollection[index] = cardManagerVariables.indexCardtoChar;
                cardManagerVariables.indexCardAllCollectionMenu[index] = cardManagerVariables.indexCardInMenu;
                cardManagerVariables.indexCardtoChar = -1;
                Debug.Log(cardProperties.cardinMenu[cardManagerVariables.indexCardAllCollectionMenu[index]].cardiden.BestStats);

                undoSystem[index].cardparent = cardProperties.cardinChar[index];
                return;
            }
            cardManagerVariables.charButton[index].GetComponentInChildren<Text>().text = "" + ManagerCard.instance.CardIdentityCollection[cardManagerVariables.indexCardtoChar].cardiden.cardparentName;
            cardProperties.cardinChar[index] = cardProperties.cardinMenu[cardManagerVariables.indexCardInMenu];

            Destroy(cardProperties.cardinMenuGameobject[cardManagerVariables.indexCardInMenu].gameObject);
            ManagerCard.instance.CardIdentityChecked[cardManagerVariables.indexCardtoChar] = true;

            cardManagerVariables.indexCardAllCollection[index] = cardManagerVariables.indexCardtoChar;
            cardManagerVariables.indexCardAllCollectionMenu[index] = cardManagerVariables.indexCardInMenu;
            cardManagerVariables.indexCardtoChar = -1;

            indexFull = cardManagerVariables.indexCardInMenu;
            countCardinChar += 1;

            undoSystem[index].cardparent = cardProperties.cardinChar[index];
            cardManagerVariables.charButton[index].transform.GetChild(1).GetComponent<Button>().interactable = true;
        }

    }

    public void SubmitComplete(string nama)
    {
        if (countCardinChar == 4)
        {
            for (int i = 0; i < SingletonCardData.instance.datacharacther.Length; i++)
            {
                SingletonCardData.instance.datacharacther[i].CardCharacter = cardProperties.cardinChar[i];
                for (int j = 0; j < SingletonCardData.instance.datacharacther[i].CardDataCollection.Length; j++)
                {
                    SingletonCardData.instance.datacharacther[i].CardDataCollection[j] = cardProperties.cardinChar[i].cardiden.Stats[j];
                }
            }
            SceneManager.LoadScene(nama);
        }
    }


    public void CancelCard(int index)
    {
        GameObject build;
        build = Instantiate(cardProperties.cardPrefab, transform.position, transform.rotation) as GameObject;
        build.transform.parent = cardProperties.parentCardPrefab.transform;


        build.transform.GetChild(0).GetComponent<Text>().text = "" + cardProperties.cardinChar[index].cardiden.BestStats;
        build.transform.GetChild(1).GetComponent<Text>().text = "" + cardProperties.cardinChar[index].cardiden.StatsSort[2];

        int TempBuild = cardManagerVariables.indexCardAllCollection[index];
        int TempBuild2 = cardManagerVariables.indexCardAllCollectionMenu[index];

        build.GetComponent<Button>().onClick.AddListener(delegate { DetailMenu(TempBuild, TempBuild2); });

        cardProperties.cardinMenuGameobject[cardManagerVariables.indexCardAllCollectionMenu[index]] = build;

        cardManagerVariables.charButton[index].transform.GetChild(1).GetComponent<Button>().interactable = false;
        cardManagerVariables.charButton[index].GetComponentInChildren<Text>().text = "";

        cardProperties.cardinChar[index] = null;

        cardManagerVariables.indexCardtoChar = -1;
        countCardinChar -= 1;
    }
}

