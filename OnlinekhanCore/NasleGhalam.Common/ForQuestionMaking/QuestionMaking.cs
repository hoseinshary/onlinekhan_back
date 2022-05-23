using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.Common.ForQuestionMaking
{
    public class QuestionMaking
    {

        public static bool IsQuestionParagraph(string s)
        {
            var arrayTemp = s.ToCharArray();

            var i = 0;
            while (i < arrayTemp.Length)
            {
                if (arrayTemp[i] == ' ' || arrayTemp[i] == '\n' || arrayTemp[i] == '\r')
                {
                    i++;
                }
                else if (char.IsDigit(arrayTemp[i]))
                {
                    i++;
                    while (char.IsDigit(arrayTemp[i]) && i < arrayTemp.Length)
                    {
                        i++;
                    }
                    if (arrayTemp[i] == '-')
                    {
                        var j = 0;
                        while (j < 14 && i < arrayTemp.Length)
                        {
                            i++;
                            j++;
                        }
                        if (j == 14)
                            return true;
                    }
                    return false;
                }
                else
                {
                    break;
                }
                i++;
            }
            return false;
        }


        public static bool IsAnswerParagraph(string s)
        {
            var arrayTemp = s.ToCharArray();

            var i = 0;
            while (i < arrayTemp.Length)
            {
                if (arrayTemp[i] == ' ' || arrayTemp[i] == '\n' || arrayTemp[i] == '\r' || arrayTemp[i] == '\v')
                {
                    i++;
                    continue;
                }
                else if (char.IsDigit(arrayTemp[i]))
                {
                    i++;
                    while (char.IsDigit(arrayTemp[i]) && i < arrayTemp.Length)
                    {
                        i++;
                    }
                    if (arrayTemp[i] == '-')
                    {
                        var j = 0;
                        while (j < 5 && i < arrayTemp.Length)
                        {
                            if(arrayTemp[i] == '=')
                            {
                                return false;
                            }
                            i++;
                            j++;
                        }
                        if (j == 5)
                            return true;
                    }
                    return false;
                }
                else
                {
                    break;
                }
                i++;
            }
            return false;
        }
    }
}
