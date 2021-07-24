using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhiteCheatEngine.EncryptedInt;
using WhiteCheatEngine.Detector;

public class DetectTest : MonoBehaviour
{
    public EncryptedInt lives = 5;
    private bool cheaterDetected = false;

    private void Start()
    {
        EncryptedCheatingDetector.StartDetection(OnCheaterDetected);
    }

    private void OnCheaterDetected()
    {
        cheaterDetected = true;
        Debug.Log("Gotcha!");
        int key;
        Debug.Log("Obscured lives count in memory: " + ((EncryptedInt)lives).GetEncrypted(out key) + " Key: " + key);
    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.width - 20));

        if (lives > 0)
        {
            GUILayout.Label("Lives: "  +  lives);
            if (GUILayout.Button("Kill player", GUILayout.ExpandWidth(false)))
            {
                lives--;
            }
        }
        else {
            GUILayout.Label("Game over! :(");
            if (GUILayout.Button("Start new game", GUILayout.ExpandWidth(false)))
            {
                lives = 5;
            }
        }
        GUILayout.EndArea();
    }
}
