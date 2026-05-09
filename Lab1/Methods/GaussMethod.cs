namespace Lab1.Methods
{
    public class GaussMethod
    {
        public GaussResult Optimize(
            Func<double[], double> func,
            double[] initialPoint,
            double tolerance,
            int maxIterations,
            double initialStep,
            double stepReduction)
        {
            int n = initialPoint.Length;

            double[] currentPoint = (double[])initialPoint.Clone();
            double currentValue = func(currentPoint);

            double step = initialStep;

            var result = new GaussResult();

            for (int iteration = 1; iteration <= maxIterations; iteration++)
            {
                double previousValue = currentValue;
                bool movedInCycle = false;

                for (int i = 0; i < n; i++)
                {
                    double originalCoord = currentPoint[i];
                    double originalValue = currentValue;

                    currentPoint[i] = originalCoord + step;
                    double plusValue = func(currentPoint);

                    if (plusValue < currentValue)
                    {
                        currentValue = plusValue;
                        movedInCycle = true;
                    }
                    else
                    {
                        currentPoint[i] = originalCoord - step;
                        double minusValue = func(currentPoint);

                        if (minusValue < currentValue)
                        {
                            currentValue = minusValue;
                            movedInCycle = true;
                        }
                        else
                        {
                            currentPoint[i] = originalCoord;
                            currentValue = originalValue;
                        }
                    }
                }

                result.IterationLog.Add(
                    $"{iteration}: k = {FormatPoint(currentPoint)}; I = {FormatNumber(currentValue)}; h = {FormatNumber(step)}");

                if (!movedInCycle)
                {
                    step *= stepReduction;
                }

                if (Math.Abs(previousValue - currentValue) < tolerance && step < tolerance)
                {
                    result.Converged = true;
                    result.Iterations = iteration;
                    break;
                }

                result.Iterations = iteration;
            }

            result.BestPoint = currentPoint;
            result.BestValue = currentValue;
            result.FinalStep = step;

            return result;
        }

        private static string FormatPoint(double[] point)
        {
            return "[" + string.Join("; ", point.Select((v, i) => $"k{i + 1}={FormatNumber(v)}")) + "]";
        }

        private static string FormatNumber(double value)
        {
            if (Math.Abs(value) < 1e-12)
                value = 0;

            return value.ToString("0.############");
        }
    }

    public class GaussResult
    {
        public double[] BestPoint { get; set; }
        public double BestValue { get; set; }
        public int Iterations { get; set; }
        public bool Converged { get; set; }
        public double FinalStep { get; set; }

        public List<string> IterationLog { get; set; } = new List<string>();
    }
}