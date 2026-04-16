using Lab1.Blocks;

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
            chMain.Series[0].Points.Clear();
            chMain.Series[1].Points.Clear();
            chMain.Series[2].Points.Clear();
            chMain.Series[3].Points.Clear();

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

            chMain.Series[0].Points.Clear();
            chMain.Series[1].Points.Clear();
            chMain.Series[2].Points.Clear();
            chMain.Series[3].Points.Clear();
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
    }
}