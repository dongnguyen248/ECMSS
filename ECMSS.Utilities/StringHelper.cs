using System.Text;
using System.Text.RegularExpressions;

namespace ECMSS.Utilities
{
    public static class StringHelper
    {
        public static string StringNormalization(string input)
        {
            input = ConvertToUnSign(input);
            input = RemoveExtraSpace(input);
            return input;
        }

        private static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        private static string RemoveExtraSpace(this string input)
        {
            Regex regex = new Regex(@"\s\s+");
            input = regex.Replace(input, " ").Trim();
            return input;
        }

        public static bool CheckContainSpecialCharacters(string input)
        {
            string specCharacters = @"\|!#$%&/=?»«@£§€{}'<>";
            foreach (char item in specCharacters)
            {
                if (input.Contains(item.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}