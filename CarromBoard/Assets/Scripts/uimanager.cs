using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class uimanager : MonoBehaviour
{
    public void playbutton()
    {
        SceneManager.LoadScene("Carrom");
    }
    public void closbutton()
    {
        Application.Quit();
    }
}
