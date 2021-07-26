using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogLine", menuName = "Resources/Dialog")]
public class DialogLine : ScriptableObject {
    public string DialogString;
    public Sprite SpeakerIcon;
    public AudioSource Audio;
}
