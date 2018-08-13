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
          ///      Destroy(GameManagerAll._instance.CharPrefabAttacker.gameObject);
          //      SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 0 && SaveSuitDefender == 1)
            {
                Debug.Log("menang attack batu");
                ManageAttackOnBattle();
           //     Destroy(GameManagerAll._instance.CharPrefabDefense.gameObject);
           //     SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 0 && SaveSuitDefender == 2)
            {
                Debug.Log("menang def kertas");

                ManageAttackOnBattle();
            //    Destroy(GameManagerAll._instance.CharPrefabAttacker.gameObject);
             //   SceneManage.instace.UnLoad("coreGameplay");
            }
            else if (SaveSuitAttacker == 2 && SaveSuitDefender == 0)
            {
                Debug.Log("menang attack kertas");

                ManageAttackOnBattle();
             //   Destroy(GameManagerAll._instance.CharPrefabDefense.gameObject);
            //    SceneManage.instace.UnLoad("coreGameplay");

            }
            else if (SaveSuitAttacker == 2 && SaveSuitDefender == 1)
            {
                Debug.Log("menang def gunting");

                ManageAttackOnBattle();
          //      Destroy(GameManagerAll._instance.CharPrefabAttacker.gameObject);
            //    SceneManage.instace.UnLoad("coreGameplay");
            }
          
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
}
