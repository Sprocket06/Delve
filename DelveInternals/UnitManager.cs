using DelveInternals.Units;

namespace DelveInternals;

public static class UnitManager
{
   public static List<Unit> Units;

   static UnitManager()
   {
      Units = new() { new Knight(), new Warrior(), new Mage(), new Healer() };
   }
}