using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private int currentIndex = 0;
    [SerializeField]
    Sprite[] sprites;
    Image image;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex == sprites.Length - 1)
        {
            text.text = "Presiona cualquier tecla para comenzar";
        } else text.text = "Presiona cualquier tecla para continuar";

        if (Input.anyKeyDown)
        {
            if (currentIndex < sprites.Length - 1)
            {
                currentIndex++;
                image.sprite = sprites[currentIndex];
            } else
            {
                text.text = "Cargando nivel";
                SceneManager.LoadScene(1);
            }
        }
    }
}
