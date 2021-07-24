/*WhiteCheatEngine
 Encrypts int values with XOR
*/
namespace WhiteCheatEngine.EncryptedInt
{
	using System;
	using SharpNeatLib.Maths;
	using Utils;
    using UnityEngine;

	public struct EncryptedInt : IFormattable, IEquatable<EncryptedInt>, IComparable<EncryptedInt>, IComparable<int>, IComparable
	{

		/*
		private static readonly System.Random Global = new System.Random();

		[ThreadStatic]
		private static System.Random local;

		public static int Next(int minInclusive, int maxExclusive)
		{
			var inst = local;

			if (inst != null)
			{
				return inst.Next(minInclusive, maxExclusive);
			}

			int seed;

			lock (Global)
			{
				seed = Global.Next();
			}

			local = inst = new System.Random(seed);
			return inst.Next(minInclusive, maxExclusive);
		}
		*/

		//static FastRandom frand = new FastRandom();

		internal static int GenerateIntKey()
		{
			return ThreadSafeFastRandom.Next(1000000000, int.MaxValue);
		}
		public int currentCryptoKey;
		public int hiddenValue;
		public bool inited;
        private int fakeValue;
		private bool fakeValueActive;

		private EncryptedInt(int value) : this()
		{
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(value, currentCryptoKey);

#if UNITY_EDITOR
			this.fakeValue = value;
			this.fakeValueActive = true;
//#else
/*
			var detectorRunning = Detectors.ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = detectorRunning ? value : 0;
			fakeValueActive = detectorRunning;
*/
#endif
			inited = true;
		}
		
		public static int Encrypt(int value, int key)
		{
			return value ^ key;
		}

		public static int Decrypt(int value, int key)
		{
			return value ^ key;
		}
		public static int GenerateKey()
		{
			return GenerateIntKey();
		}

		public int GetEncrypted(out int key)
		{
			key = currentCryptoKey;
			return hiddenValue;
		}

		public void RandomizeCryptoKey()
		{
			hiddenValue = InternalDecrypt();
			currentCryptoKey = GenerateKey();
			hiddenValue = Encrypt(hiddenValue, currentCryptoKey);
		}
		
		private int InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = GenerateKey();
				hiddenValue = Encrypt(0, currentCryptoKey);
				fakeValue = 0;
				fakeValueActive = false;
				inited = true;

				return 0;
			}

			var decrypted = Decrypt(hiddenValue, currentCryptoKey);
			
			if (Detector.EncryptedCheatingDetector.ExistsAndIsRunning && fakeValueActive && decrypted != fakeValue)
			{
				Detector.EncryptedCheatingDetector.Instance.OnCheatingDetected();
			}
			
			return decrypted;
		}


		private static EncryptedInt Increment(EncryptedInt input, int increment)
		{
			var decrypted = input.InternalDecrypt() + increment;
			input.hiddenValue = Encrypt(decrypted, input.currentCryptoKey);

			
			if (Detector.EncryptedCheatingDetector.ExistsAndIsRunning)
			{
				input.fakeValue = decrypted;
				input.fakeValueActive = true;
			}
			else
			{
				input.fakeValueActive = false;
			}
			
			return input;
		}

		// ÇÊ¼ö!!!!!!!
		public static implicit operator EncryptedInt(int value)
		{
			return new EncryptedInt(value);
		}
		public static implicit operator int(EncryptedInt value)
		{
			return value.InternalDecrypt();
		}

		public static EncryptedInt operator ++(EncryptedInt input)
		{
			return Increment(input, 1);
		}

		public static EncryptedInt operator --(EncryptedInt input)
		{
			return Increment(input, -1);
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}
		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public string ToString(string format)
		{
			return InternalDecrypt().ToString(format);
		}

		public string ToString(IFormatProvider provider)
		{
			return InternalDecrypt().ToString(provider);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			return InternalDecrypt().ToString(format, provider);
		}

		public override bool Equals(object obj)
		{
			return obj is EncryptedInt && Equals((EncryptedInt)obj);
		}

		public bool Equals(EncryptedInt obj)
		{
			if (currentCryptoKey == obj.currentCryptoKey)
			{
				return hiddenValue.Equals(obj.hiddenValue);
			}

			return Decrypt(hiddenValue, currentCryptoKey).Equals(Decrypt(obj.hiddenValue, obj.currentCryptoKey));
		}

		public int CompareTo(EncryptedInt other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(int other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
#if !ACTK_UWP_NO_IL2CPP
			return InternalDecrypt().CompareTo(obj);
#else
			if (obj == null) return 1;
			if (!(obj is int)) throw new ArgumentException("Argument must be int");
			return CompareTo((int)obj);
#endif
		}
	}

}
