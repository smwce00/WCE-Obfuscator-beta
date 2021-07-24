namespace WhiteCheatEngine.Utils
{
	using System;
	using SharpNeatLib.Maths;

	internal class ThreadSafeFastRandom
	{
		private static readonly FastRandom Global = new FastRandom();

		[ThreadStatic]
		private static FastRandom local;

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

			seed = Global.Next();
			local = inst = new FastRandom(seed);

			return inst.Next(minInclusive, maxExclusive);
		}

		public static long NextLong(long minInclusive, long maxExclusive)
		{
			var inst = local;

			if (inst != null)
			{
				return NextLong(inst, minInclusive, maxExclusive);
			}

			int seed;

			lock (Global)
			{
				seed = Global.Next();
			}

			local = inst = new FastRandom(seed);
			return NextLong(inst, minInclusive, maxExclusive);
		}

		public static void NextBytes(byte[] buffer)
		{
			var inst = local;

			if (inst != null)
			{
				inst.NextBytes(buffer);
				return;
			}

			int seed;

			lock (Global)
			{
				seed = Global.Next();
			}

			local = inst = new FastRandom(seed);
			inst.NextBytes(buffer);
		}

		public static void NextChars(char[] buffer)
		{
			var inst = local;

			if (inst != null)
			{
				NextChars(inst, buffer);
				return;
			}

			int seed;

			lock (Global)
			{
				seed = Global.Next();
			}

			local = inst = new FastRandom(seed);
			NextChars(inst, buffer);
		}

		public static int Next()
		{
			return Next(1, int.MaxValue);
		}

		public static int Next(int maxExclusive)
		{
			return Next(1, maxExclusive);
		}

		private static long NextLong(FastRandom random, long minInclusive, long maxExclusive)
		{
			var result = (long)random.Next((int)(minInclusive >> 32), (int)(maxExclusive >> 32));
			result <<= 32;
			result |= (uint)random.Next((int)minInclusive, (int)maxExclusive);
			return result;
		}

		private static void NextChars(FastRandom random, char[] buffer)
		{
			for (var i = 0; i < buffer.Length; ++i)
			{
				// capping to byte value here to not exceed
				// 56 bit crypto keys length requirement by
				// Apple to avoid cryptography declaration
				buffer[i] = (char)(random.Next() % 256);
			}
		}
	}
}