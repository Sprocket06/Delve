namespace DelveInternals.Units;

public abstract class Unit
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string Description { get; set; }
    // declare another set of properties separate from "Base" stats for use in the combat simulation 
    public int CombatHealth { get; set; }
    public int CombatAttack { get; set; }
    public int CombatDefense { get; set; }

    public Unit(int health, int attack, int defense, string desc)
    {
        Health = health;
        Attack = attack;
        Defense = defense;
        Description = desc;
        CombatHealth = Health;
        CombatAttack = Attack;
        CombatDefense = Defense;
    }

    public string GetName()
    {
        return GetType().Name;
    }

    public abstract Unit UnitFactory();
}