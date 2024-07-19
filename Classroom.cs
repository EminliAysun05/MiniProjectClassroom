using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectClassroom
{
    public class Classroom
    {
        //        Id oz-oz luyunde artacaq+
        //Name+
        //Student listi olsun+
        //Sinifin Type olsun-Enum olacaq(Backend ve Student)+
        //Type=Backend olsa sinifin limiti 20+
        //Type=FrontEnd olsa sinifin limiti 15 olsun+
        //StudentAdd methoduvuz olsun.
        //FindId-methodunuz id qebul edecek ve hemin id uygun telebeni qaytaracaq+
        //Delete-methodunuz id qebul edib ve hemin id uygun telebeni arrayden silecek+

        private static int _id;
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public ClassroomType Type { get; set; }
        public int Limit { get; set; }
        public Classroom(ClassroomType type, string name)
        {
            Id = _id++;
            Type = type;
            Limit = (int)type;
            Students = new List<Student>();
            Name = name;
        }

        public bool StudentAdd(Student student) //sinifin limiti>student.count
        {
            if (Students.Count < Limit)
            {
                Students.Add(student);
                student.ClassId = Id;
                return true;
            }
            return false;

        }

        public Student FindId(int id)
        {
            //var student = Students.Find(student => student.Id == id);


            //if (student is null)
            //    throw new Exception("Student not found");


            //return student;

            var student = Students.Find(student => student.Id == id);
            if (student is null)
            {
                throw new Exception("Student not found");
            }

            return student;

            //foreach (Student student in students)
            //{
            //    if (student.Id == id)
            //    {
            //        return student;
            //    }

            //}
            //return null;

        }

        public void Delete(int id)
        {
            Student student = FindId(id);

            Students.Remove(student);
            //foreach (Student student in Students)
            //{
            //    if (student.Id == id)
            //    {
            //        Students.Remove(student);
            //        return true;
            //    }
            //}
            //return false;
        }
    }
}

