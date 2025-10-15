using student_Common;
using System.Collections.Generic;   

namespace student_dataAccess;

public class StudentRepo
{
    public Dictionary<int, Student> GetStudents()
    {
        return new Dictionary<int, Student>
        {
            { 1, new Student { ID = 1, Name = "Mike", Grade = 8.5 } },
            { 2, new Student { ID = 2, Name = "Alice", Grade = 8 } },
            { 3, new Student { ID = 3, Name = "Bob", Grade = 7.5 } },
            { 4, new Student { ID = 4, Name = "Carol", Grade = 7.5 } },
            { 5, new Student { ID = 5, Name = "David", Grade = 7 } },
        };

    }
}
