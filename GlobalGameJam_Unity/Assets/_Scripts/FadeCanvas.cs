using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeCanvas : MonoBehaviour {

    [SerializeField] private float fadeTime;

    private Image[] imgs;
    private TextMeshProUGUI[] txts;
    private bool isFade;

    private void Start () {
        imgs = GetComponentsInChildren<Image>();
        txts = GetComponentsInChildren<TextMeshProUGUI>();
	}
	
	private void Update () {
		if (!isFade && Input.GetKeyDown(KeyCode.Space))
        {
            TramSpawner.instance.Spawn();
            StartCoroutine(Fade());
        }
	}

    private IEnumerator Fade()
    {
        isFade = true;
        float elapsed = 0;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            for (int i = 0; i < imgs.Length; i++)
            {
                Color c = imgs[i].color;
                c.a = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
                imgs[i].color = c;
            }
            for (int i= 0; i < txts.Length; i++)
            {
                Color c = txts[i].color;
                c.a = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
                txts[i].color = c;
            }
            yield return null;
        }
    }
}
