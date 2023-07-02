
using UnityEngine;


public class CharacterColorAndShapeChanger : MonoBehaviour
{

    public void ChangeShapeToSquare()
    {
        
        transform.Find("CircleBody").gameObject.SetActive(false);
        transform.Find("SquareBody").gameObject.SetActive(true);
        transform.Find("LeftEye").gameObject.SetActive(true);
        transform.Find("RightEye").gameObject.SetActive(true);
        transform.Find("Mouth").gameObject.SetActive(true);
        transform.Find("EmptyPartOfMouth").gameObject.SetActive(true);
    }
    public void ChangeShapeToCircle()
    {
        Debug.Log("circle cagrildi");
        transform.Find("SquareBody").gameObject.SetActive(false);
        transform.Find("CircleBody").gameObject.SetActive(true);
        transform.Find("LeftEye").gameObject.SetActive(true);
        transform.Find("RightEye").gameObject.SetActive(true);
        transform.Find("Mouth").gameObject.SetActive(true);
        transform.Find("EmptyPartOfMouth").gameObject.SetActive(true);
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
