
// игровое поле
byte[,] field = new byte[10, 10];
// набор рисователей, они проверяют возможность рисования и рисуют корабли
List<Func<byte[,], int, int, int, bool, bool>> generators = new List<Func<byte[,], int, int, int, bool, bool>>();

Random random = new Random();

//рисователь вправо
generators.Add((ar, x, y, l, e) => 
{
    if (x > ar.GetUpperBound(0))
        return false;
    if (ar[x, y] != 0)
        return false;
    if (e)
    {
        Stumb(ar, x - 1, y);

        ar[x, y] = 1;

        Stumb(ar, x - 1, y + 1);
        Stumb(ar, x - 1, y - 1);

        Stumb(ar, x, y + 1);
        Stumb(ar, x, y - 1);

        Stumb(ar, x + 1, y + 1);
        Stumb(ar, x + 1, y - 1);
    }
    if (l == 1)
    {
        Stumb(ar, x + 1, y);
        return true;
    }
    return generators[0](ar, ++x, y, --l, e);
});

//рисователь влево
generators.Add((ar, x, y, l, e) =>
{
    if (x < 0)
        return false;
    if (ar[x, y] != 0)
        return false;
    if (e)
    {
        Stumb(ar, x + 1, y);

        ar[x, y] = 1;

        Stumb(ar, x - 1, y + 1);
        Stumb(ar, x - 1, y - 1);

        Stumb(ar, x, y + 1);
        Stumb(ar, x, y - 1);

        Stumb(ar, x + 1, y + 1);
        Stumb(ar, x + 1, y - 1);

    }
    if (l == 1)
    {
        Stumb(ar, x - 1, y);
        return true;
    }
    return generators[1](ar, --x, y, --l, e);
});

//рисователь вверх
generators.Add((ar, x, y, l, e) =>
{
    if (y < 0)
        return false;
    if (ar[x, y] != 0)
        return false;
    if (e)
    {
        Stumb(ar, x, y + 1);
        ar[x, y] = 1;
        Stumb(ar, x - 1, y + 1);
        Stumb(ar, x + 1, y + 1);

        Stumb(ar, x + 1, y);
        Stumb(ar, x - 1, y);

        Stumb(ar, x - 1, y - 1);
        Stumb(ar, x + 1, y - 1);

    }
    if (l == 1)
    {
        Stumb(ar, x, y - 1);
        return true;
    }
    return generators[2](ar, x, --y, --l, e);
});

//рисователь вниз
generators.Add((ar, x, y, l, e) =>
{
    if (y > ar.GetUpperBound(1))
        return false;
    if (ar[x, y] != 0)
        return false;
    if (e)
    {
        Stumb(ar, x, y - 1);

        ar[x, y] = 1;

        Stumb(ar, x - 1, y + 1);
        Stumb(ar, x + 1, y + 1);

        Stumb(ar, x + 1, y);
        Stumb(ar, x - 1, y);

        Stumb(ar, x - 1, y - 1);
        Stumb(ar, x + 1, y - 1);
    }
    if (l == 1)
    {
        Stumb(ar, x, y + 1);
        return true;
    }
    return generators[3](ar, x, ++y, --l, e);
});

// вывод массива
void View(byte[,] array)
{
    Console.Clear();
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            switch (array[i, j])
            {
                case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                case 2: Console.ForegroundColor = ConsoleColor.White; break;
                case 0: Console.ForegroundColor = ConsoleColor.Black; break;
            }
            Console.Write(array[i, j]);
        }
        Console.WriteLine();
    }
    Console.ForegroundColor = ConsoleColor.White;
}

// рисователь заглушки, куда нельзя ставить корабль, ставится вокруг корабля
void Stumb(byte[,] ar, int x, int y)
{
    if (x > ar.GetUpperBound(0) || x < 0 || y < 0 || y > ar.GetUpperBound(1))
        return;
    if (ar[x, y] != 1)
        ar[x, y] = 2;
}

// рисование корабля заданной длины
void Fill(byte[,] array, int length)
{
    int x;
    int y;
    Func<byte[,], int, int, int, bool, bool> DrawCheck;
    do
    {
        x = random.Next(0, 10);
        y = random.Next(0, 10);
        DrawCheck = generators[random.Next(0, 4)];
    }
    while (!DrawCheck(array, x, y, length, false));
    DrawCheck(array, x, y, length, true);
}

// 4 палубы
Fill(field, 4);

// 3 палубы
Fill(field, 3);
Fill(field, 3);

// 2 палубы
Fill(field, 2);
Fill(field, 2);
Fill(field, 2);

// 1 палуба
Fill(field, 1);
Fill(field, 1);
Fill(field, 1);
Fill(field, 1);

// результат
View(field);