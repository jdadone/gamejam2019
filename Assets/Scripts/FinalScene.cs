﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{

	private float timeSpend;

	public Sprite[] deadSprites;

	public GameObject[] astronauts;

	private int numberOfDeaths;

    void Start()
    {
        timeSpend = PlayerPrefs.GetFloat("TimeSpend");

        Random.InitState(System.DateTime.Now.Second);

        int dead1 = Random.Range(0,5);
        int dead2 = Random.Range(0,5);
        int dead3 = Random.Range(0,5);
        int dead4 = Random.Range(0,5);

        while(dead2 == dead1)
        {
			dead2 = Random.Range(0,5);
        }

        while(dead3 == dead1 || dead3 == dead2)
        {
        	dead3 = Random.Range(0,5);
        }

        while(dead4 == dead1 || dead4 == dead2 || dead4 == dead3)
        {
        	dead4 = Random.Range(0,5);
        }

        if(timeSpend < 300)
        {
        	numberOfDeaths = 0;
        }
        else if (timeSpend < 360)
        {
        	numberOfDeaths = 1;
        	astronauts[dead1].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead1];
        }
        else if (timeSpend < 420)
        {
        	numberOfDeaths = 2;
        	astronauts[dead1].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead1];
        	astronauts[dead2].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead2];
        }
        else if (timeSpend < 480)
        {
        	numberOfDeaths = 3;
        	astronauts[dead1].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead1];
        	astronauts[dead2].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead2];
        	astronauts[dead3].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead3];
        }
        else if (timeSpend < 540)
        {
        	numberOfDeaths = 4;
        	astronauts[dead1].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead1];
        	astronauts[dead2].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead2];
        	astronauts[dead3].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead3];
        	astronauts[dead4].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[dead4];
        }
        else
        {
        	numberOfDeaths = 5;
        	astronauts[0].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[0];
        	astronauts[1].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[1];
        	astronauts[2].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[2];
        	astronauts[3].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[3];
        	astronauts[4].transform.GetComponent<SpriteRenderer>().sprite = deadSprites[4];
        }
        
        Debug.Log("Muertos: " + numberOfDeaths + " Te tardaste: " + timeSpend + " Segundos.");

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
