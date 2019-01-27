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

    private Vector3 lastCheckpoint = new Vector3(-15,0,0);
    public Vector3 LastChip { get { return lastCheckpoint; } }

    private float jetPackEnergy = 5;
    public float JetPackEnergy { get { return jetPackEnergy; } }
    bool JetPackActive = false;
    float JetPackChangeTime;
    public bool paused;

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
        var elapsedTime = Time.time - JetPackChangeTime;
        if (JetPackActive)
        {
            if ((elapsedTime >= 1) && JetPackEnergy > 0) {
                JetPackChangeTime = Time.time;
                jetPackEnergy--;
            }
        } else
        {
            if ((elapsedTime >= .5f) && JetPackEnergy < 5)
            {
                JetPackChangeTime = Time.time;
                jetPackEnergy++;
            }
        }
    }

    public void ChangeJetPackStatus(bool active)
    {
        JetPackActive = active;
        JetPackChangeTime = Time.time;
    }

    public bool HasChip(ChipType chip)
    {
        return chips[key: chip];
    }

    public void AddChip(ChipType chip, Vector3 position)
    {
        chips[chip] = true;
        lastCheckpoint = position;
    }

    public string GetChipsCount()
    {
        int i = 0;
        int c = 0;
        foreach (var b in chips)
        {
            if (b.Value) c++;
            i++;
        }
        return c.ToString() + "/" + i.ToString();
    }

    public bool HasBox(BoxType box)
    {
        return boxes[key: box];
    }

    public void AddBox (BoxType box, Vector3 position)
    {
        boxes[box] = true;
        lastCheckpoint = position;
    }

    public string GetBoxCount ()
    {
        int i = 0;
        int c = 0;
        foreach (var b in boxes)
        {
            if (b.Value) c++;
            i++;
        }
        return c.ToString() + "/" + i.ToString();
    }
}

public enum ChipType { JUMP, CLIMB, HOVER, FIRE  }

public enum BoxType { ONE, TWO, THREE, FOUR, FIVE }



