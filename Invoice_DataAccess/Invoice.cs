namespace Invoice_DataAccess;
using System;

public struct Invoice
{
    public string InvoiceCode { get; set; } //Ma hoa don
    public string CustomerCode { get; set; } //Ma khach hang
    public DateTime ExportDate { get; set; } // Ngay san xuat hoa don
    public double TotalAmount { get; set; } // Tong tien
    public double DebtAmount { get; set; } // Tong tien no
}