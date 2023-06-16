using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    
    public void OpenLoginPage()
    {
        SceneManager.LoadScene("Login Menu");
    }

    public void OpenRegisterPage()
    {
        SceneManager.LoadScene("Register Menu");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void OpenUserTypeSelection()
    {
        SceneManager.LoadScene("User Type Selection");
    }

    public void OpenToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void OpenRegisterChildMenu()
    {
        SceneManager.LoadScene("Register Child Menu");
    }

    public void OpenRegisterParentMenu()
    {
        SceneManager.LoadScene("Register Parent Menu");
    }

}
