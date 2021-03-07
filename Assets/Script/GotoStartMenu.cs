using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoStartMenu : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("StartMenu");
    }


}
