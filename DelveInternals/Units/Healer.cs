namespace DelveInternals.Units;

public class Healer : Unit
{
    public Healer() : base(200, 50, 25, "Attack heals allies instead of doing damage.")
    {
    }

    public override Unit UnitFactory()
    {
        return new Healer();
    }
}