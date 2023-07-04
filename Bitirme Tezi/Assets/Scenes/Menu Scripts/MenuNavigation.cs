
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [Header("Subject Number")]
    public int subjectNumber;

  
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

    public void OpenSubject1Menu()
    {
        SceneManager.LoadScene("Subject1");
    }

    public void OpenSubject2Menu()
    {
        SceneManager.LoadScene("Subject2");
    }

    public void OpenSubject3Menu()
    {
        SceneManager.LoadScene("Subject3");
    }

    public void OpenSubject4Menu()
    {
        SceneManager.LoadScene("Subject4");
    }

    public void OpenSubject5Menu()
    {
        SceneManager.LoadScene("Subject5");
    }
    public void OpenSubject6Menu()
    {
        SceneManager.LoadScene("Subject6");
    }

    



}
