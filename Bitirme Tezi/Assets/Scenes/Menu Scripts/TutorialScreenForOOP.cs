using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenForOOP : MonoBehaviour
{
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;

    // Start is called before the first frame update
    void Start()
    {
        OpenFirstPage();   
    }

    public void OpenFirstPage()
    {
        thirdPage.SetActive(false);
        secondPage.SetActive(false);
        firstPage.SetActive(true);
    }
    public void OpenSecondPage()
    {
        thirdPage.SetActive(false);
        firstPage.SetActive(false);
        secondPage.SetActive(true);
    }
    public void OpenThirdPage()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(false);
        thirdPage.SetActive(true);
    }
}
