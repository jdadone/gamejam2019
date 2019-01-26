using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private Dictionary<ChipType, bool> chips = new Dictionary<ChipType, bool>()
    {
        { ChipType.JUMP, false },
        { ChipType.CLIMB, false },
        { ChipType.CRAWL, false },
        { ChipType.FIRE, false }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool HasChip(ChipType chip)
    {
        return chips[key: chip];
    }

    void AddChip (ChipType chip)
    {
        chips[chip] = true;
    }
}

public enum ChipType { JUMP, CLIMB, CRAWL, FIRE  }

public class Chips
{
    public bool jump;

}
