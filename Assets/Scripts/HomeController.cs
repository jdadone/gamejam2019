using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddModule (BoxType type, int index)
    {
        if (state.HasBox(type))
        {
            StartCoroutine(Show("Module" + (index + 1).ToString("0")));
        }
    }

    IEnumerator Show (string name)
    {
        var renderer = transform.Find(name).GetComponent<SpriteRenderer>();
        while (renderer.color.a < 1)
        {
            var a = renderer.color.a;
            renderer.color = new Color(1, 1, 1, a + 0.1f);
            yield return new WaitForFixedUpdate();
        }
    }
}
