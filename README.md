# 💻 .Net-Core-HBLAB-

## Nội dung học
`C# Fundamental` ・ `Dependency Injection` ・ `.NET 8` ・ `Entity Framework` ・ `LINQ` ・ `Authentication / Authorization` ・ `OOP` ・ `N-Layer Architecture`

---

## Bài 1: Quản lý nhân sự

### Yêu cầu
Viết chương trình **C#** sử dụng `struct` để quản lý nhân sự cho một công ty.

### Struct `Employee`
- Mã nhân viên  
- Tên nhân viên  
- Ngày vào công ty  
- Hệ số lương  
- Vị trí công việc *(hệ số lương tự định nghĩa theo vị trí)*

---

### Chức năng chương trình

1. **Nhập danh sách nhân viên từ bàn phím**  
2. **Nhập danh sách nhân viên từ file Excel có sẵn**  
   - Nếu dòng nào bị sai hoặc thiếu dữ liệu → hiển thị thông báo lỗi (dòng, cột lỗi)  
3. **Hiển thị danh sách nhân viên**  
4. **Tìm kiếm nhân viên theo thâm niên**  
   - Công ty tặng quà cho nhân viên có thời gian làm việc **≥ 5 năm hoặc 10 năm**  

---

## Bài 2: Quản lý công nợ

### Struct `Invoice`
- Mã hóa đơn  
- Mã khách hàng  
- Ngày xuất hóa đơn  
- Tổng tiền  
- Tổng tiền nợ  

### Struct `NV_CongNo`
- Nhân viên công nợ (người thu hồi)  
- Lịch sử tương tác

---

### Chức năng yêu cầu

1. **Import hóa đơn từ file Excel**  
2. **Lưu lịch sử tương tác với từng hóa đơn**  
   - Gọi điện, gửi mail, gặp khách hàng, v.v.  
3. **Xuất danh sách lịch sử tương tác ra file Excel**  
   - Theo **tuần** hoặc **tháng**

---

### Bài 3: Cấu trúc dữ liệu Stack(Generic)

### MyStack<T>
- Push(T item): Thêm một phần tử vào đỉnh stack
- Pop(): Lấy và xóa phần tử ở đỉnh stack
- Peek(): Lấy phần tử ở đỉnh stack nhưng không xóa
- IsEmpty(): Kiểm tra stack có rỗng hay không

---

### Chức năng yêu cầu
1. **Tạo lớp generic MyStack<T> mô phỏng cấu trúc dữ liệu Stack**
2. **Kiểm tra hoạt động của stack với nhiều kiểu dữ liệu khác nhau:**
   - int
   - string
   - doube
   - hoặc lớp tự định nghĩa như Student
4. **Hiển thị kết quả từng thao tác (Push, Pop, Peek, IsEmpty) trên màn hình console**

---


## Bài 4: Quản lý sinh viên với Dictionary và LINQ

### Class Student
   - ID
   - Name
   - Grade

---

### Chức năng yêu cầu
1. **Tạo Dictionary<int, Student> để quản lý sinh viên theo ID**  
2. **Thêm một số sinh viên mẫu vào dictionary**  
3. **Thực hiện các truy vấn LINQ**  
   - Tìm sinh viên có điểm cao nhất
   - Lấy danh sách tên sinh viên có điểm lớn hơn giá trị cho trước (X)
   - Đếm số sinh viên đạt điểm trung bình trở lên (≥ 5.0)
