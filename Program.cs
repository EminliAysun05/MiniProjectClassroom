
using MiniProjectClassroom;
using Newtonsoft.Json;
using System.Data;
using System.Linq.Expressions;
using System.Text.Json.Serialization;




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
                    //Helper.Print("Please, enter first letter uppercase or lenght of fullname is bigger than " +
                    // "2 and lower than 22", ConsoleColor.Red);
                    Helper.Print("Please choose correct variant", ConsoleColor.Red);
                    
                    goto repeat;
                }
                classrooms.Add(classroom);
            }
            catch (Exception ex)
            {
                Helper.Print(ex.Message, ConsoleColor.Red);
               
                break;
            }


            var json = JsonConvert.SerializeObject(classrooms);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
                Helper.Print("Classroom succesfully added", ConsoleColor.Green);
                
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
            if(classrooms is null)
            {
                classrooms = new();
            }

            if (students is null)
            {
                classrooms = new();
            }
            try { 
            if(classrooms.Count == 0)
            {
                throw new ClassroomNotFoundException("No valid classroom");
            }

            



            here:

                Console.WriteLine();
            name:




                Console.WriteLine("Please, enter student name: ");
                string studentName = Console.ReadLine();

                Console.WriteLine("Please, enter student surname: ");
                string studentSurname = Console.ReadLine();

                foreach (var classroom in classrooms)
                {
                    foreach (var s in classroom.Students)
                    {
                        if (s.Name == studentName && s.Surname == studentSurname)
                        {
                            Helper.Print("Eyni ad ve soyadli student yaratmaq olmaz", ConsoleColor.Red);
                          
                            goto name;
                        }
                    }
                }
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
                    Helper.Print("Please, true name or surname...", ConsoleColor.Red);
                
                    goto here;
                }




                Student student = new Student(studentName, studentSurname);

            restartClass:
                Console.WriteLine("Choose class id: ");

                foreach (var c in classrooms)
                {
                    Console.WriteLine($"{c.Id} ,{c.Name} ,{c.Type.ToString()}");
                }

                int classId = int.Parse(Console.ReadLine());

                var existClass = classrooms.FirstOrDefault(x => x.Id == classId);//find metoddudur

                if (existClass is null)
                {
                    Helper.Print("Please enter valid classroom", ConsoleColor.Red);
                    
                    goto restartClass;
                }

                existClass.StudentAdd(student);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                break;
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
                Helper.Print("Student succesfully added to classroom", ConsoleColor.Green);
                
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
                    Helper.Print("No student in this class", ConsoleColor.Red);
                  
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
                    Helper.Print("Bu idli class yoxdur", ConsoleColor.Red);
                
                }

            }
            catch (Exception ex)
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

            catch (Exception ex)
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
            Helper.Print("Please enter valid choice... Try again...", ConsoleColor.Red);
            
            goto head;

    }





}





