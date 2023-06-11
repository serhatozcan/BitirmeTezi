using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public void OpenLoginPage()
    {
        SceneManager.LoadScene("Login Menu");
    }

    public void OpenRegisterPage()
    {
        SceneManager.LoadScene("Register Menu");
    }
    //public void goBack()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    //}

    public void Quit()
    {
        Application.Quit();
    }
}
