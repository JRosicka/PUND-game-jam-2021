using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// Handles displaying <see cref="DialogLine"/>s for crew-mates when the ship respawns
/// </summary>
public class PlayerCrew : MonoBehaviour {
    public List<DialogLine> CrewSpawnLines;

    private List<DialogLine> currentCrewLines;
    private DialogLine mostRecentLine;

    private void Start() {
        CreateCrewMateQueue();
    }

    public void DisplayNewCrewMate() {
        // Have we used all of the dialog lines already? Then recreate the list and start over again. 
        if (currentCrewLines.Count <= 0) {
            CreateCrewMateQueue();
            if (currentCrewLines.Count <= 0) {
                // No crew lines, must not be any set up
                return;
            }
        }
        
        // Pick the next dialog and remove it so it doesn't get picked again
        mostRecentLine = currentCrewLines[0];
        currentCrewLines.Remove(mostRecentLine);
        
        // Play the dialog
        EventManager.dialogEvent.Invoke(mostRecentLine);
    }
    
    private void CreateCrewMateQueue() {
        currentCrewLines = new List<DialogLine>(CrewSpawnLines);
        currentCrewLines.Shuffle();
        
        // Make sure that the most recently played line is not at the top of the list
        if (mostRecentLine != null && currentCrewLines.Count > 0 && mostRecentLine.Audio == currentCrewLines[0].Audio) {
            DialogLine line = currentCrewLines[0];
            currentCrewLines.Remove(line);
            currentCrewLines.Add(line);
        }
    }
}
