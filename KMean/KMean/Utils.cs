using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMean
{
	public static class Utils
	{
		public static double[][] ArrayDeepCopy(double[][] rawData)
		{
			double[][] result = new double[rawData.Length][];
			for (int i = 0; i < rawData.Length; ++i)
			{
				result[i] = new double[rawData[i].Length];
				Array.Copy(rawData[i], result[i], rawData[i].Length);
			}

			return result;
		}

		public static int MinIndex(double[] distances)
		{
			int indexOfMin = 0;
			double smallDist = distances[0];
			for (int k = 0; k < distances.Length; ++k)
			{
				if (distances[k] < smallDist)
				{
					smallDist = distances[k];
					indexOfMin = k;
				}
			}
			return indexOfMin;
		}

		public static double Distance(double[] tuple, double[] mean)
		{
			// Euclidean distance 
			double sumSquaredDiffs = 0.0;
			for (int j = 0; j < tuple.Length; ++j)
				sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
			return Math.Sqrt(sumSquaredDiffs);
		}

		public static int[] Frequency(int[] clustering, int max)
		{
			int[] frequency = new int[max];

			for (int i = 0; i < clustering.Length; ++i)
			{
				int cluster = clustering[i];
				++frequency[cluster];
			}
			return frequency;
		}

		public static bool IsBadFrequency(int[] frequency)
		{
			for (int k = 0; k < frequency.Length; ++k)
			{
				if (frequency[k] == 0)
					return true;
			}
			return false;
		}

		public static void ShowData(double[][] data, bool indices, bool newLine)
		{
			for (int i = 0; i < data.Length; ++i)
			{
				if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
				for (int j = 0; j < data[i].Length; ++j)
				{
					if (data[i][j] >= 0.0) Console.Write(" ");
					Console.Write(data[i][j].ToString("F" + 1) + " ");
				}
				Console.WriteLine("");
			}
			if (newLine) Console.WriteLine("");
		}

		public static void ShowClustered(double[][] rawData, int[] clustering, int numClusters)
		{
			for (int k = 0; k < numClusters; ++k)
			{
				Console.WriteLine($"=========[{k}]==========");
				for (int i = 0; i < rawData.Length; ++i)
				{
					int clusterID = clustering[i];
					if (clusterID != k) continue;
					Console.Write(i.ToString().PadLeft(3) + " ");
					for (int j = 0; j < rawData[i].Length; ++j)
					{
						if (rawData[i][j] >= 0.0) Console.Write(" ");
						Console.Write(rawData[i][j].ToString("F" + 1) + " ");
					}
					Console.WriteLine("");
				}
				Console.WriteLine("===================");
			}
		}
	}
}
