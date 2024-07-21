
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace MiniProjectClassroom;

internal class Program
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
        Console.WriteLine("6. Proqrami dayandir");

        List<Classroom> classrooms = new List<Classroom>();
        List<Student> students = new List<Student>();
        bool exit = false;
        while (true)
        {

          head:
            Console.WriteLine("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":




                    //string json = JsonConvert.SerializeObject(classroom);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "classroom.json");

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

                    if (classroomName.IsValidClassroom())
                    {
                        Console.WriteLine(classroomName);
                    }
                    else
                    {

                        Console.WriteLine("Please, enter true name");
                        goto restart;
                    }
                repeat:
                    try
                    {
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
                            classroom = new(ClassroomType.Backend, classroomName);
                        }
                        else
                        {
                            Console.WriteLine("Please choose correct variant");
                            goto repeat;
                        }
                        classrooms.Add(classroom);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    
                 
                    var json = JsonConvert.SerializeObject(classrooms);
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(json);
                        Console.WriteLine("Classroom succesfully added");
                    }




                    break;


                case "2":
                    //   string path1 = "C:\\Users\\namjoon\\source\\repos\\MiniAppConsoleProjectCode\\classrooms.json";
                    string path1 = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "classroom.json");
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

                    Console.WriteLine();
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
                    try
                    {
                        Console.WriteLine("Choose class id: ");

                        foreach (var c in classrooms)
                        {
                            Console.WriteLine($"{c.Id} ,{c.Name} ,{c.Type.ToString()}");
                        }

                        int classId = int.Parse(Console.ReadLine());

                        var existClass = classrooms.FirstOrDefault(x => x.Id == classId);//find metoddudur

                        if (existClass is null)
                        {
                            Console.WriteLine("Please enter valid classroom");
                            goto restartClass;
                        }

                        existClass.StudentAdd(student);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    //BAX CLASSROOM FOREACH

                    //    Console.WriteLine("Current classrooms: ");
                    //    foreach (var room in classrooms)
                    //    {
                    //        Console.WriteLine(room);
                    //    }
                    //selectedClassroom:
                    //    Console.WriteLine("Select classroom: ");
                    //    int selectedClassroom = int.Parse(Console.ReadLine());
                    //    if (selectedClassroom < 0)
                    //    {
                    //        Console.WriteLine("False choice...Please, try again...");
                    //        goto selectedClassroom;
                    //    }

                    // Classroom classroom1 = new(,);
                    //classroom1.StudentAdd(student);

                    var json1 = JsonConvert.SerializeObject(classrooms);
                    using (StreamWriter sw = new StreamWriter(path1))
                    {
                        sw.WriteLine(json1);
                        Console.WriteLine("Student succesfully added to classroom");
                    }

                    break;

                case "3":

                    //butun telebelri ekrana cixart
                    string path2 = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "classroom.json");
                    string result2;
                    using (StreamReader sr = new StreamReader(path2))
                    {
                        result2 = sr.ReadToEnd();
                    }
                    classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result2);
                    if (classrooms is null)
                    {
                        classrooms = new();
                    }

                    Console.WriteLine("All students: ");

                    foreach (var c in classrooms)
                    {
                        Console.WriteLine($"{c.Name}");
                        if (c.Students.Count > 0)
                        {
                            foreach (var s in c.Students)
                            {
                                Console.WriteLine($"Name- {s.Name}, Surname - {s.Surname}, Id - {s.Id}");
                            }
                        }

                        else
                        {
                            Console.WriteLine("No student in this class");
                        }

                    }







                    var existStudent = classrooms.FirstOrDefault(x => x.Students == students);

                    var json2 = JsonConvert.SerializeObject(classrooms);
                    using (StreamWriter sw = new StreamWriter(path2))
                    {
                        sw.WriteLine(json2);

                    }



                    break;
                case "4"://secilmis sinifdeki telebler YENIDEN BAX

                    string path3 = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "classroom.json");
                    string result3;
                    using (StreamReader sr = new StreamReader(path3))
                    {
                        result3 = sr.ReadToEnd();
                    }
                    classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result3);
                    if (classrooms is null)
                    {
                        classrooms = new();
                    }
                    try
                    {
                        Console.WriteLine("All classes: ");
                        foreach (var c in classrooms)
                        {
                            Console.WriteLine($"{c.Id},{c.Name}");
                        }
                    again:

                        Console.WriteLine("Please, select one class");
                        int chosenClassId = int.Parse(Console.ReadLine());

                        bool validClass = false;
                        foreach (var c in classrooms)
                        {
                            if (chosenClassId == c.Id)
                            {
                                validClass = true;
                                foreach (var s in c.Students)
                                {
                                    Console.WriteLine($"{s.Name},{s.Surname},{s.Id}");

                                }
                                break;
                            }

                        }
                        if (!validClass)
                        {
                            Console.WriteLine("Bu idli class yoxudr");
                        }

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case "5"://telebe sil
                    string path4 = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "classroom.json");
                    string result4;
                    using (StreamReader sr = new StreamReader(path4))
                    {
                        result3 = sr.ReadToEnd();
                    }
                    classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result3);
                    if (classrooms is null)
                    {
                        classrooms = new();
                    }

                    try
                    {
                        Console.WriteLine("Students with their ID");
                        foreach (var c in classrooms)
                        {
                            foreach (var s in c.Students)
                            {
                                Console.WriteLine($"Name - {s.Name},Surname - {s.Surname}, Id - {s.Id}");
                            }
                        }

                        Console.WriteLine("Select the id: ");
                        int selectedId = int.Parse(Console.ReadLine());
                        bool studentFound = false;
                        foreach (var c in classrooms)
                        {


                            var studentToDelete = c.Students.FirstOrDefault(s => s.Id == selectedId);
                            if (studentToDelete != null)
                            {
                                c.Delete(selectedId);
                                studentFound = true;
                                break;
                            }
                        }
                        if (!studentFound)
                        {
                            throw new StudentNotFoundException("Student not found");
                        }

                    }
                    
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                   
                   
                    var json4 = JsonConvert.SerializeObject(classrooms);

                    using (StreamWriter sw = new StreamWriter(path4))
                    {
                        sw.WriteLine(json4);
                        Console.WriteLine("Changes saved succesfully...");
                    }


                    break;
                case "6":
                    exit = true;
                    return;
                default:
                    Console.WriteLine("Please enter valid choice... Try again...");
                    goto head;
                    
            }





        }


    }




}





