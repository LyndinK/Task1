using System;
using System.Collections.Generic;

namespace Matrix_shortest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] field = { {".", ".", "#", ".", ".", ".", ".", ".", ".", ".", },
                                {".", ".", "#", ".", ".", ".", ".", ".", ".", ".", },
                                {".", ".", "#", ".", "#", ".", ".", ".", ".", ".", },
                                {".", ".", "#", ".", "#", "X", ".", ".", ".", ".", },
                                {".", ".", "#", ".", "#", ".", ".", ".", ".", ".", },
                                {".", ".", "#", ".", "#", "#", "#", "#", "#", "#", },
                                {".", ".", "#", ".", ".", ".", ".", ".", ".", ".", },
                                {".", ".", "#", "#", "#", ".", "#", "#", "#", ".", },
                                {".", ".", ".", "0", ".", ".", ".", ".", ".", ".", },
                                {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", },};
            int[,] paths = new int[10, 10];

            List<int> findvalue(string to_find)
            {
                List<int> res = new List<int>();
                for (int a=0; a < field.GetLength(0); a++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (String.Equals(field[a, j], to_find))
                        {
                            res.Add(a);
                            res.Add(j);
                        }
                    }
                }
                return res;
            }
            var start = findvalue("0");
            paths[start[0], start[1]] = 1;
            int value = 1;
            var end = findvalue("X");
            void try_to_fill (int x, int y,int dx, int dy, int val)
            {
                if (0<=x+dx && 9>=x+dx && 0<=y+dy && 9>=y+dy && paths[x + dx, y + dy]== 0 && field[x + dx, y + dy]!="#")
                {
                    paths[x + dx, y + dy] = val;
                }
            }
            void try_to_move(int dx, int dy)
            {
                if (0 <= end[0] + dx && 9 >= end[0] + dx && 0 <= end[1] + dy && 9 >= end[1] + dy && paths[end[0] + dx, end[1] + dy] == value-1)
                {
                    end[0] += dx;
                    end[1] += dy;
                    field[end[0], end[1]] = "*";
                    value = value - 1;
                }
            }
            var now = DateTime.Now.AddSeconds(15);
            while (paths[end[0], end[1]] == 0)
            {
                if (DateTime.Now > now)
                {
                    Console.WriteLine("the path doesn't seem to exist");
                    break;
                }
                for (int a = 0; a < paths.GetLength(0); a++)
                {
                    for (int j = 0; j < paths.GetLength(1); j++)
                    {
                        if (paths[a, j] == value)
                        {
                            try_to_fill(a, j, 0, 1, value + 1);
                            try_to_fill(a, j, 1, 0, value + 1);
                            try_to_fill(a, j, -1, 0, value + 1);
                            try_to_fill(a, j, 0, -1, value + 1);
                        }
                    }
                }
                value++;
            }
            value = paths[end[0], end[1]];
            while (value > 2)
            {
                try_to_move(0, 1);
                try_to_move(1, 0);
                try_to_move(-1, 0);
                try_to_move(0, -1);
            }

            for (int a = 0; a < paths.GetLength(0); a++)
            {
                Console.Write("\n");
                for (int j = 0; j < paths.GetLength(1); j++)
                {
                    Console.Write(paths[a, j] + " ");
                }
            }
            Console.Write("\n");
            Console.Write("\n");

            for (int a = 0; a < field.GetLength(0); a++)
            {
                Console.Write("\n");
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[a, j] + " ");
                }
            }
            Console.ReadLine();
        }
    }
}
