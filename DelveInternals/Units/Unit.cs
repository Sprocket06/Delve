namespace DelveInternals.Units;

public class Unit
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string Description { get; set; }

    public Unit(int health, int attack, int defense)
    {
        Health = health;
        Attack = attack;
        Defense = defense;
    }
}