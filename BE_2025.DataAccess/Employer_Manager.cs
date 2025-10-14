using BE_2025.Common;
using OfficeOpenXml;

namespace BE_2025.DataAccess;

public class EmployerManager
{
    List<BE_2025.DataAccess.employer> empl = new List<employer>();
    public int Employer_Insert(string employerCodeInput, string employerNameInput, DateTime startDateInput)
    {
        var ketqua = 0;
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //Buoc 1: kiem tra du lieu dau vao
            if (!BE_2025.Common.ValidateDataInput.CheckValidateString(employerCodeInput)
                || ! BE_2025.Common.ValidateDataInput.CheckXSSInput(employerCodeInput))
            {
                ketqua = (int)EmployerManagerStatus.MA_NHAN_VIEN_KHONG_HOP_LE;
            }

            if (!BE_2025.Common.ValidateDataInput.CheckValidateString(employerNameInput))
            {
                ketqua =  (int)EmployerManagerStatus.TEN_KHONG_HOP_LE;
            }
            //Buoc 2: Check trung trong list
            var isDuplicate = false;
            for (int i = 0; i < empl.Count; i++)
            {
                if (empl[i].EmployerCode == employerCodeInput)
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (isDuplicate)
            {
                ketqua = (int)EmployerManagerStatus.DA_TON_TAI;
            }
            // //C2:
            // var isdup = empl.FindAll(s => s.EmployerCode == employerCodeInput).FirstOrDefault();
            // if (isdup.EmployerCode != null)
            // {
            //     ketqua = (int)EmployerManagerStatus.DA_TON_TAI;
            // }
            
            //Buoc 3: Them vao danh sach va nhan ket qua
            var new_emp = new employer();
            new_emp.EmployerCode = employerCodeInput;
            new_emp.EmployerName = employerNameInput;
            new_emp.StartDate = startDateInput;
            
            empl.Add(new_emp);
            ketqua = (int)EmployerManagerStatus.THANH_CONG;
        }
        catch (Exception ex)
        {
            throw new Exception("Lỗi ngoại lệ:" + ex.Message);
        }

        return ketqua;
    }

    public string Employer_Insert_FromExcelFile(string filePath)
    {
        var ketqua = "";
        var errors = new List<string>();
        
        int addCount = 0;
        
        try
        {
            // Kiem tra file ton tai
            if(!File.Exists(filePath))
                return "Khong tim thay file: " + filePath;
            //Mo file excel
            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets[0]; 
            //Gia su file co tieu de o dong 1 -> du lieu bat dau dong 2
            int row = 2;
            //Lap qua tung dong cho den khi gap dong trong
            while (true)
            {
                string code = worksheet.Cells[row, 1].Text?.Trim();
                string name = worksheet.Cells[row, 2].Text?.Trim();
                string dateStr = worksheet.Cells[row, 3].Text?.Trim();

                //Neu ca dong trong -> dung
                if (string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(dateStr))
                    break;

                bool hasError = false;

                //validate tung o du lieu
                if (!ValidateDataInput.CheckValidateString(code))
                {
                    errors.Add($"Dòng {row}, Cột 1: Mã NV không hợp lệ hoặc trống");
                    hasError = true;
                }
                else if (!ValidateDataInput.CheckSpeacialCharacter(code))
                {
                    errors.Add($"Dòng {row}, Cột 1: Mã NV chứa kí tự đặc biệt");
                    hasError = true;
                }

                if (!ValidateDataInput.CheckValidateString(name))
                {
                    errors.Add($"Dòng {row}, Cột 2: Tên NV không hợp lệ hoặc trống");
                    hasError = true;
                }
                else if (!ValidateDataInput.CheckSpeacialCharacter(name))
                {
                    errors.Add($"Dòng {row}, Cột 2: Tên NV chứa kí tự đặc biệt");
                    hasError = true;
                }

                if (!DateTime.TryParse(dateStr, out DateTime startDate))
                {
                    errors.Add($"Dòng {row}, Cột 3: Ngày vào công ty không hợp lệ");
                    hasError = true;
                }

                if (hasError)
                {
                    row++;
                    continue;
                }
                int result = Employer_Insert(code, name, startDate);
                if (result == (int)EmployerManagerStatus.THANH_CONG)
                    addCount++;
                else
                    errors.Add($"Dòng {row}: Không thể thêm (Mã lỗi {result}).");

                row++;
            }
            

            ketqua = $"Đã thêm {addCount} nhân viên thành công.\n";
            if (errors.Count > 0)
            {
                ketqua += "Các lỗi phát hiện:\n";
                foreach (var err in errors)
                    ketqua += " - " + err + "\n";
            }
            else
            {
                ketqua += "Không có lỗi nào.\n";
            }
        }
        catch (Exception ex)
        {
            ketqua = "Lỗi khi đọc file Excel: " + ex.Message;
        }

        return ketqua;
    }
    public List<employer> GetAllEmployees()
    {
        return empl;
    }

}
