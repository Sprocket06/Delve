namespace DelveInternals.Units;

public class Knight : Unit
{
    public Knight() : base(100, 25, 50, "A durable front-liner with a decent melee attack. Draws aggro.")
    {
    }

    public override Unit UnitFactory()
    {
        return new Knight();
    }
}