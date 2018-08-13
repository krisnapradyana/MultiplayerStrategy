using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DetectorEnemy : MonoBehaviour {

    public string LoadName;
    public string UnloadName;

    public bool OnceAddScene;

    CharacterBehaviour CharBehav;
    // Use this for initialization
    void Start() {

        SceneManager.activeSceneChanged += ChangedActiveScene;
        CharBehav = GetComponent<CharacterBehaviour>();
       }
        // Update is called once per frame
        void Update () {
        DecreaseFade();
	}

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManage.instace.UnLoad(UnloadName);
    }

    void DecreaseFade()
    {
       
     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackerBody" && !OnceAddScene)
        {
            CharBehav.turnController.stateID = TurnBaseController.states.BattleMode;
            if (LoadName != "")
            {
                GameManagerAll._instance.CharPrefabAttacker = other.gameObject;
                GameManagerAll._instance.CharPrefabDefense = transform.gameObject;
                ManagerChar.instance.BattleModeUI.SetActive(true);
                ManagerChar.instance.StrategyModeUI.SetActive(false); 
                SceneManage.instace.load(LoadName);


            }
            if (UnloadName != "")
            {
               // StartCoroutine(DelayTime());
            }
            OnceAddScene = true;
     
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerBody" && !OnceAddScene )
        {

            if (LoadName != "")
            {
                //  PlayerPrefsx.("AttackerSpeed",other.gameObject.GetComponentInParent<CharacterData>().Speed);
             
                PlayerPrefsX.SetIntArray("DefenderData", GetComponent<CharacterData>().CharData);
                PlayerPrefsX.SetIntArray("AttackerData", other.gameObject.GetComponentInParent<CharacterData>().CharData);

                GameManagerAll._instance.AttackerData = GetComponent<CharacterData>().CharData;
                GameManagerAll._instance.DefenderData = other.gameObject.GetComponentInParent<CharacterData>().CharData;
            
              //  SceneManage.instace.load(LoadName);
               // SceneManage.instace.UnLoad("try");

            }
            if (UnloadName != "")
            {
              //  StartCoroutine(DelayTime());
            }

            OnceAddScene = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        OnceAddScene = false;

    }
}
