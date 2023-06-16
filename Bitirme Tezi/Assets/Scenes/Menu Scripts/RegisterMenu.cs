using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RegisterMenu : MonoBehaviour
{
    public GameObject UserTypeSelectionPanel;
    public GameObject RegisterParentPanel;
    public GameObject RegisterChildPanel;
    public void Start()
    {
        
    }
    public void OpenParentRegisterMenu()
    {
        UserTypeSelectionPanel.SetActive(false);
        RegisterParentPanel.SetActive(true);
    }

    public void OpenChildRegisterMenu()
    {
        UserTypeSelectionPanel.SetActive(false);
        RegisterChildPanel.SetActive(true);
    }
    public void OpenUserTypeSelectionPanel()
    {
        RegisterChildPanel.SetActive(false);
        RegisterParentPanel.SetActive(false);
        UserTypeSelectionPanel.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
