using System.Collections.Generic;

[System.Serializable]
public class AxisThrusters
{
    public List<Thruster> negative;
    public List<Thruster> positive;

    public void TurnOnPositiveThrusters()
    {
        foreach (Thruster thruster in positive)
        {
            thruster.TurnOn();
        }
    }

    public void TurnOnNegativeThrusters()
    {
        foreach (Thruster thruster in negative)
        {
            thruster.TurnOn();
        }
    }

    public void TurnOffRunningThrusters()
    {
        foreach (Thruster thruster in positive)
        {
            if (thruster.IsRunning)
            {
                thruster.TurnOff();
            }
        }

        foreach (Thruster thruster in negative)
        {
            if (thruster.IsRunning)
            {
                thruster.TurnOff();
            }
        }
    }
}
