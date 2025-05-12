using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Configuration;
public abstract class Student
{
    static string FilePath = ConfigurationManager.AppSettings["FilePath"];
    static string path = Path.Combine(FilePath, "log.txt");
    //Student Details are stored in StudentDetails dictionary
    public static Dictionary<string, Student> StudentDetails = new Dictionary<string, Student>();
   //Proprites for Every student
    public string Student_Id { get; set; }
    public string Student_Name { get; set; }
    public string Student_Email { get; set; }
    public string Grade { get; set; }
    public List<double> Marks { get; set; } = new List<double>();


    public override string ToString()
    {
        return string.Join(",", Student_Id, Student_Name, Student_Email, Grade, Marks[0], Marks[1], Marks[2], Marks[3], Marks[4]);
    }
    //constructor for initializing the student values When a object is created
    public Student(string Student_Name, string Student_Email, List<double> Marks)
    {
        this.Student_Name = Student_Name;
        this.Student_Email = Student_Email;
        this.Marks = Marks;

    }
    // Calculate the  Average for the Student Marks
    public double CalculateAverage(List<double> Marks)
    {
        return Marks.Average();
    }
    // Add new Student to the performance tracker
    public void AddStudent(Student student)
    {
        Student.StudentDetails.Add(student.Student_Id, student);
    }
    // Display student details by Matching with name or Id
    public static List<Student> SearchStudentDetailsByIdOrName(string FieldToSearch)
    {
        if (Student.StudentDetails.ContainsKey(FieldToSearch))
        {
            return new List<Student> { Student.StudentDetails[FieldToSearch] };
        }
        return Student.StudentDetails.ToList().Where(x => x.Value.Student_Name.ToLower().Equals(FieldToSearch.ToLower())).Select(x => x.Value).ToList();
    }
    // Display All Student Details
    public static void DisplayAllStudentDetails()
    {
        Console.WriteLine(Student.StudentDetails.Count);
       if(Student.StudentDetails.Count > 0)
        {
            Console.WriteLine(new string('-', 150));
            Console.WriteLine($"{new string(' ', 10)}StudentId{new string(' ', 10)}StudentName{new string(' ', 10)}StudentGrade{new string(' ', 10)}Mark1{new string(' ', 10)}Mark2{new string(' ', 10)}Mark3{new string(' ', 10)}Mark4{new string(' ', 10)}Mark5");
            Console.WriteLine(new string('-', 150));

            foreach (Student student in StudentDetails.Values)
            {
                Console.WriteLine($"{new string(' ', 10)}{student.Student_Id}{new string(' ', "StudentId".Length + 10 - student.Student_Id.Length)}{student.Student_Name}{new string(' ', "StudentName".Length + 10 - student.Student_Name.Length)}{student.Grade}{new string(' ', 10 + "StudentGrade".Length - student.Grade.Length)}{student.Marks[0]}{new string(' ', 15 - student.Marks[0].ToString().Length)}{student.Marks[1]}{new string(' ', 15 - student.Marks[1].ToString().Length)}{student.Marks[2]}{new string(' ', 15 - student.Marks[2].ToString().Length)}{student.Marks[3]}{new string(' ', 15 - student.Marks[3].ToString().Length)}{student.Marks[4]}");
            }
            Console.WriteLine(new string('-', 150));

        }
        else
        {
            Console.WriteLine("No Items in the Dictionary");
        }

    }
    //Display the number of students in each Catogory
    public static void DisplayNumberOfStudentInEachCatogory()
    {
        int Aplus = StudentDetails.Where(x => x.Value.Grade.Equals("A+")).Select(x => x.Value).Count();
        int A = StudentDetails.Where(x => x.Value.Grade.Equals("A")).Select(x => x.Value).Count();
        int B = StudentDetails.Where(x => x.Value.Grade.Equals("B")).Select(x => x.Value).Count();
        int C = StudentDetails.Where(x => x.Value.Grade.Equals("C")).Select(x => x.Value).Count();
        int D = StudentDetails.Where(x => x.Value.Grade.Equals("D")).Select(x => x.Value).Count();
        int F = StudentDetails.Where(x => x.Value.Grade.Equals("F")).Select(x => x.Value).Count();
        int Pass = StudentDetails.Where(x => x.Value.Grade.Equals("Pass")).Select(x => x.Value).Count();
        int Fail = StudentDetails.Where(x => x.Value.Grade.Equals("Fail")).Select(x => x.Value).Count();
        Console.ForegroundColor= ConsoleColor.Green;
        Console.WriteLine(new string('-', 100));
        Console.WriteLine($"{new string(' ',20)}Regular student Details in each Catogory");
        Console.WriteLine(new string('-', 100));

        Console.WriteLine($"{new string(' ',30)} A+  = " + Aplus);
        Console.WriteLine($"{new string(' ', 30)} A   = " + A);
        Console.WriteLine($"{new string(' ', 30)} B   = " + B);
        Console.WriteLine($"{new string(' ', 30)} C   = " + C);
        Console.WriteLine($"{new string(' ', 30)} D   = " + D);
        Console.WriteLine($"{new string(' ', 30)} F   = " + F);
        Console.WriteLine(new string('-', 100));
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(new string('-', 100));

        Console.WriteLine($"{new string(' ', 20)}Exchange student Details in each Catogory");
        Console.WriteLine(new string('-', 100));

        Console.WriteLine($"{new string(' ', 30)} Pass  = " + Pass);
        Console.WriteLine($"{new string(' ', 30)} Fail  = " + Fail);
        Console.WriteLine(new string('-', 100));
        Console.ResetColor();

    }
    //Update the Student Details According to the Requirements
    public static void UpdateStudentDetails()
    {
        DisplayAllStudentDetails();
        Console.WriteLine("Enter the Student Id You want to Update");
        string StudentIdToUpdate = Console.ReadLine();
        while (!Validator.IsValidId(StudentIdToUpdate.ToUpper()))
        {
            Console.WriteLine("Student ID Not Found");
            Console.WriteLine("Do You Want To Continue (y/n)");
            string Continue = Console.ReadLine();
            while (!Continue.ToLower().Equals("y") && !Continue.ToLower().Equals("n"))
            {
                Console.WriteLine("Please Enter the Valid Option Either Y or N");
                Continue = Console.ReadLine();
            }
            if (Continue.ToLower().Equals("y"))
            {
                Console.WriteLine("Enter the valid StudentId");
                StudentIdToUpdate = Console.ReadLine();
            }
            else
            {
                return;
            }
        }
        Student student = StudentDetails[StudentIdToUpdate.ToUpper()];
        Type type = typeof(Student);
        PropertyInfo[] property = type.GetProperties();
        PropertyInfo[] RequiredProps = property.Where(x => !x.Name.Equals("Student_Id")).Where(x => !x.Name.Equals("Grade")).ToArray();
        int i = 1;
        foreach (PropertyInfo propertyInfo in RequiredProps)
        {

            Console.WriteLine(i++ + "." + propertyInfo.Name);

        }
        Console.WriteLine(i + "."+"Change Student Type");
        Console.WriteLine("Enter the Property You Want to Update");
        int option = 0;
        bool UpdateOptionLoop = true;
        while (UpdateOptionLoop)
        {

            try
            {
                option = int.Parse(Console.ReadLine());
                if (option > RequiredProps.Length+1 || option < 1)
                {
                    throw new Exception("Invalid option");
                }
                UpdateOptionLoop = false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please Enter the Valid Option");
                Console.ResetColor();
            }
        }
        if (option == 4)
        {
            if (student is RegularStudent)
            {
                Exchangestudent exchangeStudent = new Exchangestudent(student.Student_Name, student.Student_Email, student.Marks);
                Student.StudentDetails.Remove(student.Student_Id);
                using (StreamWriter reader = new StreamWriter(path)) ;

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    foreach (Student stu in Student.StudentDetails.Values)
                    {
                        writer.WriteLine(stu.ToString());
                    }
                }
                foreach(Student stu in Student.StudentDetails.Values)
                {
                    Console.WriteLine(stu.Student_Id);
                }
                Student.StudentDetails.Add(exchangeStudent.Student_Id, exchangeStudent);
                Console.WriteLine(exchangeStudent.Student_Id);
                exchangeStudent.AssignGrade(exchangeStudent, exchangeStudent.Marks);
                

            }
            else
            {
                RegularStudent regularStudent = new RegularStudent(student.Student_Name, student.Student_Email, student.Marks);
                Student.StudentDetails.Remove(student.Student_Id);
                using (StreamWriter reader = new StreamWriter(path)) ;

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    foreach (Student stu in Student.StudentDetails.Values)
                    {
                        writer.WriteLine(stu.ToString());
                    }
                }
                foreach (Student stu in Student.StudentDetails.Values)
                {
                    Console.WriteLine(stu.Student_Id);
                }
                Student.StudentDetails.Add(regularStudent.Student_Id, regularStudent);
                Console.WriteLine(regularStudent.Student_Id);

                regularStudent.AssignGrade(regularStudent, regularStudent.Marks);
            }
            Console.WriteLine("Student Type Successfully Changed");
            double RegularStudentId = 0;
            double ExchangestudentId = 0;
            foreach(Student stu in Student.StudentDetails.Values)
            {
                if (stu.Student_Id.StartsWith("R"))
                {
                    RegularStudentId = Math.Max(double.Parse(stu.Student_Id.Substring(1)), RegularStudentId);
                }
                if (stu.Student_Id.StartsWith("E"))
                {
                    ExchangestudentId = Math.Max(double.Parse(stu.Student_Id.Substring(1)), ExchangestudentId);
                }
            }
            RegularStudent.IdCounter = RegularStudentId;
            Exchangestudent.IdCounter = ExchangestudentId;
        }
        else
        {
            PropertyInfo PropertyTOUpdate = RequiredProps[option - 1];

            if (option == 1)
            {
                Console.WriteLine("Enter the Name to Update");
                var UpdatedValue = Console.ReadLine();
                while (!Validator.IsValidName(UpdatedValue))
                {
                    Console.WriteLine("Enter the Valid Name");
                    UpdatedValue = Console.ReadLine();
                }
                PropertyTOUpdate.SetValue(student, UpdatedValue);
            }
            if (option == 2)
            {
            ValidEmail:
                Console.WriteLine("Enter the Email to Update");
                var UpdatedValue = Console.ReadLine();
                while (!Validator.IsValidEmail(UpdatedValue))
                {
                    Console.WriteLine("Please Enter the Valid Email");
                    UpdatedValue = Console.ReadLine();
                }
                if (Validator.ContainsEmail((string)UpdatedValue))
                {
                    Console.WriteLine("User Email Already Exists");
                    goto ValidEmail;
                }
                PropertyTOUpdate.SetValue(student, UpdatedValue);

            }
            if (option == 3)
            {
                Console.WriteLine("Enter the Updated Value for Marks");

                for (int k = 0; k < 5;)
                {
                    Console.WriteLine($"Do you want to Update the Mark for Subject {k + 1} (y/n)");
                    string ContinueUpdate = Console.ReadLine();
                    while (!ContinueUpdate.ToLower().Equals("y") && !ContinueUpdate.ToLower().Equals("n"))
                    {
                        Console.WriteLine("Enter the Valid option (y/n)");
                        ContinueUpdate = Console.ReadLine();
                    }
                    if (ContinueUpdate.ToLower().Equals("n"))
                    {
                        k++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Enter the mark for Subject {k + 1}");
                        double Mark = 0;
                        try
                        {
                            Mark = double.Parse(Console.ReadLine());
                            if (Mark > 100 || Mark < 0) throw new Exception("Mark must be with in 100 and postivie number");
                            student.Marks[k] = Mark;
                        }
                        catch (FormatException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Mark Must Be a Numeic Value");
                            Console.WriteLine("Please Enter the Valid Mark");
                            Console.ResetColor();

                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                        k++;
                    }
                }


                student.Grade = student.CalculateGrade(student.Marks);


            }
           
        }
        using (StreamWriter reader = new StreamWriter(path)) ;

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            foreach (Student stu in Student.StudentDetails.Values)
            {
                writer.WriteLine(stu.ToString());
            }
        }


    }

    public abstract void AssignGrade(Student student, List<double> Marks);
    public abstract string CalculateGrade(List<double> Marks);
}