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
    private float newbiesTutorialTime;
    private SpriteRenderer newbiesRender;

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
        Camera cam = Camera.main;
        newbiesRender.transform.position = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, cam.nearClipPlane));

        if (state.HasChip(type) && !newbiesRender.enabled)
        {
            transform.localScale = Vector3.zero;
        }

        if(Input.anyKeyDown && (Time.time > (newbiesTutorialTime + 1f)) && newbiesRender.enabled)
        {
            newbiesRender.enabled = false;
            state.paused = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !state.HasChip(type))
        {
            collision.transform.position = transform.position;
            newbiesRender.enabled = true;
            state.paused = true;
            newbiesTutorialTime = Time.time;
            PlayGrabSound();
            state.AddChip(type, transform.position);
        }
    }

    private void PlayGrabSound()
    {
        grabSource.Play();
    }
}
