using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CommandDisplay : MonoBehaviour
{

    public CodeCommand command;

    public new TMP_Text name;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        name.text = command.name;
        image.sprite = command.image;
    }

    // Update is called once per frame
   
}
