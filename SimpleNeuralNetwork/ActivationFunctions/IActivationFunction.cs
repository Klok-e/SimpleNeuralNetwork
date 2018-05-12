using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        float Activate(float x);

        float ActivateDeriv(float x);
    }
}
