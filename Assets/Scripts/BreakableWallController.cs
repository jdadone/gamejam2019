using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BreakWall()
    {
        StartCoroutine(Break());
    }

    IEnumerator Break ()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("drill", true);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
