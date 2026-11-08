using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using MyFrac = (long nom, long denom);

namespace Lab_4_PALM
{
    public class Block1 //варіант 2
    {
        public static void Logic()
        {
            string choice;
            do
            {
                Console.Write("" +
                "\n\tШо ви хочітє?" +
                "\n1.Нормалізувати дріб" +
                "\n2.Виділити цілу частину з дробу" +
                "\n3.Дійсне значення дробу" +
                "\n4.Суму двох дробів" +
                "\n5.Різницю двох дробів" +
                "\n6.Добуток двох дробів" +
                "\n7.Частку двох дробів" +
                "\n8.Суму дробів 1/(n*(n+1))" +
                "\n9.Множення дробів (n+1)/(2n)" +
                "\n0.Нічеґо(вихід)" +
                "\n\tВаш вибір: ");
                choice = Console.ReadLine();
                MyFrac f1, f2;
                switch (choice)
                {

                    case ("1"):
                        f1 = Filling();
                        f1 = Normalize(f1);
                        Console.WriteLine("результат: " + MyFracToString(f1));
                        break;

                    case ("2"):
                        f1 = Filling();
                        Console.WriteLine("результат: " + ToStringWithIntPart(f1));
                        break;

                    case ("3"):
                        f1 = Filling();
                        double result = DoubleValue(f1);
                        Console.WriteLine("результат: " + result);

                        break;
                    case ("4"):
                        Console.WriteLine("Перший дріб");
                        f1 = Filling();
                        Console.WriteLine("Другий дріб");
                        f2 = Filling();
                        f1 = Plus(f1, f2);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("5"):
                        Console.WriteLine("Перший дріб");
                        f1 = Filling();
                        Console.WriteLine("Другий дріб");
                        f2 = Filling();
                        f1 = Minus(f1, f2);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("6"):
                        Console.WriteLine("Перший дріб");
                        f1 = Filling();
                        Console.WriteLine("Другий дріб");
                        f2 = Filling();
                        f1 = Multiply(f1, f2);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("7"):
                        Console.WriteLine("Перший дріб");
                        f1 = Filling();
                        Console.WriteLine("Другий дріб");
                        f2 = Filling();
                        f1 = Divide(f1, f2);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("8"):
                        Console.Write("Введіть кількість повторень: ");
                        int n = int.Parse(Console.ReadLine());
                        f1 = CalcExpr1(n);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("9"):
                        Console.Write("Введіть кількість повторень: ");
                        int n1 = int.Parse(Console.ReadLine());
                        f1 = CalcExpr2(n1);
                        Console.WriteLine("результат: " + MyFracToString(f1));

                        break;

                    case ("0"):
                        Console.WriteLine("Допобачення! Заходьте ще :)");
                        break;
                    default:
                        Console.WriteLine("Такого варіанту я ще не придумав!\n");
                        break;
                }
            } while (choice != "0");
        }
        public static MyFrac Filling()
        {
            MyFrac f = new MyFrac();
            Console.Write("Введіть чисельник: ");
            f.nom = long.Parse(Console.ReadLine());
            do
            {
                Console.Write("\nВведіть знаменник: ");
                f.denom = long.Parse(Console.ReadLine());
                if (f.denom == 0)
                {
                    Console.WriteLine("Ділення на нуль не можливе!");
                }
            } while (f.denom == 0);
            return f;
        }

        public static string MyFracToString(MyFrac f)
        {
            return $"{f.nom} / {f.denom}";
        } //виводить дріб
        public static long EvklidAlgorithm(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                long temp = a % b;
                (a, b) = (b, temp);
            }
            return a;
        }//НСД
        public static MyFrac Normalize(MyFrac f)
        {
            long NSD = EvklidAlgorithm(f.nom, f.denom);

            if (f.denom < 0)
            {
                f = (-f.nom, -f.denom);
            }

            return (f.nom / NSD, f.denom / NSD);

        }  //нормалізує дріб
        public static string ToStringWithIntPart(MyFrac f)
        {
            f = Normalize(f);
            if (f.nom % f.denom == 0) return $"{f.nom / f.denom}";

            long intPart = Math.Abs(f.nom / f.denom);
            long remainder = Math.Abs(f.nom % f.denom);

            string sign = (f.nom < 0) ? "-" : "";
            return $"{sign}({intPart} + {remainder} / {f.denom})";

        }// Виділяє цілу частину з дробу
        public static double DoubleValue(MyFrac f)
        {
            return (double)f.nom / f.denom;
        }
        public static MyFrac Plus(MyFrac f1, MyFrac f2)
        {
            if (f1.denom == f2.denom) return Normalize((f1.nom + f2.nom, f1.denom));
            long NSD = EvklidAlgorithm(f1.denom, f2.denom);
            long NSK = Math.Abs(f1.denom * f2.denom) / NSD;
            return Normalize((NSK / f1.denom * f1.nom + NSK / f2.denom * f2.nom, NSK));

        }
        public static MyFrac Minus(MyFrac f1, MyFrac f2)
        {
            if (f1.denom == f2.denom) return Normalize((f1.nom - f2.nom, f1.denom));
            long NSD = EvklidAlgorithm(f1.denom, f2.denom);
            long NSK = Math.Abs(f1.denom * f2.denom) / NSD;
            return Normalize((NSK / f1.denom * f1.nom - NSK / f2.denom * f2.nom, NSK));
        }
        public static MyFrac Multiply(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.nom, f1.denom * f2.denom));
        }
        public static MyFrac Divide(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom, f1.denom * f2.nom));
        }
        public static MyFrac CalcExpr1(int n)
        {
            MyFrac sum = (0, 1);

            for (int i = 1; i <= n; i++)
            {
                MyFrac temp = (1, (long)i * (i + 1));
                sum = Plus(sum, temp);

            }
            return sum;

        }
        public static MyFrac CalcExpr2(int n)
        {
            MyFrac result = (1, 1);

            for (int i = 2; i <= n; i++)
            {
                MyFrac temp = (Minus((1, 1), (1, i * i)));
                result = Multiply(temp, result);
            }
            return result;
        }
    }
}