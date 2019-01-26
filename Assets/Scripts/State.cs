using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private Dictionary<ChipType, bool> chips = new Dictionary<ChipType, bool>()
    {
        { ChipType.JUMP, false },
        { ChipType.CLIMB, false },
        { ChipType.HOVER, false },
        { ChipType.FIRE, false }
    };

    private ChipType lastChip;
    public ChipType LastChip { get { return lastChip; } }

    private Dictionary<BoxType, bool> boxes = new Dictionary<BoxType, bool>()
    {
        { BoxType.ONE, false },
        { BoxType.TWO, false },
        { BoxType.THREE, false },
        { BoxType.FOUR, false },
        { BoxType.FIVE, false }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool HasChip(ChipType chip)
    {
        return chips[key: chip];
    }

    public void AddChip(ChipType chip)
    {
        chips[chip] = true;
        lastChip = chip;
    }

    public bool HasBox(BoxType box)
    {
        return boxes[key: box];
    }

    public void AddBox (BoxType box)
    {
        boxes[box] = true;
    }
}

public enum ChipType { JUMP, CLIMB, HOVER, FIRE  }

public enum BoxType { ONE, TWO, THREE, FOUR, FIVE }



