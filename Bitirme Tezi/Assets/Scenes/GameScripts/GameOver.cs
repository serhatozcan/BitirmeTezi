using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public GameObject gameOverPanel;
    //public int catNumber, levelNumber;
    public TMP_Text errorMessageText;
        //, codeInputField_1Text, codeInputField_2Text;
    //public string codeInputField_1Text, codeInputField_2Text;
    public void Setup(string errorMessage)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        //catNumber = CatNumberInput;
        //levelNumber = levelNumberInput;
        errorMessageText.text = errorMessage;
        ////codeInputField_1Text.text = codeInputField_1;
        //PlayerPrefs.SetString("codeInput1", codeInputField_1);
        //PlayerPrefs.SetString("codeInput2", codeInputField_2);
        ////codeInputField_2Text.text = codeInputField_2;
        
    }

}
