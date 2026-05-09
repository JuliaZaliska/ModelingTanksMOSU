using Lab1.Blocks;

namespace Lab1.Methods
{
    public enum CriterionType
    {
        IntegralSquare,
        DynamicDeviation
    }

    public enum CriterionTarget
    {
        Both,
        Z1,
        Z2
    }

    public class SynthesisSettings
    {
        public double Dt { get; set; } = 0.01;
        public double MaxTime { get; set; } = 30.0;
        public double Alpha { get; set; } = 0.0;

        public double StartZ1 { get; set; } = 0.0;
        public double StartZ2 { get; set; } = 0.0;

        public double Setpoint1 { get; set; } = 5.0;
        public double Setpoint2 { get; set; } = 10.0;

        public double Xout2 { get; set; } = 0.5;

        public double MinZ { get; set; } = 0.0;
        public double MaxZ1 { get; set; } = 20.0;
        public double MaxZ2 { get; set; } = 20.0;

        public double ParameterMin { get; set; } = 0.0;
        public double ParameterMax { get; set; } = 20.0;
    }

    public class SimulationResult
    {
        public List<double> Time { get; set; } = new List<double>();
        public List<double> Z1 { get; set; } = new List<double>();
        public List<double> Z2 { get; set; } = new List<double>();

        public double IntegralSquareCriterion { get; set; }
        public double IntegralSquareCriterionZ1 { get; set; }
        public double IntegralSquareCriterionZ2 { get; set; }

        public double DynamicDeviationCriterion { get; set; }
        public double DynamicDeviationCriterionZ1 { get; set; }
        public double DynamicDeviationCriterionZ2 { get; set; }
    }

    public static class Criterion
    {
        public static double Calculate(
            double[] parameters,
            SynthesisSettings settings,
            CriterionType type,
            CriterionTarget target = CriterionTarget.Both)
        {
            if (!AreParametersCorrect(parameters, settings))
                return 1e12;

            SimulationResult simulation = Simulate(parameters, settings);

            if (type == CriterionType.IntegralSquare)
            {
                if (target == CriterionTarget.Z1)
                    return simulation.IntegralSquareCriterionZ1;

                if (target == CriterionTarget.Z2)
                    return simulation.IntegralSquareCriterionZ2;

                return simulation.IntegralSquareCriterion;
            }

            if (target == CriterionTarget.Z1)
                return simulation.DynamicDeviationCriterionZ1;

            if (target == CriterionTarget.Z2)
                return simulation.DynamicDeviationCriterionZ2;

            return simulation.DynamicDeviationCriterion;
        }

        public static SimulationResult Simulate(double[] parameters, SynthesisSettings settings)
        {
            var result = new SimulationResult();

            double kp1 = parameters[0];
            double ki1 = parameters[1];
            double kd1 = parameters[2];

            double kp2 = parameters[3];
            double ki2 = parameters[4];
            double kd2 = parameters[5];

            double z1 = settings.StartZ1;
            double z2 = settings.StartZ2;

            var tank1 = new LimitedIntBlock(settings.Dt, settings.MinZ, settings.MaxZ1, z1);
            var tank2 = new LimitedIntBlock(settings.Dt, settings.MinZ, settings.MaxZ2, z2);

            var pid1 = new PIDBlock(settings.Dt)
            {
                Kp = kp1,
                Ki = ki1,
                Kd = kd1,
                Setpoint = settings.Setpoint1
            };

            var pid2 = new PIDBlock(settings.Dt)
            {
                Kp = kp2,
                Ki = ki2,
                Kd = kd2,
                Setpoint = settings.Setpoint2
            };

            int stepsCount = (int)(settings.MaxTime / settings.Dt);

            double integralSquareZ1 = 0.0;
            double integralSquareZ2 = 0.0;

            double dynamicDeviationZ1 = 0.0;
            double dynamicDeviationZ2 = 0.0;

            double directionZ1 = Math.Sign(settings.Setpoint1 - settings.StartZ1);
            double directionZ2 = Math.Sign(settings.Setpoint2 - settings.StartZ2);

            if (directionZ1 == 0) directionZ1 = 1;
            if (directionZ2 == 0) directionZ2 = 1;

            for (int step = 0; step <= stepsCount; step++)
            {
                double time = step * settings.Dt;

                double e1 = settings.Setpoint1 - z1;
                double e2 = settings.Setpoint2 - z2;

                double timeWeight = Math.Pow(Math.Max(time, settings.Dt), settings.Alpha);

                integralSquareZ1 += e1 * e1 * timeWeight * settings.Dt;
                integralSquareZ2 += e2 * e2 * timeWeight * settings.Dt;

                // Динамічне відхилення як викид за завдання:
                // якщо процес іде вгору, рахуємо перевищення над setpoint;
                // якщо процес іде вниз, рахуємо провал нижче setpoint.
                double overshootZ1 = directionZ1 * (z1 - settings.Setpoint1);
                double overshootZ2 = directionZ2 * (z2 - settings.Setpoint2);

                if (overshootZ1 > dynamicDeviationZ1)
                    dynamicDeviationZ1 = overshootZ1;

                if (overshootZ2 > dynamicDeviationZ2)
                    dynamicDeviationZ2 = overshootZ2;

                result.Time.Add(time);
                result.Z1.Add(z1);
                result.Z2.Add(z2);

                double x_in1 = pid1.Calc(z1);
                double x_in2 = pid2.Calc(z2);

                double G_in1 = x_in1;
                double G_in2 = x_in2;

                double G_out2 = (z2 > 0.01) ? settings.Xout2 : 0.0;
                double G_out1 = (z1 > 0.01) ? 0.1 : 0.0;
                double G_12 = (z1 > 0.01) ? 0.1 : 0.0;

                z1 = tank1.Calc(G_in1 - G_12 - G_out1);
                z2 = tank2.Calc(G_in2 + G_12 - G_out2);

                if (double.IsNaN(z1) || double.IsNaN(z2) ||
                    double.IsInfinity(z1) || double.IsInfinity(z2))
                {
                    result.IntegralSquareCriterion = 1e12;
                    result.IntegralSquareCriterionZ1 = 1e12;
                    result.IntegralSquareCriterionZ2 = 1e12;

                    result.DynamicDeviationCriterion = 1e12;
                    result.DynamicDeviationCriterionZ1 = 1e12;
                    result.DynamicDeviationCriterionZ2 = 1e12;

                    return result;
                }
            }

            result.IntegralSquareCriterionZ1 = integralSquareZ1;
            result.IntegralSquareCriterionZ2 = integralSquareZ2;
            result.IntegralSquareCriterion = integralSquareZ1 + integralSquareZ2;

            result.DynamicDeviationCriterionZ1 = dynamicDeviationZ1;
            result.DynamicDeviationCriterionZ2 = dynamicDeviationZ2;
            result.DynamicDeviationCriterion = Math.Max(dynamicDeviationZ1, dynamicDeviationZ2);

            return result;
        }

        private static bool AreParametersCorrect(double[] parameters, SynthesisSettings settings)
        {
            if (parameters == null || parameters.Length != 6)
                return false;

            foreach (double value in parameters)
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    return false;

                if (value < settings.ParameterMin || value > settings.ParameterMax)
                    return false;
            }

            return true;
        }
    }
}