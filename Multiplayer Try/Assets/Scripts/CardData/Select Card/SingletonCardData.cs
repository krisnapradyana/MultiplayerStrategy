using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCardData : MonoBehaviour {

    public static SingletonCardData instance;

    [System.Serializable]
    public  class DataCharacther {
        public CardParent CardCharacter;
        public int[] CardDataCollection = new int[4];
    }

    public DataCharacther[] datacharacther = new DataCharacther[4] ;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
