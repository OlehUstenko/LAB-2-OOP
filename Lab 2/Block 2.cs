using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Channels;


namespace Lab_4_PALM
{
    public class Block2 // варіант 14
    {
        public static void Logic()
        {
            Console.WriteLine("ЗАПУСК БЛОКУ 2 (Варіант 14)");

            Student[] myStudents = ReadData("input.txt");

            Variant14(myStudents);

            Console.WriteLine("Додаткове завдання 1:");
            Dodatkove1(myStudents);

            Console.WriteLine("Додаткове завдання 2:");
            Dodatkove2(myStudents);

        }
        public struct Student
        {
            public string Surname;
            public string Name;
            public string Patronymic;
            public char Gender;
            public DateTime BirthDate;
            public string MathGrade;
            public string PhysicsGrade;
            public string CsGrade;
            public string Scholarship;
        }
        public static Student[] ReadData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Student[] students = new Student[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ', '\t', StringSplitOptions.RemoveEmptyEntries);
                students[i].Surname = parts[0];
                students[i].Name = parts[1];
                students[i].Patronymic = parts[2];
                students[i].Gender = char.Parse(parts[3]);
                students[i].BirthDate = DateTime.Parse(parts[4]);

                //if (parts[5] == "-") parts[5] = "2";
                students[i].MathGrade = parts[5];

                //if (parts[6] == "-") parts[6] = "2";
                students[i].PhysicsGrade = parts[6];

                //if (parts[7] == "-") parts[7] = "2";
                students[i].CsGrade = parts[7];

                students[i].Scholarship = parts[8];
            }
            return students;
        }
        public static void Variant14(Student[] students)
        {
            Console.WriteLine(" прізвища студентів, що мають тільки позитивні (від 3 до 5) оцінки і не одержують стипендію:");
            foreach (Student s in students)
            {

                bool math = (s.MathGrade == "3" || s.MathGrade == "4" || s.MathGrade == "5");
                bool physics = (s.PhysicsGrade == "3" || s.PhysicsGrade == "4" || s.PhysicsGrade == "5");
                bool cs = (s.CsGrade == "3" || s.CsGrade == "4" || s.CsGrade == "5");
                bool schoolarship = s.Scholarship == "0";

                if (math && physics && cs && schoolarship)
                {
                    Console.WriteLine(s.Surname);
                }

            }
        }
        public static void Dodatkove1(Student[] students)
        {
            Console.WriteLine("ПІБ студенток які мають середній бал строго більший, чим середній бал серед студентів чоловічої статі.");

            if (students == null)
            {
                Console.WriteLine("Пустий список");
                return;
            }

            int male = 0;
            int female = 0;
            double averageScore = 0;

            foreach (Student s in students)
            {
                if (s.Gender == 'M' || s.Gender == 'М' || s.Gender == 'Ч')
                {
                    male += 1;
                    averageScore += AverageGrade(s.MathGrade, s.PhysicsGrade, s.CsGrade);
                }
                if (s.Gender == 'F' || s.Gender == 'Ж')
                {
                    female = +1;
                }
            }
            if (male == 0)
            {
                Console.WriteLine("Чоловіків немає");
                return;
            }
            if (female == 0)
            {
                Console.WriteLine("Жінок немає");
            }

            averageScore = averageScore / male;
            bool exist = false;
            foreach (Student s in students)
            {
                if ((s.Gender == 'F' || s.Gender == 'Ж') && AverageGrade(s.MathGrade, s.PhysicsGrade, s.CsGrade) > averageScore)
                {
                    exist = true;
                    Console.WriteLine($"\tУ {s.Name} {s.Surname} {s.Patronymic} середній бал вище срередньому балу всіх чоловіків");
                }
            }
            if (!exist) Console.WriteLine("\tНе існує");
        }
        public static double AverageGrade(string Math, string Phisics, string CS)
        {
            if (Math == "-") Math = "2";
            if (Phisics == "-") Phisics = "2";
            if (CS == "-") CS = "2";
            int M = int.Parse(Math);
            int P = int.Parse(Phisics);
            int C = int.Parse(CS);

            return (M + P + C) / 3.0;

        }
        public static void Dodatkove2(Student[] students)
        {
            Console.WriteLine("День народження в один день та один рік:");
            bool[] visitedFullDate = new bool[students.Length];
            bool foundAnyFullDate = false;
            for (int i = 0; i < students.Length; i++)
            {
                if (visitedFullDate[i]) continue;
                bool hasMatch = false;

                for (int j = i + 1; j < students.Length; j++)
                {
                    if (!visitedFullDate[j] && students[i].BirthDate == students[j].BirthDate)
                    {
                        if (!hasMatch)
                        {
                            Console.WriteLine($"\nГрупа (Дата: {students[i].BirthDate:dd.MM.yyyy}):");
                            Console.WriteLine($" - {students[i].Surname} {students[i].Name} {students[i].Patronymic}");
                            hasMatch = true;
                            foundAnyFullDate = true;
                        }
                        Console.WriteLine($" - {students[j].Surname} {students[j].Name} {students[j].Patronymic}");
                        visitedFullDate[j] = true;
                    }
                }
            }
            if (!foundAnyFullDate)
            {
                Console.WriteLine("Збігів немає");
            }
            Console.WriteLine("День народження в один день");
            bool[] visitedDayMonth = new bool[students.Length];
            bool foundAnyDayMonth = false;
            for (int i = 0; i < students.Length; i++)
            {
                if (visitedDayMonth[i]) continue;

                bool match = false;
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (!visitedDayMonth[j]
                        &&
                        students[i].BirthDate.Day == students[j].BirthDate.Day
                        &&
                        students[i].BirthDate.Month == students[j].BirthDate.Month)
                    {
                        if (!match)
                        {
                            Console.WriteLine($"\nГрупа (Святкують: {students[i].BirthDate:dd MMMM}):");
                            Console.WriteLine($" - {students[i].Surname} {students[i].Name} (Рік: {students[i].BirthDate.Year})");
                            match = true;
                            foundAnyDayMonth = true;
                        }
                        Console.WriteLine($" - {students[j].Surname} {students[j].Name} (Рік: {students[j].BirthDate.Year})");
                        visitedDayMonth[j] = true;
                    }
                }
            }
            if (!foundAnyDayMonth)
            {
                Console.WriteLine("Збігів немає");
            }
        }
    }
}
