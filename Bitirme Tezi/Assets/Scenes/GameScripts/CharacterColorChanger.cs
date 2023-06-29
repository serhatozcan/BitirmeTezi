using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColorChanger : MonoBehaviour
{
    private Renderer renderer1;

    [SerializeField]
    public Color color;

    private void Start()
    {
        color = Color.red;
        renderer1 = GetComponent<Renderer>();
        
        
    }

    public void ChangeColorToBlue()
    {
        //Bu renk kullanýlabilir. Direkt mavi diye de isimlendirilebilir.
        //renderer1.material.color = new Color(0, 1, 3);
        renderer1.material.color = Color.blue;
    }

    public void ChangeColorToRed()
    {
        renderer1.material.color = Color.red;
    }

    public void ChangeColorToGreen()
    {
        renderer1.material.color = Color.green;
    }

    public void ChangeColorToYellow()
    {
        renderer1.material.color = Color.yellow;
    }
}
