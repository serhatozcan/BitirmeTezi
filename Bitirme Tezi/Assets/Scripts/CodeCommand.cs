using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Command")]
public class CodeCommand : ScriptableObject
{
    // Start is called before the first frame update
    public new string name;
    public Sprite image;

}
