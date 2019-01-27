using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    State state;

    [SerializeField]
    Sprite[] boxesSprites;

    HomeController home;
    SpacemenController spacemen;

    Canvas canvas;

    private BoxType[] boxes = new BoxType[] { BoxType.ONE, BoxType.TWO, BoxType.THREE, BoxType.FOUR, BoxType.FIVE };

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<State>();
        home = FindObjectOfType<HomeController>();
        spacemen = GetComponentInChildren<SpacemenController>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        	PlayerPrefs.SetFloat("TimeSpend", Time.time);
            StartCoroutine(AddBoxes());
        }
    }

    

    private void GoFinalScene ()
    {
        SceneManager.LoadScene(2);
    }

    void ShowStats ()
    {
        Text textUI = canvas.GetComponentInChildren<Text>();
        textUI.text = "Chips\n" + state.GetChipsCount() + "\n\n";
        canvas.gameObject.SetActive(true);
    }

    IEnumerator AddBoxes ()
    {
        yield return new WaitForSeconds(1);
        yield return AddBox(0);
        yield return AddBox(1);
        yield return AddBox(2);
        yield return AddBox(3);
        yield return AddBox(4);
        ShowStats();
        spacemen.Show();
    }

    IEnumerator AddBox (int i)
    {
        var boxGO = new GameObject();
        boxGO.transform.localScale *= 2;
        var renderer = boxGO.AddComponent<SpriteRenderer>();
        renderer.sprite = boxesSprites[i];
        renderer.sortingOrder = 10;

        renderer.color = new Color(1, 1, 1, 0);

        var startAnimationTime = Time.time;
        var cam = Camera.main;
        var height = cam.pixelHeight / 5;
        var y = height * (i + 1) - height / 2;
        var x = 10 * cam.pixelWidth / 100;
        boxGO.transform.position = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));

        var maxAlpha = state.HasBox(boxes[i]) ? 1 : 0.2f;
        var alphaStep = 0.1f * maxAlpha;
        home.AddModule(boxes[i], i);

        while (renderer.color.a < maxAlpha)
        {
            var a = renderer.color.a;
            renderer.color = new Color(1,1,1, a +  alphaStep);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
