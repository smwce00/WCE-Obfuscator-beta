namespace WhiteCheatEngine.Detector
{
	using Common;

	using System;
	using UnityEngine;
	
	/// <summary>
	/// Base class for all detectors.
	/// </summary>
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class EncryptedCheatingDetector : WCEDetectorBase<EncryptedCheatingDetector>
	{

		#region public static methods

		internal const string LogPrefix = "[WCE] ";
		public const string ComponentName = "Obscured Cheating Detector";
		internal const string FinalLogPrefix = LogPrefix + ComponentName + ": ";

		/// <summary>
		/// Max allowed difference between encrypted and fake values in \link ObscuredTypes.ObscuredFloat ObscuredFloat\endlink. Increase in case of false positives.
		/// </summary>
		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredFloat. Increase in case of false positives.")]
		public float floatEpsilon = 0.0001f;

		/// <summary>
		/// Creates new instance of the detector at scene if it doesn't exists. Make sure to call NOT from Awake phase.
		/// </summary>
		/// <returns>New or existing instance of the detector.</returns>
		public static EncryptedCheatingDetector AddToSceneOrGetExisting()
		{
			return GetOrCreateInstance;
		}

		/// <summary>
		/// Starts all Obscured types cheating detection for detector you have in scene.
		/// </summary>
		/// Make sure you have properly configured detector in scene with #autoStart disabled before using this method.
		public static EncryptedCheatingDetector StartDetection()
		{
			if (Instance != null)
			{
				return Instance.StartDetectionInternal(null);
			}

			Debug.LogError(FinalLogPrefix + "can't be started since it doesn't exists in scene or not yet initialized!");
			return null;
		}

		/// Starts all Obscured types cheating detection with specified callback.
		/// If you have detector in scene make sure it has empty Detection Event.<br/>
		/// Creates a new detector instance if it doesn't exists in scene.
		/// <param name="callback">Method to call after detection.</param>
		public static EncryptedCheatingDetector StartDetection(Action callback)
		{
			return GetOrCreateInstance.StartDetectionInternal(callback);
		}

		/// <summary>
		/// Stops detector. Detector's component remains in the scene. Use Dispose() to completely remove detector.
		/// </summary>
		public static void StopDetection()
		{
			if (Instance != null) Instance.StopDetectionInternal();
		}

		/// <summary>
		/// Stops and completely disposes detector component.
		/// </summary>
		/// On dispose Detector follows 2 rules:
		/// - if Game Object's name is "Anti-Cheat Toolkit Detectors": it will be automatically
		/// destroyed if no other Detectors left attached regardless of any other components or children;<br/>
		/// - if Game Object's name is NOT "Anti-Cheat Toolkit Detectors": it will be automatically destroyed only
		/// if it has neither other components nor children attached;
		public static void Dispose()
		{
			if (Instance != null) Instance.DisposeInternal();
		}
		#endregion

		internal static bool ExistsAndIsRunning
		{
			get
			{
				return (object)Instance != null && Instance.IsRunning;
			}
		}

		private EncryptedCheatingDetector() { } // prevents direct instantiation

		private EncryptedCheatingDetector StartDetectionInternal(Action callback)
		{
			if (isRunning)
			{
				Debug.LogWarning(FinalLogPrefix + "already running!", this);
				return this;
			}

			if (!enabled)
			{
				Debug.LogWarning(FinalLogPrefix + "disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return this;
			}

			if (callback != null && detectionEventHasListener)
			{
				Debug.LogWarning(FinalLogPrefix + "has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}

			if (callback == null && !detectionEventHasListener)
			{
				Debug.LogWarning(FinalLogPrefix + "was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				enabled = false;
				return this;
			}

			CheatDetected += callback;
			started = true;
			isRunning = true;

			return this;
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(null);
		}

		protected override string GetComponentName()
		{
			return ComponentName;
		}

    }

}
