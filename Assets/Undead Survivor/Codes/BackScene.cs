using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackScene : MonoBehaviour
{
    
    public void BackSceneStart()
    {

        switch (this.gameObject.name)
        {
            case "Back_Map":
                SceneManager.LoadScene("MapChoose");
                break;
            case "Back":
                SceneManager.LoadScene("Start");
                break;

        }
    }


}
