using System;
using System.Configuration;

class StudentPerformanceTracker
{

    static string FilePath = ConfigurationManager.AppSettings["FilePath"];
    static string path = Path.Combine(FilePath, "log.txt");
   
    public StudentPerformanceTracker()
    {
        //Console.WriteLine("hi");
        //Console.WriteLine(path);
        if (File.Exists(path)) {
            using (StreamReader sr = new StreamReader(path)) {
                string Line;
                while ((Line = sr.ReadLine()) != null)
                {
                    string[] line=Line.Split(',');
                    Console.WriteLine(line.Length);
                    if (line[3].Equals("PASS") || line[3].Equals("FAIL"))
                    {
                        List<double> Marks = new List<double>();
                        Marks.Add(double.Parse(line[4]));
                        Marks.Add(double.Parse(line[5]));
                        Marks.Add(double.Parse(line[6]));
                        Marks.Add(double.Parse(line[7]));
                        Marks.Add(double.Parse(line[8]));
                        Student.StudentDetails[line[0]]=new Exchangestudent(line[0], line[1], line[2], line[3],Marks);
                        
                    }
                    else
                    {
                        List<double> Marks = new List<double>();
                        Marks.Add(double.Parse(line[4]));
                        Marks.Add(double.Parse(line[5]));
                        Marks.Add(double.Parse(line[6]));
                        Marks.Add(double.Parse(line[7]));
                        Marks.Add(double.Parse(line[8]));
                        Student.StudentDetails[line[0]] = new RegularStudent(line[0], line[1], line[2], line[3], Marks);
                        
                        
                    }
                }
                double count = 0;

                foreach (Student stu in Student.StudentDetails.Values)
                {
                    if (stu.Student_Id.StartsWith("E"))
                    {
                        count = Math.Max(double.Parse(stu.Student_Id.Substring(1)), count);
                    }
                }
                Exchangestudent.IdCounter = count;
                count = 0;
                foreach (Student stu in Student.StudentDetails.Values)
                {
                    if (stu.Student_Id.StartsWith("R"))
                    {
                        count = Math.Max(double.Parse(stu.Student_Id.Substring(1)), count);
                    }
                }
                RegularStudent.IdCounter = count;
            }
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(path)) ;
        }
    }
    public void AddStudent(Student student)
    {
        student.AddStudent(student);
    }
    public void AssignGrade(Student student)
    {
        student.AssignGrade(student, student.Marks);
    }
    public void DeleteStudentDetails(string ID)
    {
        Student.StudentDetails.Remove(ID.ToUpper());
        using (StreamWriter reader = new StreamWriter(path)) ;
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            foreach(Student student in Student.StudentDetails.Values)
            {
                writer.WriteLine(student.ToString());
            }
        }
    }
    public List<Student> SearchStudentDetailsByIdOrName(String FieldToSearch)
    {
        return Student.SearchStudentDetailsByIdOrName(FieldToSearch);
    }
    public void DisplayAllStudentDetails()
    {
        Student.DisplayAllStudentDetails();
    }
    public void DisplayNumberOfStudentInEachCatogory()
    {
        Student.DisplayNumberOfStudentInEachCatogory();
    }
    public void UpdateStudentDetails()
    {
        Student.UpdateStudentDetails();
    }
    
}