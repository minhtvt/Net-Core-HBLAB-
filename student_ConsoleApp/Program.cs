using System;
using System.Linq;
using student_dataAccess;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("==== QUẢN LÝ SINH VIÊN ====\n");

        var repo = new StudentRepo();
        var students = repo.GetStudents();
        
        Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
        foreach (var s in students.Values)
            Console.WriteLine(s);
        
        // 1. Tim sinh vien co diem cao nhat
        var topStudent = students.Values
            .OrderByDescending(s => s.Grade)
            .FirstOrDefault();
        Console.WriteLine($"\n Sinh viên có điểm cao nhất: {topStudent}");
        
        // 2. Lấy danh sách tên sinh viên có điểm > X
        double X = 7.0;
        var higherStudents = students.Values
            .Where(s => s.Grade > X)
            .Select(s => s.Name);
        Console.WriteLine($"\nSinh viên có điểm > {X}: {string.Join(",", higherStudents)}");
        
        // 3. Đếm số lượng sinh viene đạt điểm trung bình trở lên (>=5.0)
        int count = students.Values.Count(s => s.Grade >= 5.0);
        Console.WriteLine($"\nSố sinh viên đạt điểm trung bình trở lên: {count}");
        
        Console.WriteLine("\n Kết thúc chương trình");

    }
}