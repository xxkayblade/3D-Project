using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // changes scene when play button pressed
    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
}
