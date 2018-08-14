using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBattleMode : MonoBehaviour {

    public GameObject CobaPrefab;

    public int IndexTurn;
    public int SaveSuitAttacker;
    public int SaveSuitDefender;

    public Animator Anim;
    // Use this for initialization

    public enum Turn
    {
        nullstate = 0,
        AttackTurn, DefenderTurn
    }

    public Turn TurnId;

    void Start() {
       // Time.timeScale = 0;
        Debug.Log("Masuk Battle Mode");
        //  Instantiate(CobaPrefab, transform.position, transform.rotation);
        TurnId = Turn.nullstate;
        TurnId = Turn.AttackTurn;
        

    }

    // Update is called once per frame
    void Update() {

    }

    public void ButtonSuit(int index)
    {
       // IndexTurn = (int) TurnId;
        if (IndexTurn == 0)
        {
            SaveSuitAttacker = index;
            
        } else if (IndexTurn ==1)
        {
            SaveSuitDefender = index;
           
        }

//        IndexTurn += 1;
       
     }

    public void EndTurnSuit()
    {
        IndexTurn += 1;
        if (IndexTurn == 2)
        {
            if (SaveSuitAttacker == SaveSuitDefender)
            {
                Debug.Log("seri");
                IndexTurn = 0;
            }
            else if (SaveSuitAttacker == 1 && SaveSuitDefender == 2)
            {
                Debug.Log("Menang save attacker gunting");

                ManageAttackOnBattle();
                TurnOffDetectorOpponentDefender();

                GameManagerAll._instance.CharPrefabDefense.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[ (int) GameManagerAll._instance.CharPrefabIndex[0]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;

               
                GameManagerAll._instance.CharPrefabDefense.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");
         
            }
            else if (SaveSuitAttacker == 1 && SaveSuitDefender == 0)
            {
                Debug.Log("Menang DEF batu");

                ManageAttackOnBattle();
                TurnOffDetectorOpponentAttacker();

                GameManagerAll._instance.CharPrefabAttacker.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[(int)GameManagerAll._instance.CharPrefabIndex[1]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;


                GameManagerAll._instance.CharPrefabAttacker.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 0 && SaveSuitDefender == 1)
            {
                Debug.Log("menang attack batu");
                ManageAttackOnBattle();
                TurnOffDetectorOpponentDefender();

                GameManagerAll._instance.CharPrefabDefense.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[(int)GameManagerAll._instance.CharPrefabIndex[0]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;


                GameManagerAll._instance.CharPrefabDefense.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 0 && SaveSuitDefender == 2)
            {
                Debug.Log("menang def kertas");

                ManageAttackOnBattle();
                TurnOffDetectorOpponentAttacker();

                GameManagerAll._instance.CharPrefabAttacker.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[(int)GameManagerAll._instance.CharPrefabIndex[1]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;


                GameManagerAll._instance.CharPrefabAttacker.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");
            }
            else if (SaveSuitAttacker == 2 && SaveSuitDefender == 0)
            {
                Debug.Log("menang attack kertas");

                ManageAttackOnBattle();
                TurnOffDetectorOpponentDefender();

                GameManagerAll._instance.CharPrefabDefense.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[(int)GameManagerAll._instance.CharPrefabIndex[0]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;


                GameManagerAll._instance.CharPrefabDefense.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 2 && SaveSuitDefender == 1)
            {
                Debug.Log("menang def gunting");

                ManageAttackOnBattle();
                TurnOffDetectorOpponentAttacker();
                GameManagerAll._instance.CharPrefabAttacker.transform.GetComponentInParent<CharacterSelectLogic>().DeadOrLife[(int)GameManagerAll._instance.CharPrefabIndex[1]] = true;

                GameManagerAll._instance.TurnManager.PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false;


                GameManagerAll._instance.CharPrefabAttacker.SetActive(false);

                GameManagerAll._instance.TurnManager.stateID = TurnBaseController.states.Attacker;
                SceneManage.instace.UnLoad("coreGameplay");
            }
            
            GameManagerAll._instance.HoldMovingChar = true;
            
           
        }
    }

    void ManageAttackOnBattle()
    {

        //  GameManagerAll._instance.DefenderData[3] += GameManagerAll._instance.DefenderData[2] - GameManagerAll._instance.AttackerData[1];
       
        ManagerChar.instance.BattleModeUI.SetActive(false);
        ManagerChar.instance.StrategyModeUI.SetActive(true);
        //StrategyModeUI.instace.UndoTurn();
    
        // PlayerPrefsX.SetIntArray("DefenderData", GetComponent<CharacterData>().CharData);
        // PlayerPrefsX.SetIntArray("AttackerData", other.gameObject.GetComponentInParent<CharacterData>().CharData);
    }

    void TurnOffDetectorOpponentAttacker( )
    {
        for (int i = 1; i <= 4; i++)
        {
            int IndexOpponent;
            if (i % 2 == 0) {
                IndexOpponent = i - 1;
            }
            else {
                IndexOpponent = i + 1;
            }
            if (GameManagerAll._instance.CharPrefabAttacker.GetComponent<DirectionControl>().movTrigger.dirDetector[i - 1].dirAvailable)
            {
                GameManagerAll._instance.CharPrefabAttacker.GetComponentInChildren<DirectionDetectorOpponent>().FriendObject.GetComponent<DirectionControl>().movTrigger.dirDetector[IndexOpponent].dirAvailable = false ;
            }
            else if (GameManagerAll._instance.CharPrefabAttacker.GetComponentInChildren<DirectionDetectorOpponent>().EnemyDetected)
            {
                GameManagerAll._instance.CharPrefabAttacker.GetComponentInChildren<DirectionDetectorOpponent>().EnemyObject.GetComponent<DirectionControl>().movTrigger.dirDetector[IndexOpponent].GetComponentInChildren<DirectionDetectorOpponent>().EnemyDetected = false;
            }
        }
    }

    void TurnOffDetectorOpponentDefender( )
    {
        int IndexOpponent;
        for (int i = 1; i <= 4; i++)
        {
            
            
            if (i % 2 == 0)
            {
                IndexOpponent = i - 1;
            }
            else
            {
                IndexOpponent = i + 1;
            }

            if (!GameManagerAll._instance.CharPrefabDefense.GetComponent<DirectionControl>().movTrigger.dirDetector[i - 1].dirAvailable)
            {
                GameManagerAll._instance.CharPrefabDefense.GetComponent<DirectionControl>().movTrigger.dirDetector[i-1].GetComponentInChildren<DirectionDetectorOpponent>().FriendObject.GetComponent<DirectionControl>().movTrigger.dirDetector[IndexOpponent - 1].OpponentTouch = false;
            }

            // pr belum selesai sama yang attack
            else if (GameManagerAll._instance.CharPrefabDefense.GetComponentInChildren<DirectionDetectorOpponent>().EnemyDetected)
            {
             //   GameManagerAll._instance.CharPrefabDefense.GetComponent<DirectionControl>().movTrigger.dirDetector[i - 1].GetComponentInChildren<DirectionDetectorOpponent>().EnemyObject.GetComponent<DirectionControl>().movTrigger.dirDetector[IndexOpponent - 1].OpponentTouch = false;
            }
            Debug.Log(IndexOpponent);
        }
    }
}
