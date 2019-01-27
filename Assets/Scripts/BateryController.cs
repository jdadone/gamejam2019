using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateryController : MonoBehaviour
{
    State state;
    [SerializeField]
    Sprite Level1, Level2, Level3, Level4, Level5;
    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprite sprite = null;
        switch (state.JetPackEnergy)
        {
            case 5:
                sprite = Level5;
                break;
            case 4:
                sprite = Level4;
                break;
            case 3:
                sprite = Level3;
                break;
            case 2:
                sprite = Level2;
                break;
            case 1:
                sprite = Level1;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
