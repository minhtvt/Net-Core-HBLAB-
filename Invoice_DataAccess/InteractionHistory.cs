using System;

namespace Invoice_DataAccess;

public struct InteractionHistory
{
    public string InvoiceCode {get;set;} // Ma hoa don
    public string EmployeeCode {get;set;} // Ai tuong tac
    public DateTime InteractionDate {get;set;} // Ngay tuong tac
    public string InteractionType {get;set;}    //goi dien, gui mail, gap truc tiep
    public string Note {get;set;}  // ghi chu
}