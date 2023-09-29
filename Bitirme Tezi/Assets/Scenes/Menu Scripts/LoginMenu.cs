using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
