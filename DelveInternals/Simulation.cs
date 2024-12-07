using DelveInternals.Units;
using KaimiraGames;

namespace DelveInternals;

public class Simulation
{
    private Unit[] _playerTeam;
    private Random _rng = new();
    // the difficulty of monsters and the amount of gold rewarded will both scale with total length of the delve
    // public read access to let the game UI display total rooms cleared at the end.
    public int TotalRooms { get; private set; } = 0;
    // MIT-Licensed RNG my beloved
    private WeightedList<RoomType> _roomTable = new();
    // track the amount of gold earned during this run so the game can display it to the player
    public int TotalGoldCollected { get; private set; }
    
    public Simulation(Unit[] team)
    {
        _playerTeam = team;
        _roomTable.Add(RoomType.Encounter, 90);
        _roomTable.Add(RoomType.Treasure, 10);
        
        // reset team combat stats
        foreach (var unit in _playerTeam)
        {
            unit.CombatHealth = unit.Health;
            unit.CombatAttack = unit.Attack;
            unit.CombatDefense = unit.Defense;
        }
    }

    public IEnumerable<string> RunSimulation()
    {
        while (!SimOver())
        {
            var roomType = _roomTable.Next();

            if (roomType is RoomType.Encounter)
            {
                // to make things easier on myself, enemy health and damage and player damage will all be pooled.
                // this makes the simulation a lot simpler
                // i can revisit this later if i want to make it more complex.
                var enemyHealth = 500 + (TotalRooms * 30);
                var enemyDamage = 100 + (TotalRooms * 10);
                // only pool attack of units still alive
                var playerDamage = _playerTeam.Sum(unit => (unit.CombatHealth > 0) ? unit.CombatAttack : 0);
                var attackTable = new WeightedList<Unit>();
                foreach (var unit in _playerTeam)
                {
                    Console.WriteLine($"{unit.GetName()} | {unit.CombatHealth}");
                    if(unit.CombatHealth <= 0)continue;
                    if (unit is Knight) attackTable.Add(unit, 30);
                    else attackTable.Add(unit, 10);
                }

                // player gets first hit
                enemyHealth -= playerDamage;
                while (enemyHealth > 0)
                {
                    var unitToAttack = attackTable.Next();
                    float dmgReduction = ((float)unitToAttack.CombatDefense / 2) / 100;
                    var damagePostReduction = (int)MathF.Round(dmgReduction * enemyDamage); 
                    unitToAttack.CombatHealth -= damagePostReduction;
                    enemyHealth -= playerDamage;
                }

                var loot = 20 + (TotalRooms * (2 + _rng.Next(1,6)));
                TotalGoldCollected += loot;
                
                // every time the player does not find a treasure chest the chance should increase
                _roomTable.SetWeight(RoomType.Encounter, _roomTable.GetWeightOf(RoomType.Encounter) - 5);
                _roomTable.SetWeight(RoomType.Treasure, _roomTable.GetWeightOf(RoomType.Treasure) + 5);

                yield return $"Fought monsters and gained {loot} gold. {attackTable.Count} units alive.";
            }
            else if (roomType is RoomType.Treasure)
            {
                // every time we find a chest, set the weights back to normal
                _roomTable.SetWeight(RoomType.Encounter, 90); 
                _roomTable.SetWeight(RoomType.Treasure, 10);
                
                var treasure = 100 + (TotalRooms * 50) + _rng.Next(0,100);
                TotalGoldCollected += treasure;
                yield return $"Found a treasure chest! It contained {treasure} gold.";
            }

            TotalRooms++;
        }

        yield return "Your team of adventurers has tired and returned to town.";
    }

    private bool SimOver()
    {
        foreach (var unit in _playerTeam)
        {
            // if at least one player unit is alive, the sim should continue
            if (unit.CombatHealth > 0) return false;
        }

        return true;
    }
}