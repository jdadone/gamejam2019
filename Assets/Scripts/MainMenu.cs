using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if(!PlayerPrefs.HasKey("TimeSpend"))
        {
            PlayerPrefs.SetFloat("TimeSpend", 0.0F);
        }
        
    }

    void Update()
    {
    	if(Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
