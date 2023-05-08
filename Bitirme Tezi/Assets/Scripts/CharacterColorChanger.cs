using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColorChanger : MonoBehaviour
{
    private Renderer renderer1;

    [SerializeField]
    public Color color = Color.white;

    private void Start()
    {
        renderer1 = GetComponent<Renderer>();
        
        
    }

    public void ChangeColorToBlue()
    {
        //Bu renk kullanýlabilir. Direkt mavi diye de isimlendirilebilir.
        renderer1.material.color = new Color(0, 1, 3);
    }

    public void ChangeColorToRed()
    {
        renderer1.material.color = Color.white;
    }
}
