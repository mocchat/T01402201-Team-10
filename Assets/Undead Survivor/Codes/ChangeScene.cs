using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  public void ChangeSceneMain()
    {
        switch (this.gameObject.name)
        {
            case "Start":
                SceneManager.LoadScene("MapChoose");
                break;
            case "GroundMap":
                SceneManager.LoadScene("GroundGameScene");
                break;
            case "SnowMap":
                SceneManager.LoadScene("SnowGameScene");
                break;
            case "CaveMap":
                SceneManager.LoadScene("CaveGameScene");
                break;
            case "SwampMap":
                SceneManager.LoadScene("SwampGameScene");
                break;
            case "HellMap":
                SceneManager.LoadScene("HellGrounScene");
                break;
                // case " Option":
                //      SceneManager.LoadScene();
                //   break;

        }
    }





}
