using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionSide : MonoBehaviour
{ 

    public bool isHittingWall = false;

    private void Update()
    {
        var collider = Physics2D.OverlapPoint(transform.position);
        isHittingWall = collider && collider.gameObject.tag == "Wall";
    }
}
