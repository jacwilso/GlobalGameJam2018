using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLight : MonoBehaviour {

    public const string Material_Emission = "_EmissionColor";

    [SerializeField] private LeverState matchLever;
    [SerializeField] private GameObject go, stop;

    private Color goColor = new Color(0.2275865f, 3f, 0);
    private Color stopColor = new Color(3f, 0, 0);
    private Material goMat, stopMat;
    private Renderer goRend, stopRend;

	private void Start () {
        goRend = GetComponent<Renderer>();
        goMat = goRend.material;
        stopRend = GetComponent<Renderer>();
        stopMat = stopRend.material;
        ChangeLight();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeLight();
        }
	}

    private void ChangeLight()
    {
        if (LeverController.instance.State == matchLever)
        {
            goMat.SetColor(Material_Emission, goColor);
            stopMat.SetColor(Material_Emission, Color.black);
        }
        else
        {
            goMat.SetColor(Material_Emission, Color.black);
            stopMat.SetColor(Material_Emission, stopColor);
        }
        goRend.material = goMat;
        stopRend.material = stopMat;
    }
}
