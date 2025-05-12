using System;

class Validator
{
    public static bool ContainsEmail(string email)
    {
        return Student.StudentDetails.Any(x => x.Value.Student_Email.Equals(email.ToLower()));
    }
    public static bool IsValidName(string name)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[A-Za-z]+( [A-Za-z]+)*$");
    }
    public static bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z][a-zA-Z0-9.]+@[a-zA-Z]+\.[a-zA-Z]{2,3}$");
    }
    public static bool IsValidId(string id)
    {
        return Student.StudentDetails.ContainsKey(id.Trim().ToUpper());
    }


}