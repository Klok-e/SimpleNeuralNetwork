using SimpleNeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork
{
    public class FeedForward : INeuralNetwork
    {
        #region INeuralNetwork

        public float[] Output { get; private set; }

        public float[] Input { get; private set; }

        public void Forward()
        {
            for (int i = 0; i < Input.Length; i++)
            {
                neuronLayers[0].Neurons[0, i] = Input[i];
            }

            for (int i = 0; i < neuronLayers.Count - 1; i++)
            {
                neuronLayers[i + 1].Neurons += neuronLayers[i].Activate().Multiply(weightLayers[i].Weights);
            }

            var activated = neuronLayers[neuronLayers.Count - 1].Activate();
            for (int i = 0; i < Output.Length; i++)
            {
                Output[i] = activated[0, i];
            }
        }

        public void Backward(MatrixFloat expected)
        {
            //error = (1/2)*(actual-expected)^2

            var lastLayer = neuronLayers[neuronLayers.Count - 1];

            var deltaErrorOut = 0.1f * (lastLayer.Neurons - expected) * lastLayer.ActivateDeriv();
            lastLayer.Gradient = deltaErrorOut;

            var update = neuronLayers[neuronLayers.Count - 2].Activate().Transpose().Multiply(deltaErrorOut);
            weightLayers[neuronLayers.Count - 2].Gradient = update;

            var prevDelta = deltaErrorOut;
            for (int i = neuronLayers.Count - 2; i >= 1; i--)
            {
                var dLayer = prevDelta.Multiply(weightLayers[i].Weights.Transpose()) * neuronLayers[i].ActivateDeriv();
                neuronLayers[i].Gradient = dLayer;

                update = neuronLayers[i - 1].Activate().Transpose().Multiply(dLayer);
                weightLayers[i - 1].Gradient = update;

                prevDelta = dLayer;
            }

            for (int i = 0; i < weightLayers.Count; i++)
            {
                weightLayers[i].Weights -= weightLayers[i].Gradient;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < neuronLayers.Count; i++)
            {
                neuronLayers[i].Neurons.Clear();
                neuronLayers[i].Gradient.Clear();
            }
            for (int i = 0; i < weightLayers.Count; i++)
            {
                weightLayers[i].Gradient.Clear();
            }
        }

        #endregion INeuralNetwork

        private IList<NeuronLayer> neuronLayers;
        private IList<WeightLayer> weightLayers;

        public FeedForward()
        {
            neuronLayers = new List<NeuronLayer>();
            weightLayers = new List<WeightLayer>();
        }

        public void AddLayer(NeuronLayer layer)
        {
            neuronLayers.Add(layer);
        }

        public void Construct()
        {
            for (int i = 0; i < neuronLayers.Count - 1; i++)
            {
                weightLayers.Add(new WeightLayer(neuronLayers[i], neuronLayers[i + 1]));
            }
            Input = new float[neuronLayers[0].NumberOfNeurons];
            Output = new float[neuronLayers[neuronLayers.Count - 1].NumberOfNeurons];
        }
    }
}
