public class MainEngine : Engine
{
    public float fuel;
    public float maxThrust = 100f;

    public override void TurnOn()
    {
        IsRunning = true;
        force = maxThrust;
    }

    public override void TurnOff()
    {
        IsRunning = false;
        force = 0;
    }

    public void Toggle()
    {
        if (IsRunning)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
