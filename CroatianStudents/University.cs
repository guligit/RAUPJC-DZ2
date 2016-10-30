using System;
using System.Collections.Specialized;
using System.Threading;

namespace CroatianStudents
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
        private int Count;

        public University(string name)
        {
            Name = name;
        }

    }
}