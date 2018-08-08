using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DetectorEnemy : MonoBehaviour {

    public string LoadName;
    public string UnloadName;

    public bool OnceAddScene;
    // Use this for initialization
    void Start() {

        SceneManager.activeSceneChanged += ChangedActiveScene;
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
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerBody" && !OnceAddScene)
        {

            if (LoadName != "")
            {
                SceneManage.instace.load(LoadName);
                Debug.Log("Load Scene ");

            }
            if (UnloadName != "")
            {
                StartCoroutine(DelayTime());
            }

            OnceAddScene = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        

    }
}
