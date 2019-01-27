using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController : MonoBehaviour
{
    [SerializeField]
    private ChipType type;

    public ChipType Type { get { return type; } }

    private SpriteRenderer sRenderer;

    private State state;

    private AudioSource grabSource;

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        grabSource = this.gameObject.transform.GetComponent<AudioSource>();
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
            PlayGrabSound();
            state.AddChip(type, transform.position);
        }
    }

    private void PlayGrabSound()
    {
        grabSource.Play();
    }
}
