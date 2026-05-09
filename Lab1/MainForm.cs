using Lab1.Blocks;
using Lab1.Methods;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab1
{
    public partial class MainForm : Form
    {
        double x_in1, x_in2, x_out2,
               G_in1, G_in2, G_12, G_out1, G_out2,
               time = 0, dt = 0.01,
               z1 = 0, z2 = 0,
               min_z = 0, max_z1 = 20,
                          max_z2 = 20;
        double setpoint1, setpoint2, kp1, kp2, ki1, ki2, kd1, kd2;
        bool isAuto = false;
        private PIDBlock pid1, pid2;

        private LimitedIntBlock tank1, tank2;
        private LimitBlock z1Limit, z2Limit;

        public MainForm()
        {
            InitializeComponent();

            tank1 = new LimitedIntBlock(dt, min_z, max_z1, z1);
            tank2 = new LimitedIntBlock(dt, min_z, max_z2, z2);

            z1Limit = new LimitBlock(min_z, max_z1);
            z2Limit = new LimitBlock(min_z, max_z2);

            pid1 = new PIDBlock(dt);
            pid2 = new PIDBlock(dt);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            double.TryParse(tbSetZ1.Text, out z1);
            double.TryParse(tbSetZ2.Text, out z2);

            double.TryParse(tbSetKp1.Text, out kp1);
            double.TryParse(tbSetKp2.Text, out kp2);
            double.TryParse(tbSetKi1.Text, out ki1);
            double.TryParse(tbSetKi2.Text, out ki2);
            double.TryParse(tbSetKd1.Text, out kd1);
            double.TryParse(tbSetKd2.Text, out kd2);
            double.TryParse(tbSetPointZ1.Text, out setpoint1);
            double.TryParse(tbSetPointZ2.Text, out setpoint2);

            z1 = z1Limit.Calc(z1);
            z2 = z2Limit.Calc(z2);
            tank1.z = z1;
            tank2.z = z2;

            time = 0;
            ClearAllChartPoints();

            lbCurrentZ1.Text = $"Current z1: {z1:F2}";
            lbCurrentZ2.Text = $"Current z2: {z2:F2}";

            tmModel.Start();
            btnPause.Text = "Pause";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (tmModel.Enabled)
            {
                tmModel.Stop();
                btnPause.Text = "Resume";
            }
            else
            {
                tmModel.Start();
                btnPause.Text = "Pause";
            }

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            //tmModel.Stop();

            time = 0;

            z1 = 0;
            z2 = 0;
            tank1.z = z1;
            tank2.z = z2;

            /*x_in1 = 0;
            x_in2 = 0;
            x_out2 = 0;

            trbXin1.Value = 0;
            trbXin2.Value = 0;
            trbXout2.Value = 0;
            lbXin1.Text = "0.0";
            lbXin2.Text = "0.0";
            lbXout2.Text = "0.0";*/

            lbCurrentZ1.Text = $"Current z1: {z1:F2}";
            lbCurrentZ2.Text = $"Current z2: {z2:F2}";

            ClearAllChartPoints();
            ClearParamFields();
        }

        private void tmModel_Tick(object sender, EventArgs e)
        {
            double.TryParse(tbSetKp1.Text, out kp1);
            double.TryParse(tbSetKp2.Text, out kp2);
            double.TryParse(tbSetKi1.Text, out ki1);
            double.TryParse(tbSetKi2.Text, out ki2);
            double.TryParse(tbSetKd1.Text, out kd1);
            double.TryParse(tbSetKd2.Text, out kd2);
            double.TryParse(tbSetPointZ1.Text, out setpoint1);
            double.TryParse(tbSetPointZ2.Text, out setpoint2);

            pid1.Kp = kp1;
            pid1.Ki = ki1;
            pid1.Kd = kd1;
            pid1.Setpoint = setpoint1;

            pid2.Kp = kp2;
            pid2.Ki = ki2;
            pid2.Kd = kd2;
            pid2.Setpoint = setpoint2;

            if (isAuto)
            {
                x_in1 = pid1.Calc(z1);
                x_in2 = pid2.Calc(z2);
            }
            else
            {
                pid1.Sync(x_in1, z1);
                pid2.Sync(x_in2, z2);
            }

            G_in1 = x_in1;
            G_in2 = x_in2;

            G_out2 = (z2 > 0.01) ? x_out2 : 0;
            G_out1 = (z1 > 0.01) ? 0.1 : 0;
            G_12 = (z1 > 0.01) ? 0.1 : 0;

            z1 = tank1.Calc(G_in1 - G_12 - G_out1);
            z2 = tank2.Calc(G_in2 + G_12 - G_out2);

            lbCurrentZ1.Text = $"Current z1: {z1:F3}";
            lbCurrentZ2.Text = $"Current z2: {z2:F3}";
            lbXin1.Text = $"{x_in1:F3}";
            lbXin2.Text = $"{x_in2:F3}";

            chMain.Series[0].Points.AddXY(time, z1);
            chMain.Series[1].Points.AddXY(time, z2);
            chMain.Series[2].Points.AddXY(time, setpoint1);
            chMain.Series[3].Points.AddXY(time, setpoint2);

            time += dt;
        }

        private void trbXin1_Scroll(object sender, EventArgs e)
        {
            ChangeX(ref x_in1, trbXin1.Value, lbXin1);
        }

        private void trbXin2_Scroll(object sender, EventArgs e)
        {
            ChangeX(ref x_in2, trbXin2.Value, lbXin2);
        }

        private void trbXout1_Scroll(object sender, EventArgs e)
        {
            ChangeX(ref x_out2, trbXout2.Value, lbXout2);
        }

        private void ChangeX(ref double x, double tb, Label lb)
        {
            x = tb / 10.0;
            lb.Text = $"{x:F1}";
        }

        private void ClearAllChartPoints()
        {
            foreach (Series series in chMain.Series)
            {
                series.Points.Clear();
            }
        }
        private void ClearParamFields()
        {
            tbSetKp1.Clear();
            tbSetKi1.Clear();
            tbSetKd1.Clear();

            tbSetKp2.Clear();
            tbSetKi2.Clear();
            tbSetKd2.Clear();

/*          tbSetPointZ1.Clear();
            tbSetPointZ2.Clear();*/
        }

        private void btnSpeed1_Click(object sender, EventArgs e)
        {
            tmModel.Interval = 100;
        }

        private void btnSpeed10_Click(object sender, EventArgs e)
        {
            tmModel.Interval = 10;
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            isAuto = !isAuto;
            btnMode.Text = isAuto ? "Auto" : "Manual";
            trbXin1.Enabled = !isAuto;
            trbXin2.Enabled = !isAuto;
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            tmModel.Stop();

            double.TryParse(tbSetZ1.Text, out double startZ1);
            double.TryParse(tbSetZ2.Text, out double startZ2);

            double.TryParse(tbSetPointZ1.Text, out double targetZ1);
            double.TryParse(tbSetPointZ2.Text, out double targetZ2);

            double.TryParse(tbSetKp1.Text, out double startKp1);
            double.TryParse(tbSetKi1.Text, out double startKi1);
            double.TryParse(tbSetKd1.Text, out double startKd1);

            double.TryParse(tbSetKp2.Text, out double startKp2);
            double.TryParse(tbSetKi2.Text, out double startKi2);
            double.TryParse(tbSetKd2.Text, out double startKd2);

            if (targetZ1 <= 0)
            {
                targetZ1 = 2;
                tbSetPointZ1.Text = targetZ1.ToString("0.####");
            }

            if (targetZ2 <= 0)
            {
                targetZ2 = 4;
                tbSetPointZ2.Text = targetZ2.ToString("0.####");
            }

            double[] startParameters =
            {
                startKp1, startKi1, startKd1,
                startKp2, startKi2, startKd2
            };

            var settings = new SynthesisSettings
            {
                Dt = dt,
                MaxTime = 15,
                Alpha = 0,

                StartZ1 = startZ1,
                StartZ2 = startZ2,

                Setpoint1 = targetZ1,
                Setpoint2 = targetZ2,

                Xout2 = x_out2 > 0 ? x_out2 : 0.5,

                MinZ = min_z,
                MaxZ1 = max_z1,
                MaxZ2 = max_z2
            };

            var gaussMethod = new GaussMethod();

            double tolerance = 1e-6;
            int maxIterations = 1000;
            double initialStep = 0.1;
            double stepReduction = 0.5;

            double startIntegralCriterionZ1 = Criterion.Calculate(
                startParameters,
                settings,
                CriterionType.IntegralSquare,
                CriterionTarget.Z1);

            double startIntegralCriterionZ2 = Criterion.Calculate(
                startParameters,
                settings,
                CriterionType.IntegralSquare,
                CriterionTarget.Z2);

            double startDynamicCriterionZ1 = Criterion.Calculate(
                startParameters,
                settings,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z1);

            double startDynamicCriterionZ2 = Criterion.Calculate(
                startParameters,
                settings,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z2);

            GaussResult integralPid1Result = OptimizeSinglePid(
                gaussMethod,
                CriterionType.IntegralSquare,
                CriterionTarget.Z1,
                startParameters,
                settings,
                tolerance,
                maxIterations,
                initialStep,
                stepReduction);

            double[] integralAfterPid1 = ReplacePidParameters(
                startParameters,
                integralPid1Result.BestPoint,
                CriterionTarget.Z1);

            GaussResult integralPid2Result = OptimizeSinglePid(
                gaussMethod,
                CriterionType.IntegralSquare,
                CriterionTarget.Z2,
                integralAfterPid1,
                settings,
                tolerance,
                maxIterations,
                initialStep,
                stepReduction);

            double[] integralBestParameters = ReplacePidParameters(
                integralAfterPid1,
                integralPid2Result.BestPoint,
                CriterionTarget.Z2);

            GaussResult dynamicPid1Result = OptimizeSinglePid(
                gaussMethod,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z1,
                startParameters,
                settings,
                tolerance,
                maxIterations,
                initialStep,
                stepReduction);

            double[] dynamicAfterPid1 = ReplacePidParameters(
                startParameters,
                dynamicPid1Result.BestPoint,
                CriterionTarget.Z1);

            GaussResult dynamicPid2Result = OptimizeSinglePid(
                gaussMethod,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z2,
                dynamicAfterPid1,
                settings,
                tolerance,
                maxIterations,
                initialStep,
                stepReduction);

            double[] dynamicBestParameters = ReplacePidParameters(
                dynamicAfterPid1,
                dynamicPid2Result.BestPoint,
                CriterionTarget.Z2);

            double finalIntegralCriterionZ1 = Criterion.Calculate(
                integralBestParameters,
                settings,
                CriterionType.IntegralSquare,
                CriterionTarget.Z1);

            double finalIntegralCriterionZ2 = Criterion.Calculate(
                integralBestParameters,
                settings,
                CriterionType.IntegralSquare,
                CriterionTarget.Z2);

            double finalDynamicCriterionZ1 = Criterion.Calculate(
                dynamicBestParameters,
                settings,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z1);

            double finalDynamicCriterionZ2 = Criterion.Calculate(
                dynamicBestParameters,
                settings,
                CriterionType.DynamicDeviation,
                CriterionTarget.Z2);

            ShowProcesses(startParameters, integralBestParameters, settings);

            tbSetKp1.Text = integralBestParameters[0].ToString("0.####");
            tbSetKi1.Text = integralBestParameters[1].ToString("0.####");
            tbSetKd1.Text = integralBestParameters[2].ToString("0.####");

            tbSetKp2.Text = integralBestParameters[3].ToString("0.####");
            tbSetKi2.Text = integralBestParameters[4].ToString("0.####");
            tbSetKd2.Text = integralBestParameters[5].ToString("0.####");

            var message = new StringBuilder();

            message.AppendLine("Регулятори налаштовуються окремо:");
            message.AppendLine("PID1: k1 = [Kp1, Ki1, Kd1]");
            message.AppendLine("PID2: k2 = [Kp2, Ki2, Kd2]");
            message.AppendLine("Під час оптимізації одного PID-регулятора параметри іншого залишаються сталими.");
            message.AppendLine();

            message.AppendLine("Початкові параметри:");
            message.AppendLine(FormatPidParameters(startParameters));
            message.AppendLine("Задані значення уставок:");
            message.AppendLine($"Setpoint z1 = {targetZ1:0.####}");
            message.AppendLine($"Setpoint z2 = {targetZ2:0.####}");
            message.AppendLine();

            message.AppendLine("Оптимізація за інтегральним квадратичним критерієм:");
            message.AppendLine($"I1поч = {startIntegralCriterionZ1:0.####}");
            message.AppendLine($"I1опт = {finalIntegralCriterionZ1:0.####}");
            message.AppendLine($"Ітерацій PID1 = {integralPid1Result.Iterations}");
            message.AppendLine($"Збіжність PID1 = {(integralPid1Result.Converged ? "так" : "ні")}");
            message.AppendLine();
            message.AppendLine($"I2поч = {startIntegralCriterionZ2:0.####}");
            message.AppendLine($"I2опт = {finalIntegralCriterionZ2:0.####}");
            message.AppendLine($"Ітерацій PID2 = {integralPid2Result.Iterations}");
            message.AppendLine($"Збіжність PID2 = {(integralPid2Result.Converged ? "так" : "ні")}");
            message.AppendLine("Оптимальні параметри:");
            message.AppendLine(FormatPidParameters(integralBestParameters));
            message.AppendLine();

            message.AppendLine("Оптимізація за мінімальним динамічним відхиленням:");
            message.AppendLine($"D1поч = {startDynamicCriterionZ1:0.####}");
            message.AppendLine($"D1опт = {finalDynamicCriterionZ1:0.####}");
            message.AppendLine($"Ітерацій PID1 = {dynamicPid1Result.Iterations}");
            message.AppendLine($"Збіжність PID1 = {(dynamicPid1Result.Converged ? "так" : "ні")}");
            message.AppendLine();
            message.AppendLine($"D2поч = {startDynamicCriterionZ2:0.####}");
            message.AppendLine($"D2опт = {finalDynamicCriterionZ2:0.####}");
            message.AppendLine($"Ітерацій PID2 = {dynamicPid2Result.Iterations}");
            message.AppendLine($"Збіжність PID2 = {(dynamicPid2Result.Converged ? "так" : "ні")}");
            message.AppendLine("Оптимальні параметри:");
            message.AppendLine(FormatPidParameters(dynamicBestParameters));

            MessageBox.Show(message.ToString(), "Результати параметричного синтезу системи регулювання");
        }

        private GaussResult OptimizeSinglePid(
            GaussMethod gaussMethod,
            CriterionType criterionType,
            CriterionTarget target,
            double[] baseParameters,
            SynthesisSettings settings,
            double tolerance,
            int maxIterations,
            double initialStep,
            double stepReduction)
        {
            int offset = target == CriterionTarget.Z1 ? 0 : 3;

            double[] startPidParameters =
            {
                baseParameters[offset],
                baseParameters[offset + 1],
                baseParameters[offset + 2]
            };

            return gaussMethod.Optimize(
                pidParameters => Criterion.Calculate(
                    ReplacePidParameters(baseParameters, pidParameters, target),
                    settings,
                    criterionType,
                    target),
                startPidParameters,
                tolerance,
                maxIterations,
                initialStep,
                stepReduction);
        }

        private double[] ReplacePidParameters(
            double[] baseParameters,
            double[] pidParameters,
            CriterionTarget target)
        {
            double[] result = (double[])baseParameters.Clone();

            int offset = target == CriterionTarget.Z1 ? 0 : 3;

            result[offset] = pidParameters[0];
            result[offset + 1] = pidParameters[1];
            result[offset + 2] = pidParameters[2];

            return result;
        }

        private string FormatPidParameters(double[] p)
        {
            return
                $"Kp1 = {p[0]:0.####}, Ki1 = {p[1]:0.####}, Kd1 = {p[2]:0.####}\n" +
                $"Kp2 = {p[3]:0.####}, Ki2 = {p[4]:0.####}, Kd2 = {p[5]:0.####}";
        }

        private void ShowProcesses(double[] beforeParameters, double[] afterParameters, SynthesisSettings settings)
        {
            SimulationResult before = Criterion.Simulate(beforeParameters, settings);
            SimulationResult after = Criterion.Simulate(afterParameters, settings);

            ClearAllChartPoints();

            Series z1Before = chMain.Series[4];
            Series z2Before = chMain.Series[5];
            Series z1After = chMain.Series[6];
            Series z2After = chMain.Series[7];
            Series setpoint1Series = chMain.Series[8];
            Series setpoint2Series = chMain.Series[9];

            for (int i = 0; i < before.Time.Count; i++)
            {
                double t = before.Time[i];

                z1Before.Points.AddXY(t, before.Z1[i]);
                z2Before.Points.AddXY(t, before.Z2[i]);

                z1After.Points.AddXY(t, after.Z1[i]);
                z2After.Points.AddXY(t, after.Z2[i]);

                setpoint1Series.Points.AddXY(t, settings.Setpoint1);
                setpoint2Series.Points.AddXY(t, settings.Setpoint2);
            }
        }

    }
}
