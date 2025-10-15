// See https://aka.ms/new-console-template for more information

using generic_Common;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("==== DEMO STACK GENERIC ==\n");

        // stack<int>
        var intStack = new MyStack<int>();
        intStack.Push(10);
        intStack.Push(20);
        intStack.Push(30);
        Console.WriteLine($"Đỉnh stack (int): {intStack.Peek()}");
        Console.WriteLine($"Pop: {intStack.Pop()}");
        Console.WriteLine($"Stack rỗng? {intStack.IsEmpty()}");
        
        //stack<string>
        Console.WriteLine("\n--- Stack<string> ---");
        var strStack = new MyStack<string>();
        strStack.Push("Hello");
        strStack.Push("World");
        Console.WriteLine($"Đỉnh stack (string): {strStack.Peek()}");
        Console.WriteLine($"Pop: {strStack.Pop()}");    
        
        //stack<double>
        Console.WriteLine("\n--- Stack<double> ---");
        var doubleStack = new MyStack<double>();
        doubleStack.Push(1.5);
        doubleStack.Push(3.14);
        Console.WriteLine($"Đỉnh stack (double): {doubleStack.Peek()}");
        Console.WriteLine($"Pop: {doubleStack.Pop()}");

        Console.WriteLine("\n Chương trình kết thúc");
    }
    
}