using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BystanderCounter : MonoBehaviour {

    private TextMeshProUGUI text;
    private int count;

	private void Start () {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "0";
        Bystander.tramCollision += UpdateText;
	}
	
	private void UpdateText () {
        count++;
        text.text = count.ToString();
	}
}
