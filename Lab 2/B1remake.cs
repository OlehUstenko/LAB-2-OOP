using System;

namespace Lab2_OOP
{
    class MyFrac
    {
        long nom, denom;

        public long Nom
        {
            get { return nom; }
        }

        public long Denom
        {
            get { return denom; }
        }
        
        public MyFrac(long nom, long denom)
        {
            if (denom == 0) throw new DivideByZeroException("Знаменник не може дорівнювати нулю!");

            if (denom < 0)
            {
                nom *= -1;
                denom *= -1;
            }

            long NSD = EvklidAlgorithm(nom, denom);
            this.nom = nom / NSD;
            this.denom = denom / NSD;
            
        }

        public override string ToString()
        {
            return $"{nom}/{denom}";
        }
        
        public static MyFrac operator +(MyFrac f1, MyFrac f2)
        {
            long newNom = (f1.nom * f2.denom) + (f2.nom * f1.denom);
            long newDenom = f1.denom * f2.denom;
            
            return new MyFrac(newNom, newDenom);
        }

        public static MyFrac operator -(MyFrac f1, MyFrac f2)
        {
            long newNom = (f1.nom * f2.denom) - (f2.nom * f1.denom);
            long newDenom = f1.denom * f2.denom;
            return new MyFrac(newNom, newDenom);
        }

        public static MyFrac operator *(MyFrac f1, MyFrac f2)
        {
            long newNom = f1.nom * f2.nom;
            long newDenom = f1.denom * f2.denom;
            return new MyFrac(newNom, newDenom);
        }

        public static MyFrac operator /(MyFrac f1, MyFrac f2)
        {
            long newNom = f1.nom * f2.denom;
            long newDenom = f1.denom * f2.nom;
            return new MyFrac(newNom, newDenom);
        }
        public double GetDoubleValue()
        {
            return (double)nom / denom;
        }

        public string ToStringWithIntegerPart()
        {
            long absNom = Math.Abs(nom); // Беремо модуль для зручних розрахунків
            long intPart = absNom / denom;
            long remainder = absNom % denom;

            // Визначаємо знак
            string sign = (nom < 0) ? "-" : "";

            if (intPart == 0)
            {
                // Якщо цілої частини немає, просто повертаємо дріб (ToString сам розбереться)
                return ToString();
            }

            if (remainder == 0)
            {
                // Якщо дріб скоротився до цілого числа (наприклад 14/7 -> 2)
                return $"{sign}{intPart}";
            }

            // Формуємо рядок з виділеною цілою частиною і залишком
            return $"{sign}({intPart}+{remainder}/{denom})";
        }



        public static long EvklidAlgorithm(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0 && b == 0) return 1;

            while (b != 0)
            {
                long temp = a % b;
                (a, b) = (b, temp);
            }
            return a;
        }
        public static MyFrac CalcSum1(int n)
        {
            MyFrac sum = new MyFrac(0, 1); // Початкова сума 0/1

            for (int i = 1; i <= n; i++)
            {
                // Доданок: 1 / (i * (i + 1))
                MyFrac term = new MyFrac(1, (long)i * (i + 1));
                sum = sum + term; // Використовуємо перевантажений оператор +
            }

            return sum;
        }

        public static MyFrac CalcSum2(int n)
        {
            MyFrac prod = new MyFrac(1, 1); // Початковий добуток 1/1
            MyFrac one = new MyFrac(1, 1);  // Дріб 1/1 для віднімання

            for (int i = 2; i <= n; i++)
            {
                // Множник: (1 - 1 / i^2)
                MyFrac term = one - new MyFrac(1, (long)i * i);
                prod = prod * term; // Використовуємо перевантажені оператори - та *
            }

            return prod;
        }



    }
}