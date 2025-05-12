using System;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

class Program
{
    static string FilePath = ConfigurationManager.AppSettings["FilePath"];
    static string path = Path.Combine(FilePath, "log.txt");
    static void Main(string[] args)
    {

        StudentPerformanceTracker studentPerformanceTracker = new StudentPerformanceTracker();

        bool StudentPerformanceTrackerLoop = true;
        while (StudentPerformanceTrackerLoop)
        {
            Console.WriteLine("Welcome to Student Performance Tracker");
            Console.WriteLine("1.AddStudent");
            Console.WriteLine("2.UpdateStudent");
            Console.WriteLine("3.DeleteStudent");
            Console.WriteLine("4.Search Student by Name or Id");
            Console.WriteLine("5.Display All Student Details");
            Console.WriteLine("6.Display the Number of Students in Each catogory");
            Console.WriteLine("7.Exit");

            Console.WriteLine("Please choose the option you want to perform");
            bool UserOptionLoop = true;
            int UserOption = 0;
            while (UserOptionLoop)
            {
                try
                {
                    UserOption = int.Parse(Console.ReadLine());
                    if (UserOption > 7 || UserOption < 1) { throw new Exception("Invalid Option"); }
                    UserOptionLoop = false;
                }
                catch (FormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The UserOption Must be a numeric value");
                    Console.ResetColor();
                    Console.WriteLine("Please Enter the valid Option");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Please Enter the valid Option");
                }
            }
            switch (UserOption)
            {
                case 1:
                    AddNewStudent(studentPerformanceTracker);
                    break;
                case 2:
                    UpdateStudentDetails(studentPerformanceTracker);
                    break;
                case 3:
                    DeleteStudentDetails(studentPerformanceTracker);
                    break;
                case 4:
                    SearchStudentDetailsByIdOrName(studentPerformanceTracker);
                    break;
                case 5:
                    DisplayAllStudentDetails(studentPerformanceTracker);
                    break;
                case 6:
                    DisplayNumberOfStudentInEachCatogory(studentPerformanceTracker);
                    break;
                case 7:
                    Console.WriteLine("Application Closed");
                    return;

            }
        }


    }
    static void AddNewStudent(StudentPerformanceTracker studentPerformanceTracker)
    {
        Console.WriteLine("Enter the Student Name");
        string StudentName = Console.ReadLine();
        while (!Validator.IsValidName(StudentName))
        {
            Console.WriteLine("Student Name Must Be a Alpabatical word");
            StudentName = Console.ReadLine();
        }
    ValidEmail:
        Console.WriteLine("Enter the Student Email Id");
        string Student_Email = Console.ReadLine();
        while (!Validator.IsValidEmail(Student_Email))
        {
            Console.WriteLine("Please Enter the Valid Email");
            Student_Email = Console.ReadLine();
        }
        if (Validator.ContainsEmail(Student_Email))
        {
            Console.WriteLine("User Email Already Exists");
            goto ValidEmail;
        }
        List<double> Marks = new List<double>();
        Console.WriteLine("Enter The Mark for 5 Subjects");
        for (int i = 0; i < 5;)
        {
            Console.WriteLine($"Enter the mark for Subject {i + 1}");
            double Mark = 0;
            try
            {
                Mark = double.Parse(Console.ReadLine());
                if (Mark > 100 || Mark < 0) throw new Exception("Mark must be with in 100 and postivie number");
                Marks.Add(Mark);
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
            i++;
        }
        Console.WriteLine("Select The type of student");
        Console.WriteLine("1.Regular student");
        Console.WriteLine("2.Exchange Student");
        int StudentType = 0;
        bool StudentTypeLoop = true;
        while (StudentTypeLoop)
        {
            try
            {
                StudentType = int.Parse(Console.ReadLine());
                if (StudentType > 2 || StudentType < 1)
                {
                    throw new Exception("Invalid option");
                }
                StudentTypeLoop = false;
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input shuold be a number");
                Console.WriteLine("Please Enter the Valid option");
                Console.ResetColor();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please Enter the Valid option");

                Console.ResetColor();
            }
        }

        if (StudentType == 1)
        {
            RegularStudent regularStudent = new RegularStudent(StudentName, Student_Email, Marks);
            studentPerformanceTracker.AssignGrade(regularStudent);
            studentPerformanceTracker.AddStudent(regularStudent);
            using(StreamWriter writer =new StreamWriter(path,true))
            {
                writer.WriteLine(regularStudent.ToString());
            }

        }
        else
        {
            Exchangestudent exchangestudent = new Exchangestudent(StudentName, Student_Email, Marks);
            studentPerformanceTracker.AssignGrade(exchangestudent);
            studentPerformanceTracker.AddStudent(exchangestudent);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(exchangestudent.ToString());
            }
        }
        Console.WriteLine("Student Added Successfully");

    }
    static void DeleteStudentDetails(StudentPerformanceTracker studentPerformanceTracker)
    {
        Console.WriteLine("Enter the Student Id To Delete");
        string StudentIdToDelete = Console.ReadLine().Trim();
        while (!Validator.IsValidId(StudentIdToDelete))
        {
            Console.WriteLine("Please Enter the Valid User Id To Delete");
            StudentIdToDelete = Console.ReadLine();
        }
        studentPerformanceTracker.DeleteStudentDetails(StudentIdToDelete);
    }
    static void SearchStudentDetailsByIdOrName(StudentPerformanceTracker studentPerformanceTracker)
    {
        Console.WriteLine("Enter the Student Id Or Name to Search");
        string FieldToSearch = Console.ReadLine();
        List<Student> students = studentPerformanceTracker.SearchStudentDetailsByIdOrName(FieldToSearch);
        if (students.Count > 0)
        {
            Console.WriteLine($"{new string(' ', 10)}StudentId{new string(' ', 10)}StudentName{new string(' ', 10)}StudentGrade");

        }
        if (students.Count > 0)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"{new string(' ', 10)}{student.Student_Id}{new string(' ', "StudentId".Length + 10 - student.Student_Id.Length)}{student.Student_Name}{new string(' ', "StudentName".Length + 10 - student.Student_Name.Length)}{student.Grade}");
            }
        }
        else
        {
            Console.WriteLine("Name Or Id Not Found");
        }

    }
    static void DisplayAllStudentDetails(StudentPerformanceTracker studentPerformanceTracker)
    {
        studentPerformanceTracker.DisplayAllStudentDetails();
    }
    static void DisplayNumberOfStudentInEachCatogory(StudentPerformanceTracker studentPerformanceTracker)
    {
        studentPerformanceTracker.DisplayNumberOfStudentInEachCatogory();
    }
    static void UpdateStudentDetails(StudentPerformanceTracker studentPerformanceTracker)
    {
        studentPerformanceTracker.UpdateStudentDetails();
    }
}