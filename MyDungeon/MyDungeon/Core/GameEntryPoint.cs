using MyDungeon.Base;
using MyDungeon.Enemies;
using MyDungeon.MyDungeon.Base.Armor;
using MyDungeon.MyDungeon.Base.Armour;
using MyDungeon.MyDungeon.Base.Dodge;
using MyListLib;

internal class GameEntryPoint
{
    private static MyList<Enemy> _enemies = new MyList<Enemy>();

    public void RunGame()
    {
        Console.WriteLine("Welcome to My Game\n");

        while (true)
        {
            PrintMenu();
            string input = ReadInput();

            switch (input)
            {
                case "1": AddZombie() ; break;
                case "2": TakeDamageToMonster(_enemies[0]); break;
                case "3": UpgradeArmor(_enemies[0], ArmorValues.IRON); break;
                case "4": UpgradeArmor(_enemies[0], ArmorValues.GOLDEN); break;
                case "5": AddDodgeChance(_enemies[0], DodgeChances.LOW); break;
                case "6": PrintAllEnemies(); break;
                case "q": return;
                default: Console.WriteLine("Неверный ввод!"); break;
            }

            Console.WriteLine();
        }
    }

    private static void AddZombie() =>
        _enemies.Add(new Zombie(150, "Zombie № " + _enemies.Count));

    private void PrintMenu()
    {
        Console.WriteLine("Меню (выберете один из вариантов)");
        Console.WriteLine("1) Создать зомби");
        Console.WriteLine("2) Нанести урон выбранному монстру");
        Console.WriteLine("3) Улучшить первого монстра (выдать железную броню)");
        Console.WriteLine("4) Улучшить первого монстра (выдать золотую броню)");
        Console.WriteLine("5) Улучшить первого монстра (выдать шанс уворота)");
        Console.WriteLine("6) Вывести данные о всех монстрах");
        Console.WriteLine("Введите \"q\" для выхода");
        Console.Write("Ваш выбор: ");
    }

    private static void TakeDamageToMonster(Enemy enemy)
    {
        Console.Write("Enter damage: ");
        var input = ReadInput();

        if (int.TryParse(input, out var damage))
        {
            var oldHp = enemy.HealthPoint;
            enemy.TakeDamage(damage);
            var newHp = enemy.HealthPoint;

            Console.WriteLine($"{enemy.Name} took {damage}. Hp: {oldHp} -> {newHp}");
        }
        else
        {
            Console.WriteLine($"Invalid damage {input}");
        }
    }

    private static void UpgradeArmor(Enemy enemy, int armorValue)
    {
        var currentHealth = enemy.HealthComponent;
        enemy.HealthComponent = new ArmorHealth(currentHealth, armorValue);
        Console.WriteLine($"Броня {armorValue} добавлена!");
    }

    private static void AddDodgeChance(Enemy enemy, float dodgeChance)
    {
        var health = enemy.HealthComponent;
        health = new DodgeChanceHealth(health, dodgeChance);
        enemy.HealthComponent = health;
    }

    private void PrintAllEnemies()
    {
        if (_enemies.Count == 0)
        {
            Console.WriteLine("Нет монстров");
            return;
        }

        Console.WriteLine($"Всего монстров: {_enemies.Count}");
    }

    private static string ReadInput() =>
        Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
}