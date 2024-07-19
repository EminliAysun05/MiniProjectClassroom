
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MiniProjectClassroom
{
    internal class Program
    {
       

        static void Main(string[] args)
        {
            static void Main(string[] args)
            {
                //                1.Classroom yarat
                //    2.Student yarat
                //    3.Butun Telebeleri ekrana cixart
                //    4.Secilmis sinifdeki telebeleri ekrana cixart
                //    5.Telebe sil(Verilmis id olan telbeni taparaq silecek) eger telebe tapilmasa StudentNotFoundException(Ozunuz yaradirsiz) qaytaracaq
                
                Console.WriteLine("1. Classroom yarat");
                Console.WriteLine("2. Student yarat");
                Console.WriteLine("3. Butun telebeleri ekrana cixart");
                Console.WriteLine("4. Secilmis sinifdeki telebeleri ekrana cixart ");
                Console.WriteLine("5. Telebe sil ");

                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();
                List<Classroom> classrooms = new List<Classroom>();
                List<Student> students = new List<Student>();
                switch (choice)
                {
                    case "1":




                        //string json = JsonConvert.SerializeObject(classroom);
                        string path = "C:\\Users\\namjoon\\source\\repos\\MiniAppConsoleProjectCode\\classrooms.json";
                        
                        //  string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..","Jsons","student.json");

                        string result;
                        using (StreamReader sr = new StreamReader(path))
                        {
                            result = sr.ReadToEnd();
                        }
                        
                        classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result);
                        // var json = JsonConvert.DeserializeObject<string>(result);
                        if (classrooms is null)
                        {
                            classrooms = new();
                        }

                    restart:

                        Console.WriteLine("Please, enter name of classroom: ");
                        string classroomName = Console.ReadLine();

                        if (classroomName.IsValidNameSurName())
                        {
                            Console.WriteLine(classroomName);
                        }
                        else
                        {

                            Console.WriteLine("Please, enter true name");
                            goto restart;
                        }
                    repeat:
                        Console.WriteLine("Please enter the type of classroom(0-Backend,1-Frontend)");
                        int selectedClass = int.Parse(Console.ReadLine());
                        //Bax

                        Classroom classroom = null!;

                        if (selectedClass == 0)
                        {
                            classroom = new(ClassroomType.Frontend, classroomName);
                        }
                        else if (selectedClass == 1)
                        {
                            classroom = new(ClassroomType.Backend,classroomName); 
                        }
                        else
                        {
                            Console.WriteLine("Please choose correct variant");
                            goto repeat;
                        }
                        classrooms.Add(classroom);


                        var json = JsonConvert.SerializeObject(classrooms);
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine(json);
                            Console.WriteLine("Classroom succesfully added");
                        }
                        break;


                    case "2":
                        string path1 = "C:\\Users\\namjoon\\source\\repos\\MiniAppConsoleProjectCode\\classrooms.json";
                        //  string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..","Jsons","student.json");

                        string result1;
                        using (StreamReader sr = new StreamReader(path1))
                        {
                            result1 = sr.ReadToEnd();
                        }
                        classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result1);
                        // var json = JsonConvert.DeserializeObject<string>(result);

                        if (students is null)
                        {
                            classrooms = new();
                        }
                    here:
                        Console.WriteLine("Please, enter student name: ");
                        string studentName = Console.ReadLine();

                        Console.WriteLine("Please, enter student surname: ");
                        string studentSurname = Console.ReadLine();
                        if (studentName.IsValidNameSurName())
                        {
                            Console.WriteLine(studentName);
                        }

                        else if (studentSurname.IsValidNameSurName())
                        {
                            Console.WriteLine(studentSurname);
                        }
                        else
                        {
                            Console.WriteLine("Please, true name or surname...");
                            goto here;
                        }




                        Student student = new Student(studentName, studentSurname);

                    restartClass:
                        Console.WriteLine("Choose class id");

                        foreach (var c in classrooms)
                        {
                            Console.WriteLine($"{c.Id} {c.Name} {c.Type.ToString()}");
                        }

                        int classId = int.Parse(Console.ReadLine());

                        var existClass  = classrooms.FirstOrDefault(x => x.Id == classId);

                        if (existClass is null)
                        {
                            Console.WriteLine("Please enter valid classroom");
                            goto restartClass;
                        }

                        existClass.StudentAdd(student);

                        //BAX CLASSROOM FOREACH

                        Console.WriteLine("Current classrooms: ");
                        foreach (var room in classrooms)
                        {
                            Console.WriteLine(room);
                        }
                    selectedClassroom:
                        Console.WriteLine("Select classroom: ");
                        int selectedClassroom = int.Parse(Console.ReadLine());
                        if (selectedClassroom < 0)
                        {
                            Console.WriteLine("False choice...Please, try again...");
                            goto selectedClassroom;
                        }

                        // Classroom classroom1 = new(,);
                        //classroom1.StudentAdd(student);

                        var json1 = JsonConvert.SerializeObject(classrooms);
                        using (StreamWriter sw = new StreamWriter(path1))
                        {
                            sw.WriteLine(json1);
                            Console.WriteLine("Student succesfully added to classroom");
                        }

                        break;
                }






            }
        }
    }
}

