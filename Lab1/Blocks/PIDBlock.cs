using Lab1.Blocks;

public class PIDBlock : BaseBlock
{
    private double dt, prevE = 0, sum = 0;
    public double Kp { get; set; } = 1.0;
    public double Ki { get; set; } = 0.5;
    public double Kd { get; set; } = 0.1;
    public double Setpoint { get; set; } = 1.0;
    public bool IsAuto { get; set; } = false;

    public PIDBlock(double dt) { this.dt = dt; }

    public override double Calc(double measuredValue)
    {
        double e = Setpoint - measuredValue;

        double pTerm = Kp * e;
        double dTerm = Kd * (e - prevE) / dt;

        double u = pTerm + Ki * sum + dTerm;

        if (u > 1.0)
        {
            u = 1.0;
            if (Math.Abs(Ki) > 0.0001)
                sum = (1.0 - pTerm - dTerm) / Ki;
        }
        else if (u < 0.0)
        {
            u = 0.0;
            if (Math.Abs(Ki) > 0.0001)
                sum = (0 - pTerm - dTerm) / Ki;
        }
        else
        {
            sum += (e + prevE) * dt / 2;
        }

        prevE = e;
        return u;
    }

    public void Sync(double manualOutput, double measuredValue)
    {
        double e = Setpoint - measuredValue;
        if (Math.Abs(Ki) > 0.0001)
            sum = (manualOutput - Kp * e - Kd * 0) / Ki;
        prevE = e;
    }
}