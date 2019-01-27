using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
	private float tiempo;

    void Start()
    {
        tiempo = 0;
    }

    // Update is called once per frame
    void Update()
    {
    	tiempo += Time.deltaTime;
        if(tiempo > 15)
        {
			SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
        	SceneManager.LoadScene(0);
        }
    }
}
