﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManage : MonoBehaviour {

    public static SceneManage instace;
    public Animator Fade;

	// Use this for initialization
	void Start () {
        instace = this;
        load("try");
        UnLoad("coreGameplay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void load(string NamaScene)
    {

            if (!SceneManager.GetSceneByName(NamaScene).isLoaded)
        {
            
            Fade.SetTrigger("FadeOut");
            SceneManager.LoadScene(NamaScene,LoadSceneMode.Additive);
            
        }
  
    }

    public void UnLoad(string NamaScene)
    {
        
            if (SceneManager.GetSceneByName(NamaScene).isLoaded)
        {
            SceneManager.UnloadScene(NamaScene);
        }

    }
}
