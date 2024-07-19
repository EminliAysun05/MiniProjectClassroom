using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectClassroom
{
    public static class StringExtension
    {
        public static bool IsValidNameSurName(this string value)
        {
            if (value.Length < 3) return false;
            if (!char.IsUpper(value[0])) return false;
            if (value.Contains(" ")) return false;
            return true;
        }
        public static bool IsValidClassroom(this string value)
        {
            int upperCount = 0;
            int digitCount = 0;
            foreach (char c in value)
            {
                if (char.IsUpper(c))
                {
                    upperCount++;
                }
                else if (char.IsDigit(c))
                {
                    digitCount++;
                }

            }

            return upperCount == 2 && digitCount == 3 && value.Length == 5;
        }

    }
}

