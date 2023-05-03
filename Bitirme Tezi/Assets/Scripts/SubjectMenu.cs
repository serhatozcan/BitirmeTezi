using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SubjectMenu : MonoBehaviour
{
   public void OpenCat1()
    {
        SceneManager.LoadScene("Cat1");
    }
    public void OpenCat2()
    {
        SceneManager.LoadScene("Cat2");
    }
    public void OpenCat3()
    {
        SceneManager.LoadScene("Cat3");
    }
    public void OpenCat4()
    {
        SceneManager.LoadScene("Cat4");
    }
    public void OpenCat5()
    {
        SceneManager.LoadScene("Cat5");
    }
    public void OpenCat6()
    {
        SceneManager.LoadScene("Cat6");
    }
    public void OpenCat7()
    {
        SceneManager.LoadScene("Cat7");
    }
    public void OpenCat8()
    {
        SceneManager.LoadScene("Cat8");
    }
    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                SceneManager.LoadScene("MainMenu");
            }
        }
    }


}
