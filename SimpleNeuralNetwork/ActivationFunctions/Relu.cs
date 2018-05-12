using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.ActivationFunctions
{
    public class Relu : IActivationFunction
    {
        private const float epsilon = 0.1f;

        public float Activate(float x)
        {
            float ret;
            if (x > 0)
                ret = x;
            else
                ret = x * epsilon;
            return ret;
        }

        public float ActivateDeriv(float x)
        {
            float ret;
            if (x > 0)
                ret = 1;
            else
                ret = epsilon;
            return ret;
        }
    }
}
