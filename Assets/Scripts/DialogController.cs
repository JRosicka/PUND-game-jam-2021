using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    // Adjustable Parameters
    public float textDelay;

    private TextMeshProUGUI dialogText;
    private TMP_Text activeText;

    private GameObject dialog;

    // Start is called before the first frame update
    void Start()
    {
        // Grab relevant children
        dialog = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

        // Clear text box
        dialogText = dialog.GetComponent<TextMeshProUGUI>();
        dialogText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialog(DialogLine activeLine)
    {
        dialogText.text = activeLine.dialogString;
        activeText = dialog.GetComponent<TMP_Text>();
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
