// See https://aka.ms/new-console-template for more information
byte[,] array = new byte[10, 10];
List<Func<byte[,], int, int, int, bool, bool>> test = new List<Func<byte[,], int, int, int, bool, bool>>();

test.Add((ar, x, y, l, e) =>
{
    if (x > ar.GetUpperBound(0))
        return false;
    if (ar[x, y] != 0)
        return false;
    if (e)
    {
        Stumb(ar, x-1, y);

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
    return test[0](ar, ++x, y, --l, e);
});

test.Add((ar, x, y, l, e) =>
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
    return test[1](ar, --x, y, --l, e);
});

test.Add((ar, x, y, l, e) =>
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
    return test[2](ar, x, --y, --l, e);
});

test.Add((ar, x, y, l, e) =>
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
    return test[3](ar, x, ++y, --l, e);
});

Random random = new Random();

Fill(array, 4);

Fill(array, 3);
Fill(array, 3);

Fill(array, 2);
Fill(array, 2);
Fill(array, 2);

Fill(array, 1);
Fill(array, 1);
Fill(array, 1);
Fill(array, 1);

View(array);

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


void Stumb(byte[,] ar, int x, int y)
{
    if (x > ar.GetUpperBound(0) || x < 0 || y < 0 || y > ar.GetUpperBound(1))
        return;
    if (ar[x,y] != 1)
        ar[x, y] = 2;
}

void Fill(byte[,] array, int length)
{
    int x;
    int y;
    Func<byte[,], int, int, int, bool, bool> action;
    do
    {
        x = random.Next(0, 10);
        y = random.Next(0, 10);
        action = test[random.Next(0, 4)];
    }
    while (!action(array, x, y, length, false));
    action(array, x, y, length, true);
}