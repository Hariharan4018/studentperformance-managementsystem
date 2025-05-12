using System;

public class RegularStudent: Student
{
    public static double IdCounter = 0;
    public RegularStudent(string Student_Name, string Student_Email, List<double> Marks):base(Student_Name, Student_Email,  Marks)
    {
        this.Student_Id = "R"+ ++IdCounter;
    }
    public RegularStudent(string Student_Id, string Student_Name, string Student_Email, string Grade, List<double> Marks):base(Student_Name, Student_Email, Marks)
    {
        this.Student_Id = Student_Id;
        this.Grade = Grade;
 
    }
    public  void AddStudent(Student student)
    {
        base.AddStudent(student);
    }
    public override void AssignGrade(Student student,List<double> Marks)
    {
        student.Grade = CalculateGrade(Marks);
    }
    public override string CalculateGrade(List<double> Marks)
    {
        double Average = CalculateAverage(Marks);
        if (Average >= 95)
        {
            return "A+";
        }
        else if (Average >= 90)
        {
            return "A";
        }
        else if (Average >= 80)
        {
            return "B";
        }
        else if (Average >= 70)
        {
            return "C";
        }
        else if (Average >= 60)
        {
            return "D";
        }
        return "F";
    }
    public double CalculateAveage(List<double> Marks)
    {
        return base.CalculateAverage(Marks);
    }
}
