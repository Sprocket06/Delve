namespace DelveInternals.Units;

public class Warrior : Unit
{
   public Warrior() : base(100, 50, 25, "A simple but powerful melee unit.")
   {
   }

   public override Unit UnitFactory()
   {
      return new Warrior();
   }
}