using System;

namespace KMean
{
	public static class Program
	{
		static void Main(string[] args)
		{
			double[][] rawData = new double[20][];
			rawData[0] = new double[] { 65.0, 220.0, 1 };
			rawData[1] = new double[] { 73.0, 160.0, 0 };
			rawData[2] = new double[] { 59.0, 110.0, 0 };
			rawData[3] = new double[] { 61.0, 120.0, 0 };
			rawData[4] = new double[] { 75.0, 150.0, 0 };
			rawData[5] = new double[] { 67.0, 240.0, 1 };
			rawData[6] = new double[] { 68.0, 230.0, 1 };
			rawData[7] = new double[] { 70.0, 220.0, 1 };
			rawData[8] = new double[] { 62.0, 130.0, 0 };
			rawData[9] = new double[] { 66.0, 210.0, 1 };
			rawData[10] = new double[] { 77.0, 190.0, 0 };
			rawData[11] = new double[] { 75.0, 180.0, 0 };
			rawData[12] = new double[] { 74.0, 170.0, 0 };
			rawData[13] = new double[] { 70.0, 210.0, 1 };
			rawData[14] = new double[] { 61.0, 110.0, 0 };
			rawData[15] = new double[] { 58.0, 100.0, 0 };
			rawData[16] = new double[] { 66.0, 230.0, 1 };
			rawData[17] = new double[] { 59.0, 120.0, 0 };
			rawData[18] = new double[] { 68.0, 210.0, 1 };
			rawData[19] = new double[] { 61.0, 130.0, 0 };

			Console.WriteLine("Raw data:");
			Console.WriteLine("    Height Weight");
			Console.WriteLine("-------------------");
			Utils.ShowData(rawData, true, true);

			int k = 2;

			int[] clustering = new KMean().Cluster(rawData, k);

			Console.WriteLine("Data by Cluster:\n");
			Utils.ShowClustered(rawData, clustering, k);

			Console.ReadLine();
		}
	}
}
