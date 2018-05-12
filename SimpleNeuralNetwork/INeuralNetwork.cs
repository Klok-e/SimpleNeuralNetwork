using System;

namespace SimpleNeuralNetwork
{
    public interface INeuralNetwork
    {
        float[] Output { get; }
        float[] Input { get; }

        void Forward();

        void Backward(MatrixFloat expected);

        void Clear();
    }
}
