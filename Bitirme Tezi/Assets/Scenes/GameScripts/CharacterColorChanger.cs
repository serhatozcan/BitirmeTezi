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

    private void Start()
    {
        color = Color.red;
        renderer1 = GetComponent<Renderer>();
        emptyPartOfMouth = transform.Find("EmptyPartOfMouth").gameObject;
        circleBody = transform.Find("CircleBody").gameObject;
        squareBody = transform.Find("SquareBody").gameObject;

    }

    public void ChangeColorToBlue()
    {
        //Bu renk kullanýlabilir. Direkt mavi diye de isimlendirilebilir.
        //renderer1.material.color = new Color(0, 1, 3);
        //renderer1.material.color = Color.blue;
        circleBody.GetComponent<Renderer>().material.color = Color.blue;
        squareBody.GetComponent<Renderer>().material.color = Color.blue;
        emptyPartOfMouth.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void ChangeColorToRed()
    {
        //renderer1.material.color = Color.red;
        circleBody.GetComponent<Renderer>().material.color = Color.red;
        squareBody.GetComponent<Renderer>().material.color = Color.red;
        emptyPartOfMouth.GetComponent<Renderer>().material.color = Color.red;
    }

    public void ChangeColorToGreen()
    {
        //renderer1.material.color = Color.green;
        circleBody.GetComponent<Renderer>().material.color = Color.green;
        squareBody.GetComponent<Renderer>().material.color = Color.green;
        emptyPartOfMouth.GetComponent<Renderer>().material.color = Color.green;
    }

    public void ChangeColorToYellow()
    {
        //renderer1.material.color = Color.yellow;
        circleBody.GetComponent<Renderer>().material.color = Color.yellow;
        squareBody.GetComponent<Renderer>().material.color = Color.yellow;
        emptyPartOfMouth.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
