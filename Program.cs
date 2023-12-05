namespace FinalProject;

class Weapon
{
    public string Name { get; }
    public int Damage { get; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}

class Player
{
    public string Name { get; }
    public int Health { get; private set; }
    private Weapon? equippedWeapon;

    public bool IsAlive => Health > 0;

    public Player(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
    }

    public void Attack(Enemy enemy)
    {
        int damage = equippedWeapon != null ? equippedWeapon.Damage : 1;
        enemy.TakeDamage(damage);
        Console.WriteLine($"You attack with {equippedWeapon?.Name ?? "your fists"} for {damage} damage!");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"You take {damage} damage!");
    }

    public string GetEquippedWeaponName()
    {
        return equippedWeapon?.Name ?? "fists";
    }
}

class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }

    public bool IsAlive => Health > 0;

    public Enemy(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Attack(Player player)
    {
        Random random = new Random();
        int damage = random.Next(5, 15);
        player.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks you for {damage} damage!");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} takes {damage} damage!");
    }
}
class Program
{
    static Enemy? enemy;
    static void Main(string[] args)
    {
        Player player = new("playerName", 100);
        // enemy = new Enemy("Enemy", 50);

        // Other
        GameTitle();
        First(player);
    }
    public static void GameTitle()
    {
        Console.WriteLine("You are about to start this epic adventure. Please press 'ENTER' now to begin!");
        Console.ReadLine();
        Console.Clear();

    }


    public static void First(Player player)
    {
        string choice;
        string playerName;
        Console.Write("Tell me your name: ");
        playerName = Console.ReadLine()!;
        Console.Clear();

        Console.WriteLine($"Welcome, {playerName}!");
        Console.WriteLine("You have entered the notoriously dangerous dungeon that has spawned in Springfield Missouri. You walk towards the entrance to explore the dungeon for it's treasure. In front of you are three weapons. Which one do you want to take into the dungeon?");
        Console.WriteLine("1. A long Spear with a sharp point and a blunt end.");
        Console.WriteLine("2. A short Sword with sharp edges enough to easily cut through flesh.");
        Console.WriteLine("3. A pair of Daggers with sharp edges, ready to cut down your enemies!");
        Console.Write("Choose your weapon using the number or name: ");
        choice = Console.ReadLine()!.ToLower();
        Console.Clear();

        Enemy enemy = new Enemy("Enemy", 50);

        switch (choice)
        {
            case "1":
            case "Spear":
                {
                    Console.WriteLine("You have chosen the long reaching spear!");
                    player.EquipWeapon(new Weapon("Spear", 20));
                    Second(player, enemy);
                    break;
                }

            case "2":
            case "Sword":
                {
                    Console.WriteLine("You have chosen the mighty Sword!");
                    player.EquipWeapon(new Weapon("Sword", 15));
                    Second(player, enemy);
                    break;
                }

            case "3":
            case "Daggers":
            case "Dagger":
                {
                    Console.WriteLine("You have chosen the mighty pair of Daggers!");
                    player.EquipWeapon(new Weapon("Daggers", 10));
                    Second(player, enemy);
                    break;
                }

            default:
                {
                    Console.WriteLine("Command is invalid...");
                    Console.WriteLine("Press 'Enter' to restart.");
                    Console.ReadLine();
                    First(player);
                    break;
                }
        }
    }
    public static void Second(Player player, Enemy enemy)
    {
        Console.Clear();
        Console.WriteLine("After choosing the weapon you enter the dungeon");
        Console.WriteLine($"After walkiing for not even 4 minutes an enemy appears! You ready your {player.GetEquippedWeaponName()}");

        while (player.IsAlive && enemy.IsAlive)
        {
            Console.WriteLine($"{player.Name} (Health: {player.Health}) vs. {enemy.Name} (Health: {enemy.Health})");

            Console.WriteLine("It's your turn to attack! Type 'attack' to attack:");
            string playerInput = Console.ReadLine()!.ToLower();

            if (playerInput == "attack")
            {
                player.Attack(enemy);

                if (!enemy.IsAlive)
                {
                    Console.Clear();
                    Console.WriteLine($"You defeated {enemy.Name}!");
                    break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid command. Type 'attack' to attack.");
                continue; 
            }


            enemy.Attack(player);


            if (!player.IsAlive)
            {
                Console.Clear();
                Console.WriteLine($"{enemy.Name} defeated you!");
                break;
            }
        }

        if (player.IsAlive)
        {
            Console.WriteLine("Congratulations! You won the battle! But don't be to excited, there is more to come....To be continued.");
        }
        else
        {
            Console.WriteLine("Game over! You were defeated in battle.");
        }
    }
}