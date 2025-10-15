using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Invoice_DataAccess;
using OfficeOpenXml;


public class InvoiceManager
{
    private List<Invoice> invoices = new List<Invoice>();
    private List<InteractionHistory> histories = new List<InteractionHistory>();

    public string ImportInvoicesFromExcel(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var errors = new List<string>();
        int addedCount = 0;
        try
        { 
        if (!File.Exists(filePath))
        return "Khong tim thay file: " + filePath;
        
        using var package = new ExcelPackage(new FileInfo(filePath));
        var sheet = package.Workbook.Worksheets[0];
        int row = 2;
        while (true)
        {
            string code = sheet.Cells[row, 1].Text?.Trim();
            string customer = sheet.Cells[row, 2].Text?.Trim();
            string dateStr = sheet.Cells[row, 3].Text?.Trim();
            string totalStr = sheet.Cells[row, 4].Text?.Trim();
            string debtStr = sheet.Cells[row, 5].Text?.Trim(); 
            
            // Nếu 3 ô đầu tiên đều trống => kết thúc
            if (string.IsNullOrWhiteSpace(code) &&
                string.IsNullOrWhiteSpace(customer) &&
                string.IsNullOrWhiteSpace(dateStr))
                break;
        bool hasError = false;
        
        //Validate
        if (string.IsNullOrWhiteSpace(code))
        {
            errors.Add($"Dòng {row}: Mã hoá đơn bị trống");
            hasError = true;
        }

        if (!DateTime.TryParse(dateStr, out DateTime date))
        {
            errors.Add($"Dòng {row}: Ngày xuất không hợp lệ");
            hasError = true;
        }
        // Tổng tiền
        if (!double.TryParse(totalStr, out double total) || total <= 0)
        {
            errors.Add($"Dòng {row}: Tổng tiền không hợp lệ");
            hasError = true;
            
        }
        // Tổng nợ
        if (!double.TryParse(debtStr, out double debt) || debt < 0)
        {
            errors.Add($"Dòng {row}: Tổng nợ không hợp lệ");
            hasError = true;
        }
        // Nếu dữ liệu hợp lệ
        if (!hasError)
        {
            invoices.Add(new Invoice
            {
                InvoiceCode = code,
                CustomerCode = customer,
                ExportDate = date,
                TotalAmount = total,
                DebtAmount = debt
            });
            addedCount++;
        }

        row++;
        }
        //result
        string result = $"Đã import {addedCount} hoá đơn thành công.\n";
        if (errors.Count > 0)
        {
            result += "Các lỗi: \n" + string.Join("\n", errors);
        }
        else
        {
            result += "Không có lỗi nào";
        }

        return result;
        }
        catch (Exception ex)
        {
            return"Lỗi khi đọc file Excel: " + ex.Message;
        }
    }
    // 2. Thêm lịch sử tương tác
    public void AddInteraction()
    {
        Console.Write("Nhập mã hoá đơn: ");
        string invoiceCode = Console.ReadLine();
        
        var invoice = invoices.FirstOrDefault(x => x.InvoiceCode == invoiceCode);
        if (string.IsNullOrEmpty(invoice.InvoiceCode))
        {
            Console.WriteLine("Không tìm thấy hoá đơn này");
            return;
        }
        Console.Write("Nhập mã nhân viên: ");
        string empCode = Console.ReadLine() ?? "";
        
        Console.Write("Loại tương tác (call/mail/meet): ");
        string type = Console.ReadLine() ?? "";
        
        Console.Write("Ghi chú: ");
        string note = Console.ReadLine() ?? "";
        
        histories.Add(new InteractionHistory
        {
            InvoiceCode = invoiceCode,
            EmployeeCode = empCode,
            InteractionType = type,
            Note = note,
            InteractionDate = DateTime.Now
        });    
        
        Console.WriteLine("Đa thêm lịch sử tương tác");
    }
    // 3. Xuất lịch sử tương tác ra Excel
    public void ExportInteractionReport(string mode)
    {
        DateTime startDate;
        DateTime endDate = DateTime.Now;
        
        if (mode == "week")
            startDate = DateTime.Now.AddDays(-7);
        else
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        
        var filtered = histories
            .Where(h => h.InteractionDate >= startDate && h.InteractionDate <= endDate)
            .ToList();

        if (filtered.Count == 0)
        {
            Console.WriteLine("Không có lịch sử tương tác trong khoảng thời gian này");
            return;
        }

        string fileName = $"InteractionReport_{mode}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Report");

        sheet.Cells[1, 1].Value = "Mã hoá đơn";
        sheet.Cells[1, 2].Value = "Mã NV";
        sheet.Cells[1, 3].Value = "Ngày";
        sheet.Cells[1, 4].Value = "Loại";
        sheet.Cells[1, 5].Value = "Ghi chú";

        int row = 2;
        foreach (var h in filtered)
        {
            sheet.Cells[row, 1].Value = h.InvoiceCode;
            sheet.Cells[row, 2].Value = h.EmployeeCode;
            sheet.Cells[row, 3].Value = h.InteractionDate.ToString("dd/MM/yyyy HH:mm");
            sheet.Cells[row, 4].Value = h.InteractionType;
            sheet.Cells[row, 5].Value = h.Note;
            row++;
        }
        package.SaveAs(new FileInfo(fileName));
        Console.WriteLine($"✅ Đã xuất file: {fileName}");
    }

    public void showInvoices()
    {
        if (invoices.Count == 0)
        {
            Console.WriteLine("Chưa có hoá đơn nào trong hệ thống");
            return;
        }
        Console.WriteLine("\n ===== DANH SÁCH HOÁ ĐƠN =====");
        Console.WriteLine($"{"Mã HĐ",-8} | {"Mã KH",-8} | {"Ngày Xuất",-12} | {"Tổng Tiền",12} | {"Tổng Nợ",10}");
        Console.WriteLine(new string('-', 60));

        foreach (var inv in invoices)
        {
            Console.WriteLine($"{inv.InvoiceCode,-8} | {inv.CustomerCode,-8} | {inv.ExportDate:dd/MM/yyyy,-12} | {inv.TotalAmount,12:N0} | {inv.DebtAmount,10:N0}");
        }
    }

    public void showHistories()
    {
        if (histories.Count == 0)
        {
            Console.Write("Chưa có lịch sử tương tác nào trong hệ thống");
            return;
        }
        Console.WriteLine("\n ==== LỊCH SỬ TƯƠNG TÁC ====");
        Console.WriteLine($"{"Mã HĐ",-8} | {"Mã NV", -8} | {"Ngày",-12} | {"Loại", -10} | {"Ghi chú"}");
        Console.WriteLine(new string('-', 70));

        foreach (var h in histories)
        {
            Console.WriteLine($"{h.InvoiceCode,-8} | {h.EmployeeCode,-8} | {h.InteractionDate:dd/MM/yyyy,-12} | {h.InteractionType,-10} | {h.Note}");
        }
    }
}