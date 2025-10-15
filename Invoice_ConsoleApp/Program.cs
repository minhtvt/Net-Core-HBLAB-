// See https://aka.ms/new-console-template for more information

using System;
using Invoice_DataAccess;
var manager = new InvoiceManager();

while (true)
{
    Console.WriteLine("\n=== MENU QUẢN LÝ CÔNG NỢ ===");
    Console.WriteLine("1. Import hóa đơn từ Excel");
    Console.WriteLine("2. Thêm lịch sử tương tác");
    Console.WriteLine("3. Xuất báo cáo tương tác theo tuần");
    Console.WriteLine("4. Xuất báo cáo tương tác theo tháng");
    Console.WriteLine("5. Hiển thị danh sách hóa đơn");
    Console.WriteLine("6. Hiển thị lịch sử tương tác");
    Console.WriteLine("7. Thoát");
    Console.Write("Chọn: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Nhập đường dẫn file Excel: ");
            string path = Console.ReadLine() ?? "";
            Console.WriteLine(manager.ImportInvoicesFromExcel(path));
            break;

        case "2":
            manager.AddInteraction();
            break;

        case "3":
            manager.ExportInteractionReport("week");
            break;

        case "4":
            manager.ExportInteractionReport("month");
            break;
        
        case "5":
            manager.showInvoices();
            break;
        
        case "6":
            manager.showHistories();    
            break;
        
        case "7":
            return;

        default:
            Console.WriteLine("Lựa chọn không hợp lệ!");
            break;
    }
}
