using MyListLib;
using MyDungeon.Enemies;
using MyDungeon.Base;

internal class GameEntryPoint
{
    private MyList<Enemy> _enemies = new MyList<Enemy>();
    private Enemy _currentEnemy = null;
    private Random _random = new Random();

    public void RunGame()
    {
        Console.WriteLine("Welcome to My Game\n");

        while (true)
        {
            PrintMenu();
            string input = Console.ReadLine()?.Trim().ToLower();

            switch (input)
            {
                case "1": CreateEnemy<Zombie>(); break;
                case "2": CreateEnemy<Ghost>(); break;
                case "3": DamageSelected(); break;
                case "4": DamageRandom(); break;
                case "5": DestroyCurrentEnemy(); break;
                case "6": PrintAllEnemies(); break;
                case "q": return;
                default: Console.WriteLine("Неверный ввод!"); break;
            }

            Console.WriteLine();
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("Меню (выберете один из вариантов)");
        Console.WriteLine("1) Создать зомби");
        Console.WriteLine("2) Создать призрака");
        Console.WriteLine("3) Нанести урон выбранному монстру");
        Console.WriteLine("4) Нанести урон случайному монстру");
        Console.WriteLine("5) Уничтожить выбранного монстра");
        Console.WriteLine("6) Вывести данные о всех текущих монстрах");
        Console.WriteLine("Введите \"q\" для выхода");
        Console.Write("Ваш выбор: ");
    }

    private void CreateEnemy<T>() where T : Enemy, new()
    {
        T enemy = new T();
        _enemies.Add(enemy);
        _currentEnemy  = enemy;
        Console.WriteLine($"Добавлен {enemy.Name}");
    }

    private void DamageSelected()
    {
        if (_currentEnemy  == null)
        {
            Console.WriteLine("Нет выбранного монстра");
            return;
        }

        Console.Write($"Введите урон для {_currentEnemy.Name}: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int damage) || damage <= 0)
        {
            Console.WriteLine("Неверный формат");
            return;
        }

        Console.WriteLine($"Наносим {damage} урона {_currentEnemy.Name}");
        _currentEnemy.TakeDamage(damage);

        if (!_currentEnemy .IsAlive)
        {
            RemoveEnemy(_currentEnemy );
        }
    }

    private void DamageRandom()
    {
        if (_enemies.Count == 0)
        {
            Console.WriteLine("Нет монстров для нанесения урона");
            return;
        }

        int randomIndex = _random.Next(_enemies.Count);
        var randomEnemy = _enemies[randomIndex];

        //TODO избавиться от магических чисел
        int damage = _random.Next(1, 10);

        Console.WriteLine($"Наносим {damage} урона {randomEnemy.Name}");
        randomEnemy.TakeDamage(damage);

        if (!randomEnemy.IsAlive)
        {
            RemoveEnemyAt(randomIndex);
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_currentEnemy  == enemy)
        {
            _currentEnemy  = _enemies.FirstOrDefault();
        }
    }

    private void RemoveEnemyAt(int index) => RemoveEnemy(_enemies[index]);

    private void DestroyCurrentEnemy()
    {
        if (_currentEnemy  == null)
        {
            Console.WriteLine("Нет выбранного монстра");
            return;
        }

        RemoveEnemy(_currentEnemy);
    }

    private void PrintAllEnemies()
    {
        if (_enemies.Count == 0)
        {
            Console.WriteLine("Нет монстров");
            return;
        }

        Console.WriteLine($"Всего монстров: {_enemies.Count}");

        foreach (var enemy in _enemies)
        {
            string selected = enemy == _currentEnemy ? "---> " : "";
            Console.WriteLine($"{selected}{enemy}");
        }
    }
}