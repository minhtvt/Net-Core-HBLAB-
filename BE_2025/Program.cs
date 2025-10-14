using System;
using System.ComponentModel;
using BE_2025.DataAccess;
using OfficeOpenXml;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace BE_2025
{
    class Program
    {
        static void Main()
        {
            var manager = new EmployerManager();

            while (true)
            {
                Console.WriteLine("\n=== MENU QUẢN LÝ NHÂN VIÊN ===");
                Console.WriteLine("1. Nhập nhân viên mới (thủ công)");
                Console.WriteLine("2. Nhập danh sách từ file Excel");
                Console.WriteLine("3. Hiển thị danh sách nhân viên");
                Console.WriteLine("4. Tìm nhân viên có thâm niên 5 hoặc 10 năm");
                Console.WriteLine("5. Thoát");
                Console.Write("Chọn: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        NhapNhanVien(manager);
                        break;
                    case "2":
                        NhapTuExcel(manager);
                        break;
                    case "3":
                        HienThiDanhSach(manager);
                        break;
                    case "4":
                        TimNhanVienTheoThamNien(manager);
                        break;
                    case "5":
                        Console.WriteLine("Kết thúc chương trình!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }

            static void NhapTuExcel(EmployerManager manager)
            {
                Console.Write("Nhập đường dẫn file Excel (.xlsx): ");
                string filePath = Console.ReadLine() ?? "";

                string result = manager.Employer_Insert_FromExcelFile(filePath);
                Console.WriteLine("\n" + result);
            }

            static void HienThiDanhSach(EmployerManager manager)
            {
                var list = manager.GetAllEmployees();

                if (list.Count == 0)
                {
                    Console.WriteLine("Danh sách nhân viên trống!");
                    return;
                }

                Console.WriteLine("\n=== DANH SÁCH NHÂN VIÊN ===");
                Console.WriteLine($"{"Mã NV",-10} | {"Tên nhân viên",-25} | {"Ngày vào",-15}");
                Console.WriteLine(new string('-', 55));

                foreach (var e in list)
                {
                    Console.WriteLine($"{e.EmployerCode,-10} | {e.EmployerName,-25} | {e.StartDate:dd/MM/yyyy}");
                }
            }

            static void TimNhanVienTheoThamNien(EmployerManager manager)
            {
                var list = manager.GetAllEmployees();

                if (list.Count == 0)
                {
                    Console.WriteLine("Danh sách nhân viên trống!");
                    return;
                }

                Console.WriteLine("Nhập mốc năm cần tìm (5 hoặc 10): ");
                if (!int.TryParse(Console.ReadLine(), out int mocNam) || (mocNam != 5 && mocNam != 10))
                {
                    Console.WriteLine("Chỉ được nhập 5 hoặc 10!");
                    return;
                }

                var ketQua = new List<employer>();
                foreach (var e in list)
                {
                    int soNam = DateTime.Now.Year - e.StartDate.Year;
                    if (soNam == mocNam)
                        ketQua.Add(e);
                }

                if (ketQua.Count == 0)
                {
                    Console.WriteLine($"Không có nhân viên nào đạt {mocNam} năm thâm niên.");
                    return;
                }

                Console.WriteLine($"\n=== NHÂN VIÊN CÓ {mocNam} NĂM THÂM NIÊN ===");
                foreach (var e in ketQua)
                {
                    Console.WriteLine($"- {e.EmployerCode}: {e.EmployerName} (Ngày vào: {e.StartDate:dd/MM/yyyy})");
                }
            }

            static void NhapNhanVien(EmployerManager manager)
            {
                Console.Write("Nhập mã nhân viên (VD: NV001): ");
                string code = Console.ReadLine() ?? "";

                Console.Write("Nhập tên nhân viên: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Nhập ngày vào công ty (dd/MM/yyyy): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null,
                        System.Globalization.DateTimeStyles.None, out DateTime startDate))
                {
                    Console.WriteLine("Ngày không hợp lệ!");
                    return;
                }

                int result = manager.Employer_Insert(code, name, startDate);

                switch ((EmployerManagerStatus)result)
                {
                    case EmployerManagerStatus.THANH_CONG:
                        Console.WriteLine("Thêm nhân viên thành công!");
                        break;

                    case EmployerManagerStatus.MA_NHAN_VIEN_KHONG_HOP_LE:
                        Console.WriteLine("Mã nhân viên không hợp lệ!");
                        break;

                    case EmployerManagerStatus.TEN_KHONG_HOP_LE:
                        Console.WriteLine("Tên nhân viên không hợp lệ!");
                        break;

                    case EmployerManagerStatus.DA_TON_TAI:
                        Console.WriteLine("Nhân viên này đã tồn tại!");
                        break;

                    default:
                        Console.WriteLine("Thêm nhân viên thất bại!");
                        break;
                }

            }
        }
    }
}