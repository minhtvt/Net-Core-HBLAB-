using System.Text.RegularExpressions;

namespace BE_2025.Common;

public class ValidateDataInput
{
    //Kiem tra so hop le
    public static bool CheckValidInputNumber(string inputNumber)
    {
        if (string.IsNullOrEmpty(inputNumber))
            return false;

        inputNumber = inputNumber.Trim();

        if (!int.TryParse(inputNumber, out int numberInput))
            return false;

        if (numberInput > int.MaxValue)
            return false;
        
        return true;
    }
    //Kiem tra chuoi hop le(khong trong, khong toan so, khong qua dai)
    public static bool CheckValidateString(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
            return false;

        if (int.TryParse(inputString, out _))
            return false;

        if (inputString.Length > 200)
            return false;

        return true;
    }

    public static bool CheckSpeacialCharacter(string inputString)
    {
        if (string.IsNullOrWhiteSpace(inputString))
            return false;

        // ✅ Cho phép chữ cái (kể cả có dấu tiếng Việt), số và khoảng trắng
        var regexItem = new Regex(@"^[\p{L}\p{M}0-9\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        return regexItem.IsMatch(inputString);
    }

    //Chong ma doc(XSS, HTML, JS)
    public static bool CheckXSSInput(string input)
    {
        try
        {
            var listdangerousString = new List<string>
            {
                "<applet", "<body", "<embed", "<frame", "<script", "<frameset",
                "<html", "<iframe", "<img", "<style", "<layer", "<link", "<ilayer",
                "<meta", "<object", "<h", "<input", "<a", "&lt", "&gt"
            };
            if (string.IsNullOrEmpty(input))
                return true;
            foreach (var dangerous in listdangerousString)
            {
                if (input.Trim().ToLower().IndexOf(dangerous) >= 0)
                    return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}