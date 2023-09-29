using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SubjectMenuScript : MonoBehaviour
{
   public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void BackToSubjectsMenu()
    {
        SceneManager.LoadScene("SubjectsMenu");
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
