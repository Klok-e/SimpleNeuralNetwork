using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork.ActivationFunctions
{
    public class Sigmoid : IActivationFunction
    {
        public float Activate(float x)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-x));
        }

        public float ActivateDeriv(float x)
        {
            return (1.0f / (1.0f + (float)Math.Exp(-x))) * (1 - (1.0f / (1.0f + (float)Math.Exp(-x))));
            /*
            float ret;
            if (x > 2 || x < -2)
                ret = 0.00001f;
            else
                ret = 0.2f;
            return ret;*/
        }
    }
}
