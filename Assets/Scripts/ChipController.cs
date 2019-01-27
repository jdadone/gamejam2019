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
    public GameObject newbiesTutorials;
    public SpriteRenderer newbiesRender;

    private AudioSource grabSource;

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        grabSource = this.gameObject.transform.GetComponent<AudioSource>();
        newbiesRender = newbiesTutorials.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.HasChip(type) && !newbiesRender.enabled)
        {
            transform.localScale = Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && newbiesRender.enabled)
        {
            newbiesRender.enabled = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !state.HasChip(type))
        {
            newbiesRender.enabled = true;
            PlayGrabSound();
            state.AddChip(type, transform.position);
        }
    }

    private void PlayGrabSound()
    {
        grabSource.Play();
    }
}
