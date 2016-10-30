using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace CroatianStudents
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public Student(string name, string jmbag, Gender gender)
        {
            Name = name;
            Jmbag = jmbag;
            Gender = gender;
        }

        public override bool Equals(object obj)
        {
            Student objx = obj as Student;
            if (ReferenceEquals(objx, null)) return false;
            return (Name.Equals(objx.Name) && Jmbag.Equals(objx.Jmbag));
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Jmbag.GetHashCode();
        }

        public static bool operator ==(Student first, Student second)
        {
            if (ReferenceEquals(first, null))
                return ReferenceEquals(second, null);

            return first.Equals(second);
        }

        public static bool operator !=(Student first, Student second)
        {
            return !(first == second);
        }
    }
}

