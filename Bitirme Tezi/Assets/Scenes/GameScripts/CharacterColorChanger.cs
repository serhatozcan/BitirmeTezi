using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColorChanger : MonoBehaviour
{
    private Renderer renderer1;
    private GameObject circleBody;
    private GameObject squareBody;
    private GameObject emptyPartOfMouth;
    

    [SerializeField]
    public Color color;

    private void Awake()
    {
        //color = Color.red;
        //renderer1 = GetComponent<Renderer>();
        //emptyPartOfMouth = transform.Find("EmptyPartOfMouth").gameObject;
        //circleBody = transform.Find("CircleBody").gameObject;
        //squareBody = transform.Find("SquareBody").gameObject;

    }

    public void ChangeColorToBlue()
    {
        //Bu renk kullanýlabilir. Direkt mavi diye de isimlendirilebilir.
        //renderer1.material.color = new Color(0, 1, 3);
        //renderer1.material.color = Color.blue;
        transform.Find("CircleBody").gameObject.GetComponent<Renderer>().material.color = Color.blue;
        transform.Find("SquareBody").gameObject.GetComponent<Renderer>().material.color = Color.blue;
        transform.Find("EmptyPartOfMouth").gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void ChangeColorToRed()
    {
        //renderer1.material.color = Color.red;
        transform.Find("CircleBody").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("SquareBody").gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.Find("EmptyPartOfMouth").gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void ChangeColorToGreen()
    {
        Debug.Log("cagrildi");
        //renderer1.material.color = Color.green;
        transform.Find("CircleBody").gameObject.GetComponent<Renderer>().material.color = Color.green;
        transform.Find("SquareBody").gameObject.GetComponent<Renderer>().material.color = Color.green;
        transform.Find("EmptyPartOfMouth").gameObject.GetComponent<Renderer>().material.color = Color.green;
        Debug.Log("yapildi.");
    }

    public void ChangeColorToYellow()
    {
        //renderer1.material.color = Color.yellow;
        transform.Find("CircleBody").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        transform.Find("SquareBody").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        transform.Find("EmptyPartOfMouth").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
