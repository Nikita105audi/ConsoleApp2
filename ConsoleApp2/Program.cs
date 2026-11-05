using System;

namespace NumericalMethods_PR1
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Задача 2: Арифметика с погрешностями ---
            double a = 12.6, da = 0.05;
            double b = 3.4, db = 0.1;

            Console.WriteLine("--- Задача 2: a=12.6(0.05), b=3.4(0.1) ---");
            PrintResult("Сумма", a + b, da + db);
            PrintResult("Разность", a - b, da + db);
            PrintResult("Произведение", a * b, (a * b) * (da / a + db / b));
            PrintResult("Частное", a / b, (a / b) * (da / a + db / b));

            // --- Задача 4: Потеря значимости ---
            double x = 4.321, dx = 0.0005;
            double y = 4.319, dy = 0.0005;
            double d = x - y;
            Console.WriteLine("\n--- Задача 4: Разность близких чисел ---");
            Console.WriteLine($"double: {d:G17} (Точное: 0.002)");
            Console.WriteLine("Комментарий: Видны 'хвосты' шума. 13 цифр верны, но относительная погрешность 50%.");

            // --- Задача 3: Накопление 0.1 ---
            Console.WriteLine("\n--- Задача 3: Накопление ошибки 0.1 ---");
            for (int n = 10; n <= 1000000; n *= 100)
            {
                double sum = 0;
                for (int i = 0; i < n; i++) sum += 0.1;
                double exact = n * 0.1;
                Console.WriteLine($"{n} раз: Сумма={sum:G17}, Ошибка={sum - exact:E2}");
            }
            // Комментарий: Ошибка растет линейно с числом операций. Знак меняется хаотично.

            // --- Задача: Сравнение double и decimal ---
            Console.WriteLine("\n--- Сравнение 0.1 + 0.2 ---");
            double dd = 0.1 + 0.2;
            decimal dm = 0.1m + 0.2m;
            Console.WriteLine($"double == 0.3: {dd == 0.3} (через ==)");
            Console.WriteLine($"double (допуск 1e-9): {Math.Abs(dd - 0.3) < 1e-9}");
            Console.WriteLine($"decimal: {dm} (точен, так как хранит десятичную дробь)");

            // --- Задача: Квадратное уравнение ---
            Console.WriteLine("\n--- Квадратное уравнение x^2 - 50000x + 1 = 0 ---");
            double A = 1, B = -50000, C = 1;
            double D = Math.Sqrt(B * B - 4 * A * C);

            // Прямой способ (потеря значимости при вычитании)
            double x1_big = (-B + D) / (2 * A);
            double x2_bad = (-B - D) / (2 * A);

            // Способ через Виета (умножение/деление вместо вычитания)
            double x2_good = C / x1_big;

            Console.WriteLine($"x1 (большой): {x1_big}");
            Console.WriteLine($"x2 (прямой):  {x2_bad} (потеряно ~7 верных цифр)");
            Console.WriteLine($"x2 (Виет):    {x2_good} (верны все 15 цифр double)");
            Console.WriteLine("Комментарий: Вычитание близких чисел (100000 и ~99999.99998) схлопнуло разряды.");
        }

        static void PrintResult(string name, double val, double absErr)
        {
            double relErr = absErr / Math.Abs(val);
            // Округление погрешности вверх до 1-2 знаков
            double roundedErr = Math.Ceiling(absErr * 10) / 10;
            // Округление значения до разряда погрешности
            double multiplier = Math.Pow(10, Math.Floor(Math.Log10(roundedErr)));
            double roundedVal = Math.Round(val / multiplier) * multiplier;

            Console.WriteLine($"{name}: {roundedVal} ± {roundedErr} (δ ≈ {relErr:P2})");
        }
    }
}