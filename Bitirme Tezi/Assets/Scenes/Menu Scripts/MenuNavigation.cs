
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [Header("Subject Number")]
    public int subjectNumber;

    [Space]
    [Header("Subject 1")]
    public GameObject Subject1_Level1;
    public GameObject Subject1_Level2;
    public GameObject Subject1_Level3;
    public GameObject Subject1_Level4;
    public GameObject Subject1_Level5;
    public GameObject Subject1_Level6;
    [Header("Subject 2")]
    public GameObject Subject2_Level1;
    public GameObject Subject2_Level2;
    public GameObject Subject2_Level3;
    public GameObject Subject2_Level4;
    public GameObject Subject2_Level5;
    public GameObject Subject2_Level6;
    [Header("Subject 3")]
    public GameObject Subject3_Level1;
    public GameObject Subject3_Level2;
    public GameObject Subject3_Level3;
    public GameObject Subject3_Level4;
    public GameObject Subject3_Level5;
    public GameObject Subject3_Level6;
    [Header("Subject 4")]
    public GameObject Subject4_Level1;
    public GameObject Subject4_Level2;
    public GameObject Subject4_Level3;
    public GameObject Subject4_Level4;
    public GameObject Subject4_Level5;
    public GameObject Subject4_Level6;
    [Header("Subject 5")]
    public GameObject Subject5_Level1;
    public GameObject Subject5_Level2;
    public GameObject Subject5_Level3;
    public GameObject Subject5_Level4;
    public GameObject Subject5_Level5;
    public GameObject Subject5_Level6;
    [Header("Subject 6")]
    public GameObject Subject6_Level1;
    public GameObject Subject6_Level2;
    public GameObject Subject6_Level3;
    public GameObject Subject6_Level4;
    public GameObject Subject6_Level5;
    public GameObject Subject6_Level6;
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
        SceneManager.LoadScene("Subject1");
    }

    public void OpenSubject3Menu()
    {
        SceneManager.LoadScene("Subject1");
    }

    public void OpenSubject4Menu()
    {
        SceneManager.LoadScene("Subject1");
    }

    public void OpenSubject5Menu()
    {
        SceneManager.LoadScene("Subject1");
    }
    public void OpenSubject6Menu()
    {
        SceneManager.LoadScene("Subject1");
    }

    public void OnClickLevelButton(int subjectNumber, int levelNumber)
    {
        SceneManager.LoadScene("Subject" + subjectNumber + "Level" + levelNumber);
    }
    public void AddOnClicksOfSubject1()
    {
        
        Subject1_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 1));
        Subject1_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 2));
        Subject1_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 3));
        Subject1_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 4));
        Subject1_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 5));
        Subject1_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 6));
        
    }


    public void AddOnClicksOfSubject2()
    {
        Subject2_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 1));
        Subject2_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 2));
        Subject2_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 3));
        Subject2_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 4));
        Subject2_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 5));
        Subject2_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 6));
    }
    public void AddOnClicksOfSubject3()
    {
        Subject3_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 1));
        Subject3_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 2));
        Subject3_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 3));
        Subject3_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 4));
        Subject3_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 5));
        Subject3_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 6));
    }

    public void AddOnClicksOfSubject4()
    {
        Subject4_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 1));
        Subject4_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 2));
        Subject4_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 3));
        Subject4_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 4));
        Subject4_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 5));
        Subject4_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 6));
    }

    public void AddOnClicksOfSubject5()
    {
        Subject5_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 1));
        Subject5_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 2));
        Subject5_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 3));
        Subject5_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 4));
        Subject5_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 5));
        Subject5_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 6));
    }

    public void AddOnClicksOfSubject6()
    {
        Subject6_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 1));
        Subject6_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 2));
        Subject6_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 3));
        Subject6_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 4));
        Subject6_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 5));
        Subject6_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 6));
    }




}
