using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    // Adjustable Parameters
    public float textDelay;

    public TextMeshProUGUI dialogText;
    private TMP_Text activeText;
    
    // Start is called before the first frame update
    void Start()
    {
        // Begin listening
        EventManager.dialogEvent.AddListener(DisplayDialog);
        
        dialogText.text = "";
    }
    
    public void DisplayDialog(DialogLine activeLine)
    {
        dialogText.text = activeLine.dialogString;
        activeText = dialogText.textInfo.textComponent;
        StartCoroutine(RevealByCharacter(activeText));
    }

    IEnumerator RevealByCharacter(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
        int visibleCount = 0;

        while (visibleCount <= totalVisibleCharacters)
        {
            textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            visibleCount += 1;

            yield return new WaitForSecondsRealtime(textDelay);
        }
    }
}
