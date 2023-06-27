using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public GameObject gameOverPanel;
    public int catNumber, levelNumber;
    public TMP_Text errorMessageText, codeInputField_1Text, codeInputField_2Text;
    //public string codeInputField_1Text, codeInputField_2Text;
    public void SetupForTwoCodeInputs(int CatNumberInput, int levelNumberInput, string errorMessage, string codeInputField_1, string codeInputField_2)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        catNumber = CatNumberInput;
        levelNumber = levelNumberInput;
        errorMessageText.text = errorMessage;
        //codeInputField_1Text.text = codeInputField_1;
        PlayerPrefs.SetString("codeInput1", codeInputField_1);
        PlayerPrefs.SetString("codeInput2", codeInputField_2);
        //codeInputField_2Text.text = codeInputField_2;
        
    }

    public void SetupForOneCodeInput(int CatNumberInput, int levelNumberInput, string errorMessage, string codeInputField)
    {

    }

    public void Test()
    {

    }

    public void RestartWithCodes()
    {
        //gameOverPanel.SetActive(false);

    }

}
