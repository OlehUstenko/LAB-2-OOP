using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Lab_2_OOP.B2remake;

namespace Lab2_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool exitApp = false;

            while (!exitApp)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("       ГОЛОВНЕ МЕНЮ ЛАБОРАТОРНОЇ РОБОТИ №2");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Блок 1: Робота з дробами (Клас MyFrac)");
                Console.WriteLine("2. Блок 2: Аналіз успішності студентів (Варіант 14)");
                Console.WriteLine("0. Вихід з програми");
                Console.WriteLine("==================================================");
                Console.Write("Оберіть пункт меню (0-2): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunBlock1Menu();
                        break;
                    case "2":
                        RunBlock2();
                        break;
                    case "0":
                        exitApp = true;
                        Console.WriteLine("Дякуємо за використання програми! До побачення.");
                        break;
                    default:
                        Console.WriteLine("Помилка: Невірний пункт меню! Натисніть Enter для продовження...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void RunBlock1Menu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("         БЛОК 1: ТЕСТУВАННЯ КЛАСУ MyFrac");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("1. Створити дріб та вивести його значення");
                Console.WriteLine("2. Додавання двох дробів (+)");
                Console.WriteLine("3. Віднімання двох дробів (-)");
                Console.WriteLine("4. Множення двох дробів (*)");
                Console.WriteLine("5. Ділення двох дробів (/)");
                Console.WriteLine("6. Обчислити суму CalcSum1(n)");
                Console.WriteLine("7. Обчислити добуток CalcSum2(n)");
                Console.WriteLine("8. Тест виділення цілої частини (ToStringWithIntegerPart)");
                Console.WriteLine("9. Демонстрація незмінності об'єктів (Immutability)");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Оберіть дію (0-9): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        TestCreateFraction();
                        break;
                    case "2":
                        TestAdd();
                        break;
                    case "3":
                        TestSubtract();
                        break;
                    case "4":
                        TestMultiply();
                        break;
                    case "5":
                        TestDivide();
                        break;
                    case "6":
                        TestCalcSum1();
                        break;
                    case "7":
                        TestCalcSum2();
                        break;
                    case "8":
                        TestIntegerPart();
                        break;
                    case "9":
                        TestImmutability();
                        break;
                    case "0":
                        backToMain = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір! Введіть число від 0 до 9.");
                        break;
                }
            }
        }

        static void TestCreateFraction()
        {
            Console.WriteLine("[ 1. Створення дробу ]");
            MyFrac f = ReadFractionFromConsole("Введіть дріб:");
            Console.WriteLine($"Рядковий вигляд (ToString): {f}");
            Console.WriteLine($"Дійсне значення (GetDoubleValue): {f.GetDoubleValue():F4}");
            Console.WriteLine($"З виділеною цілою частиною: {f.ToStringWithIntegerPart()}");
        }

        static void TestAdd()
        {
            Console.WriteLine("[ 2. Додавання дробів ]");
            MyFrac f1 = ReadFractionFromConsole("Перший дріб:");
            MyFrac f2 = ReadFractionFromConsole("Другий дріб:");
            MyFrac result = f1 + f2;
            Console.WriteLine($"Результат: {f1} + {f2} = {result}");
        }

        static void TestSubtract()
        {
            Console.WriteLine("[ 3. Віднімання дробів ]");
            MyFrac f1 = ReadFractionFromConsole("Перший дріб:");
            MyFrac f2 = ReadFractionFromConsole("Другий дріб:");
            MyFrac result = f1 - f2;
            Console.WriteLine($"Результат: {f1} - {f2} = {result}");
        }

        static void TestMultiply()
        {
            Console.WriteLine("[ 4. Множення дробів ]");
            MyFrac f1 = ReadFractionFromConsole("Перший дріб:");
            MyFrac f2 = ReadFractionFromConsole("Другий дріб:");
            MyFrac result = f1 * f2;
            Console.WriteLine($"Результат: {f1} * {f2} = {result}");
        }

        static void TestDivide()
        {
            Console.WriteLine("[ 5. Ділення дробів ]");
            MyFrac f1 = ReadFractionFromConsole("Перший дріб:");
            MyFrac f2 = ReadFractionFromConsole("Другий дріб:");
            try
            {
                MyFrac result = f1 / f2;
                Console.WriteLine($"Результат: {f1} / {f2} = {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        static void TestCalcSum1()
        {
            Console.WriteLine("[ 6. Обчислення CalcSum1(n) = 1/(1*2) + 1/(2*3) + ... + 1/(n*(n+1)) ]");
            Console.Write("Введіть n (кількість елементів): ");
            if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
            {
                MyFrac sum = MyFrac.CalcSum1(n);
                Console.WriteLine($"Обчислено суму для n={n}: {sum}");
                Console.WriteLine($"Перевірка за формулою n/(n+1): {n}/{n + 1}");
            }
            else
            {
                Console.WriteLine("Некоректне значення n!");
            }
        }

        static void TestCalcSum2()
        {
            Console.WriteLine("[ 7. Обчислення CalcSum2(n) = (1-1/4)*(1-1/9)*...*(1-1/n^2) ]");
            Console.Write("Введіть n (n >= 2): ");
            if (int.TryParse(Console.ReadLine(), out int n) && n >= 2)
            {
                MyFrac prod = MyFrac.CalcSum2(n);
                Console.WriteLine($"Обчислено добуток для n={n}: {prod}");
                Console.WriteLine($"Перевірка за формулою (n+1)/(2n): {n + 1}/{2 * n}");
            }
            else
            {
                Console.WriteLine("Значення n повинно бути >= 2!");
            }
        }

        static void TestIntegerPart()
        {
            Console.WriteLine("[ 8. Форматування з цілою частиною ]");
            MyFrac f1 = new MyFrac(17, 7);
            MyFrac f2 = new MyFrac(-17, 7);
            MyFrac f3 = new MyFrac(3, 5);

            Console.WriteLine($"17/7  -> {f1.ToStringWithIntegerPart()}");
            Console.WriteLine($"-17/7 -> {f2.ToStringWithIntegerPart()}");
            Console.WriteLine($"3/5   -> {f3.ToStringWithIntegerPart()}");
        }

        static void TestImmutability()
        {
            Console.WriteLine("[ 9. Перевірка незмінності початкових дробів ]");
            MyFrac a = new MyFrac(1, 2);
            MyFrac b = new MyFrac(1, 3);

            Console.WriteLine($"Початковий дріб A: {a}");
            Console.WriteLine($"Початковий дріб B: {b}");

            MyFrac c = a + b;
            Console.WriteLine($"Результат додавання (C = A + B): {c}");
            Console.WriteLine($"Дріб A після додавання: {a} (не змінився)");
            Console.WriteLine($"Дріб B після додавання: {b} (не змінився)");
        }

        static MyFrac ReadFractionFromConsole(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                try
                {
                    Console.Write("  Чисельник: ");
                    long nom = long.Parse(Console.ReadLine());
                    Console.Write("  Знаменник: ");
                    long denom = long.Parse(Console.ReadLine());

                    return new MyFrac(nom, denom);
                }
                catch (FormatException)
                {
                    Console.WriteLine("  Помилка: введіть ціле число!");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"  Помилка: {ex.Message}");
                }
            }
        }


        static void RunBlock2()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("  БЛОК 2: ОБРОБКА ФАЙЛУ ЗІ СТУДЕНТАМИ (Варіант 14)");
            Console.WriteLine("--------------------------------------------------");

            StudentService service = new StudentService();
            string filePath = "input.txt";

            try
            {
                List<Student> students = service.ReadStudentsFromFile(filePath);
                Console.WriteLine($"Зчитано студентів з файлу: {students.Count}\n");

                service.ProcessVariant14(students);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Помилка: Файл '{filePath}' не знайдено!");
                Console.WriteLine("Переконайтеся, що файл переміщено в проект і встановлено 'Copy to Output Directory = Copy if newer'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка під час обробки: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть Enter, щоб повернутися в головне меню...");
            Console.ReadLine();
        }
    }
}