using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public const string Main_Scene = "main";

    public void StartGame()
    {
        SceneManager.LoadScene(Main_Scene);
    }
}
