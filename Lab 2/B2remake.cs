using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab_2_OOP
{
    internal class B2remake
    {
        public struct Student
        {
            public string surName;
            public string firstName;
            public string patronymic;
            public char sex;
            public string dateOfBirth;
            public char mathMark;
            public char physicsMark;
            public char informaticsMark;
            public int scholarship;
        }
        public class StudentService
        {
            public List<Student> ReadStudentsFromFile(string filePath)
            {
                var students = new List<Student>();

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    // Перевіряємо, чи рядок містить всі необхідні 9 полів
                    if (parts.Length >= 9)
                    {
                        Student student = new Student
                        {
                            surName = parts[0],
                            firstName = parts[1],
                            patronymic = parts[2],
                            sex = char.Parse(parts[3]),
                            dateOfBirth = parts[4],
                            mathMark = char.Parse(parts[5]),
                            physicsMark = char.Parse(parts[6]),
                            informaticsMark = char.Parse(parts[7]),
                            scholarship = int.Parse(parts[8])
                        };
                        students.Add(student);
                    }
                }
                return students;
            }

            private int GetMarkValue(char mark)
            {
                if (mark == '-')
                {
                    return 2; 
                }

                //'0' має код 48
                return mark - '0';
            }

            
            public void ProcessVariant14(List<Student> students)
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("Список студентів порожній.");
                    return;
                }

                int totalMarksSum = 0;
                int totalGradesCount = students.Count * 3; 

                // 1. Обчислення загального середнього балу всіх студентів
                foreach (var student in students)
                {
                    totalMarksSum += GetMarkValue(student.mathMark);
                    totalMarksSum += GetMarkValue(student.physicsMark);
                    totalMarksSum += GetMarkValue(student.informaticsMark);
                }

                double overallAverage = (double)totalMarksSum / totalGradesCount;
                Console.WriteLine($"Загальний середній бал: {overallAverage:F2}\n");
                Console.WriteLine("Студенти, чий середній бал вищий за загальний:");

                // 2. Порівняння та виведення
                foreach (var student in students)
                {
                    int studentSum = GetMarkValue(student.mathMark) +
                                     GetMarkValue(student.physicsMark) +
                                     GetMarkValue(student.informaticsMark);

                    double studentAverage = (double)studentSum / 3;

                    // Виводимо лише тих, чий бал СТРОГО БІЛЬШИЙ за загальний   
                    if (studentAverage > overallAverage)
                    {
                        Console.WriteLine($"{student.surName,-20} | Сер. бал: {studentAverage:F2}");
                    }
                }
            }
        }
    }
}
