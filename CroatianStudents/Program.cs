using System;
using System.Collections.Generic;
using System.Linq;

namespace CroatianStudents
{
    public class Program
    {
        public static void Main()
        {
           Example1(); 
           Example2();

            University[] universities = GetAllCroatianUniversities();
            Student[] allCroatianStudents = universities.SelectMany(i => i.Students).Distinct().ToArray();
            Student[] croatianStudentsOnMultipleUniversities =
                universities.SelectMany(i => i.Students)
                    .GroupBy(i => i)
                    .Where(i => i.Count() > 1)
                    .Select(i => i.Key)
                    .ToArray();
            Student[] studentsOnMaleOnlyUniversities =
                universities.Where(i => i.Students.All(x => x.Gender == Gender.Male))
                    .SelectMany(s => s.Students)
                    .Distinct()
                    .ToArray();

            Console.WriteLine();

            foreach (var item in universities)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine();

            foreach (var student in allCroatianStudents) 
            {
                Console.WriteLine(student.Name + " " + student.Jmbag);
            }

            Console.WriteLine();

            foreach (var student in croatianStudentsOnMultipleUniversities)
            {
                Console.WriteLine(student.Name + " " + student.Jmbag);
            }

            Console.WriteLine();

            foreach (var student in studentsOnMaleOnlyUniversities)
            {
                Console.WriteLine(student.Name + " " + student.Jmbag);
            }

            Console.WriteLine();


        }

        private static University[] GetAllCroatianUniversities()
        {
            University Zagreb = new University("Zagreb");
            University Split = new University("Split");
            University Zadar = new University("Zadar");
            University Rijeka = new University("Rijeka");

            Student Marko = new Student("Marko", "00123456", Gender.Male);
            Student Ivan = new Student("Ivan", "03423243", Gender.Male);
            Student Lucija = new Student("Lucija", "67294373", Gender.Female);
            Student Patricia = new Student("Patricija", "63638352", Gender.Female);
            Student Petar = new Student("Petar", "47465824", Gender.Male);

            Zagreb.Students = new[] {Marko, Ivan};
            Split.Students = new[] {Lucija, Marko};
            Zadar.Students = new[] {Patricia, Ivan};
            Rijeka.Students = new[] {Lucija, Petar};

            University[] universities = {Zagreb, Rijeka, Split, Zadar};

            return universities;

        }

        static void Example1()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567")
            };

            var ivan = new Student("Ivan", jmbag: "001234567");
            // false :(
            bool anyIvanExists = list.Any(i => i == ivan);
            Console.WriteLine(anyIvanExists);
        }

        static void Example2()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567"),
                new Student ("Ivan", jmbag :"001234567")
            };

            // 2 :(
            var distinctStudents = list.Distinct().Count();
            Console.WriteLine(distinctStudents);
        }

    }
}