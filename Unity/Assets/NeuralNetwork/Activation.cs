namespace NeuralNetwork
{
    public static class Activation
    {
        double Sigmoid(double x)
        {
            return 1.0/(1.0+Math.Exp(-x));
        }


    }

}