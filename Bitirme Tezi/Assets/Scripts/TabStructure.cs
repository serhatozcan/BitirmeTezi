using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabStructure : MonoBehaviour
{
    public GameObject tabButton1;
    public GameObject tabButton2;

    public GameObject page1;
    public GameObject page2;


    // Start is called before the first frame update
    void Start()
    {
        HideAllPages();
        ShowPage1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllPages()
    {
        page1.SetActive(false);
        page2.SetActive(false);

        tabButton1.GetComponent<Button>().image.color = new Color32(212, 212, 212, 255);
        tabButton2.GetComponent<Button>().image.color = new Color32(212, 212, 212, 255);
    }

    public void ShowPage1()
    {
        HideAllPages();
        page1.SetActive(true);
        tabButton1.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
    }

    public void ShowPage2()
    {
        HideAllPages();
        page2.SetActive(true);
        tabButton2.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
    }

    

}
