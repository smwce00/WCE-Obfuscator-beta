namespace WhiteCheatEngine.Examples
{
	using Common;
	//using Storage;
	using EncryptedInt;
	using EncryptedFloat;
    using Detector;

	using System;
	using System.Diagnostics;
	using System.Text;
	using UnityEngine;
	using Debug = UnityEngine.Debug;

	/// <summary>
	/// These super simple and stupid tests allow you to see how slower Obscured types can be compared to the regular types.
	/// Take in account iterations count though.
	/// </summary>
	[AddComponentMenu("")]
	internal class EncryptedPerformanceTest : MonoBehaviour
	{

		public bool intTest = true;
		public int intIterations = 2500000;

		
		public bool floatTest = true;
		public int floatIterations = 2500000;

		/*
		public bool vector3Test = true;
		public int vector3Iterations = 2500000;
		*/

		private readonly StringBuilder logBuilder = new StringBuilder();

		private void Start()
		{
			Invoke("StartTests", 1f);
		}

		private void StartTests()
		{
			logBuilder.Length = 0;
			logBuilder.AppendLine(EncryptedCheatingDetector.LogPrefix + "<b>[ Performance tests ]</b>");

			
			if (intTest) TestInt();
			
			if (floatTest) TestFloat();
			
			//if (vector3Test) TestVector3();


			Debug.Log(logBuilder);
		}

		private void TestFloat()
		{
			logBuilder.AppendLine("EncryptedFloat vs float, " + floatIterations + " iterations for read and write");

			EncryptedFloat encrypted = 100f;
			float notEncrypted = encrypted;
			float dummy = 0;

			var sw = Stopwatch.StartNew();

			for (var i = 0; i < floatIterations; i++)
			{
				dummy = encrypted;
			}

			for (var i = 0; i < floatIterations; i++)
			{
				encrypted = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("EncryptedFloat:").AppendLine(sw.ElapsedMilliseconds + " ms");

			sw.Reset();
			sw.Start();
			for (var i = 0; i < floatIterations; i++)
			{
				dummy = notEncrypted;
			}

			for (var i = 0; i < floatIterations; i++)
			{
				notEncrypted = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("float:").AppendLine(sw.ElapsedMilliseconds + " ms");

			if (Math.Abs(dummy) > 0.00001f) { }
			if (Math.Abs(encrypted) > 0.00001f) { }
			if (Math.Abs(notEncrypted) > 0.00001f) { }
		}
		

		private void TestInt()
		{
			logBuilder.AppendLine("EncryptedInt vs int, " + intIterations + " iterations for read and write");

			EncryptedInt encrypted = 100;
			int notEncrypted = encrypted;
			var dummy = 0;

			var sw = Stopwatch.StartNew();

			for (var i = 0; i < intIterations; i++)
			{
				dummy = encrypted;
			}

			for (var i = 0; i < intIterations; i++)
			{
				encrypted = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("EncryptedInt:").AppendLine(sw.ElapsedMilliseconds + " ms");

			sw.Reset();
			sw.Start();
			for (var i = 0; i < intIterations; i++)
			{
				dummy = notEncrypted;
			}

			for (var i = 0; i < intIterations; i++)
			{
				notEncrypted = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("int:").AppendLine(sw.ElapsedMilliseconds + " ms");

			if (dummy != 0) { }
			if (encrypted != 0) { }
			if (notEncrypted != 0) { }
		}

		/*
		private void TestVector3()
		{
			logBuilder.AppendLine("ObscuredVector3 vs Vector3, " + vector3Iterations + " iterations for read and write");

			ObscuredVector3 obscured = new Vector3(1f, 2f, 3f);
			Vector3 notObscured = obscured;
			var dummy = new Vector3(0, 0, 0);

			var sw = Stopwatch.StartNew();
			for (var i = 0; i < vector3Iterations; i++)
			{
				dummy = obscured;
			}

			for (var i = 0; i < vector3Iterations; i++)
			{
				obscured = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("ObscuredVector3:").AppendLine(sw.ElapsedMilliseconds + " ms");

			sw.Reset();
			sw.Start();
			for (var i = 0; i < vector3Iterations; i++)
			{
				dummy = notObscured;
			}

			for (var i = 0; i < vector3Iterations; i++)
			{
				notObscured = dummy;
			}
			sw.Stop();
			logBuilder.AppendLine("Vector3:").AppendLine(sw.ElapsedMilliseconds + " ms");

			if (dummy != Vector3.zero) { }
			if (obscured != Vector3.zero) { }
			if (notObscured != Vector3.zero) { }
		}
		*/

	}
}