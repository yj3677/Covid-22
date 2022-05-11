using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{

    void Update()
    {
        Invoke("MainScene", 3);
    }
    void MainScene()
    {
        LoadSceneController.LoadScene("MainStage");
    }
}
