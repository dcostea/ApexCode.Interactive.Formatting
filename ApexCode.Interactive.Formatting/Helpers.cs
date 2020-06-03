using MathNet.Numerics.Statistics;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Collections.Generic;
using System.Linq;

namespace ApexCode.Interactive.Formatting
{
    public static class Helpers
    {
        public static (IDictionary<string, IEnumerable<float>> weights, IDictionary<string, float> biases) GetModelParameters(TransformerChain<MulticlassPredictionTransformer<LinearMulticlassModelParameters>> modelForContributions, string[] categories)
        {
            var modelParameters = modelForContributions.Last() as MulticlassPredictionTransformer<LinearMulticlassModelParameters>;

            VBuffer<float>[] weights = default;
            modelParameters.Model.GetWeights(ref weights, out int _);

            var weightsDictionary = new Dictionary<string, IEnumerable<float>>();
            var i = 0;
            foreach (var weight in weights)
            {
                weightsDictionary.Add(categories[i++], (weight as VBuffer<float>?).Value.DenseValues());
            }

            var biases = modelParameters.Model.GetBiases();
            var biasesDictionary = new Dictionary<string, float>();
            i = 0;
            foreach (var bias in biases)
            {
                biasesDictionary.Add(categories[i++], bias);
            }

            return (weightsDictionary, biasesDictionary);
        }

        /// <summary>
        /// Compute Pearson correlation of the matrix
        /// </summary>
        /// <param name="matrix">Correlation matrix</param>
        /// <returns></returns>
        public static double[,] GetPearsonCorrelation(List<List<double>> matrix)
        {
            var length = matrix.Count();

            var z = new double[length, length];
            for (int x = 0; x < length; ++x)
            {
                for (int y = 0; y < length - 1 - x; ++y)
                {
                    var seriesA = matrix[x];
                    var seriesB = matrix[length - 1 - y];

                    var value = Correlation.Pearson(seriesA, seriesB);

                    z[x, y] = value;
                    z[length - 1 - y, length - 1 - x] = value;
                }

                z[x, length - 1 - x] = 1;
            }

            return z;
        }
    }
}
