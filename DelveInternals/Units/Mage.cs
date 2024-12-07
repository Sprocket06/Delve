namespace DelveInternals.Units;

public class Mage : Unit
{
    public Mage() : base(70, 150, 20, "A powerful ranged attacker with low health.")
    {
    }

    public override Unit UnitFactory()
    {
        return new Mage();
    }
}