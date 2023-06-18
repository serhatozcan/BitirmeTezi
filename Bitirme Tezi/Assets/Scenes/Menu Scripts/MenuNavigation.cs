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
    
    public void OpenRegisterSingleChildMenu()
    {
        SceneManager.LoadScene("Register Single Child Menu");
    }

    public void OpenRegisterParentMenu()
    {
        SceneManager.LoadScene("Register Parent with Child Menu");
    }

    public void OpenCategory1Menu()
    {
        SceneManager.LoadScene("Cat1");
    }



}
