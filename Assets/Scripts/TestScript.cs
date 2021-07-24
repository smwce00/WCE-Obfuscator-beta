using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhiteCheatEngine.EncryptedInt;
using System.Text;
using WhiteCheatEngine.Detector;


public class TestScript : MonoBehaviour
{

	/*
	public EncryptedInt encryptedInt = 1987;
	private bool cheaterDetected = false;


	// Start is called before the first frame update
	void Start()
    {
		ObscuredCheatingDetectorExample();
		//EncryptedCheatingDetector.StartDetection(OnCheaterDetected);
	}

	
	private void OnCheaterDetected() {
		cheaterDetected = true;
		Debug.Log("Obscured Vars Cheating Detected!");
	}
	

	// Update is called once per frame

	void Update()
    {
        Debug.Log("TestInt: " + (EncryptedInt)encryptedInt);
    }

	private readonly StringBuilder logBuilder = new StringBuilder();

	private void Awake()
	{
		EncryptedIntExample();
	}

	internal bool obscuredTypeCheatDetected;

	public void OnObscuredTypeCheatingDetected()
	{
		obscuredTypeCheatDetected = true;
		Debug.Log("Obscured Vars Cheating Detected!");
	}

	private void ObscuredCheatingDetectorExample()
	{
		// ----------------------------------------------------
		// ObscuredCheatingDetector pure code usage example. 
		// ----------------------------------------------------

		// When you start ObscuredCheatingDetector, it starts to
		// create fake unprotected versions of all obscured
		// variables in memory, making 'honey pot' for the cheaters
		// allowing them to find desired value and cheat it just to
		// be caught, original obscured value will stay untouched

		// that's it =D
		EncryptedCheatingDetector.StartDetection(OnObscuredTypeCheatingDetected);
	}
	/*
	private void EncryptedIntExample()
	{
		// -------------- usage example --------------

		var regular = 5;

		// obscured <-> regular conversion is implicit
		var obscured = (EncryptedInt)regular;

		//-------------- logs--------------

		logBuilder.Length = 0;
		logBuilder.AppendLine("[ ObscuredInt example ]");
		logBuilder.AppendLine("Original lives count: " + regular);
		int key;
		logBuilder.AppendLine("Obscured lives count in memory: " + ((EncryptedInt)regular).GetEncrypted(out key) + " Key: " + key);
		logBuilder.AppendLine("Lives count after few operations with obscured value: " + obscured + " (" + obscured.ToString("X") + "h)");

		Debug.Log(logBuilder);

	}
	*/
	
}

