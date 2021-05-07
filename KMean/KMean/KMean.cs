using System;

namespace KMean
{
	public class KMean
	{
		public int[] Cluster(double[][] rawData, int k)
		{
			double[][] data = Normalized(rawData);                  //Normaliziamo i dati

			bool changed = true; 
			bool success = true;

			int[] clustering = InitClusterRandom(data.Length, k, 0);
			double[][] means = Allocate(k, data[0].Length);

			int maxCount = data.Length * 10; // sanity check
			int ct = 0;
			while (changed == true && success == true && ct < maxCount)
			{
				++ct;
				success = UpdateMeans(data, clustering, means);
				changed = UpdateClustering(data, clustering, means);
			}
			Console.WriteLine(ct);
			return clustering;
		}

		private double[][] Normalized(double[][] rawData)
		{
			// normalize raw data (x - mean) / stddev
			// alternativa min-max:
			// v' = (v - min) / (max - min)
			double[][] result = Utils.ArrayDeepCopy(rawData);

			for (int j = 0; j < result[0].Length; ++j) // each col
			{
				double colSum = 0.0;
				for (int i = 0; i < result.Length; ++i)
					colSum += result[i][j];
				double mean = colSum / result.Length;
				double sum = 0.0;
				for (int i = 0; i < result.Length; ++i)
					sum += (result[i][j] - mean) * (result[i][j] - mean);
				double sd = sum / result.Length;
				for (int i = 0; i < result.Length; ++i)
					result[i][j] = (result[i][j] - mean) / sd;
			}
			return result;
		}

		private int[] InitClusterRandom(int dataRow, int k, int randomSeed)
		{
			Random random = new Random(randomSeed);
			int[] clustering = new int[dataRow];
			for (int i = 0; i < dataRow; ++i)
				clustering[i] = random.Next(0, k);
			return clustering;
		}

		private double[][] Allocate(int k, int singleRowDimensions)
		{
			double[][] result = new double[k][];
			for (int i = 0; i < k; ++i)
				result[i] = new double[singleRowDimensions];
			return result;
		}

		// Calcolo la media dei dati per cluster
		// inserisco la nuova posizione del cluster facendo la media
		private bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
		{
			int kCount = means.Length;
			int[] clusterCounts = Utils.Frequency(clustering, kCount);

			//verifico che ci sia almeno un campione per ogni categoria, attenzione può succedere se è inizializzato a random! (ci sono modi migliori)
			if (Utils.IsBadFrequency(clusterCounts))
				return false;

			//Reset
			for (int k = 0; k < means.Length; ++k)
				for (int j = 0; j < means[k].Length; ++j)
					means[k][j] = 0.0;

			//Sum
			for (int i = 0; i < data.Length; ++i)
			{
				int cluster = clustering[i];
				for (int j = 0; j < data[i].Length; ++j)
					means[cluster][j] += data[i][j];
			}

			//Div
			for (int k = 0; k < means.Length; ++k)
				for (int j = 0; j < means[k].Length; ++j)
					means[k][j] /= clusterCounts[k];
			return true;
		}

		//Calcolo la distanza minima tra l'informazione e il cluster più vicino
		//Se il cluster è diverso da quello precedente mi segno il cambiamento
		private bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
		{
			int numClusters = means.Length;
			bool changed = false;

			int[] newClustering = new int[clustering.Length];
			Array.Copy(clustering, newClustering, clustering.Length);

			double[] distances = new double[numClusters];

			for (int i = 0; i < data.Length; ++i)
			{
				for (int k = 0; k < numClusters; ++k)
					distances[k] = Utils.Distance(data[i], means[k]);

				int newClusterID = Utils.MinIndex(distances);
				if (newClusterID != newClustering[i])
				{
					changed = true;
					newClustering[i] = newClusterID;
				}
			}

			//se non ci sono cambiamenti abbiamo finito di eseguire
			if (changed == false)
				return false;

			if (Utils.IsBadFrequency(Utils.Frequency(newClustering, numClusters)))
				return false;

			//Save result
			Array.Copy(newClustering, clustering, newClustering.Length);
			return true;
		}
	}
}
