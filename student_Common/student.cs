namespace student_Common;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Grade  { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Grade: {Grade}";
    }
    
}