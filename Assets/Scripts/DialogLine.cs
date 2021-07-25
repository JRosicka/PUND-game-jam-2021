using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogLine", menuName = "Resources/Dialog")]
public class DialogLine : ScriptableObject {
    public enum Character {
        Poseidon,
    }
    
    public string dialogString;
    public Character speaker;
}
