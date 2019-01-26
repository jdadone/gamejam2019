using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController : MonoBehaviour
{
    [SerializeField]
    private ChipType type;

    private SpriteRenderer sRenderer;

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.HasChip(type))
        {
            transform.localScale = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !state.HasChip(type))
        {
            state.AddChip(type);
        }
    }
}
