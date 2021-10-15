using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    // Adjustable Parameters
    public float textDelay;

    public Image SpeakerImage;
    public Image DialogBox;
    public TextMeshProUGUI dialogText;
    private TMP_Text activeText;
    private DialogLine activeDialogInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        // Begin listening
        EventManager.dialogEvent.AddListener(DisplayDialog);
        
        dialogText.text = "";
    }
    
    public void DisplayDialog(DialogLine activeLine)
    {
        DialogBox.gameObject.SetActive(true);
        
        dialogText.text = activeLine.DialogString;
        activeText = dialogText.textInfo.textComponent;
        StartCoroutine(RevealByCharacter(activeText, activeLine));

        SpeakerImage.gameObject.SetActive(true);
        SpeakerImage.sprite = activeLine.SpeakerIcon;
    }

    IEnumerator RevealByCharacter(TMP_Text textComponent, DialogLine dialogInstance) {
        activeDialogInstance = dialogInstance;
        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
        int visibleCount = 0;

        // Stop displaying text if there is a new active dialog instance
        while (visibleCount <= totalVisibleCharacters && activeDialogInstance == dialogInstance)
        {
            textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            visibleCount += 1;

            yield return new WaitForSecondsRealtime(textDelay);
        }
    }
}
