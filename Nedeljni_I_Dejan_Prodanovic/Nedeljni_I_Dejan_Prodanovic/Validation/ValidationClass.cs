﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Validation
{
    class ValidationClass
    {
        public static bool JMBGisValid(string JMBG, out DateTime dateOfBirth)
        {
            dateOfBirth = new DateTime();
            if (JMBG.Length != 13)
                return false;

            DateTime now = DateTime.Now;
            for (int i = 0; i < JMBG.Length; i++)
            {
                if (!Char.IsNumber(JMBG, i))
                    return false;
            }

            int year = 0;

            if (Char.GetNumericValue(JMBG[4]) == 0)
            {

                year = 2000 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);

                if (year > now.Year)
                {

                    return false;
                }

            }
            else if (Char.GetNumericValue(JMBG[4]) == 9)
            {
                year = 1900 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);
            }
            else
            {
                return false;
            }

            int month = (int)Char.GetNumericValue(JMBG[2]) * 10 + (int)Char.GetNumericValue(JMBG[3]);


            if (year == now.Year && month > now.Month)
            {
                return false;
            }

            if (month == 0 || month > 12)
                return false;

            int day = (int)Char.GetNumericValue(JMBG[0]) * 10 + (int)Char.GetNumericValue(JMBG[1]);

            if (year == now.Year && month == now.Month && day >= now.Day)
            {
                return false;
            }

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day == 0 || day > 31)
                    return false;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day == 0 || day > 30)
                    return false;
            }
            else if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
                {
                    if (day == 0 || day > 29)
                        return false;
                }
                else
                {
                    if (day == 0 || day > 28)
                        return false;
                }

            }
            dateOfBirth = new DateTime(year, month, day);
            return true;
        }

        public int CountAge(DateTime dateOfBirth)
        {
            int age;
            DateTime today = DateTime.Now;

            if (today.Month > dateOfBirth.Month)
                age = today.Year - dateOfBirth.Year;
            else if (today.Month < dateOfBirth.Month)
            {
                age = today.Year - dateOfBirth.Year - 1;
            }
            else
            {
                if (today.Day == dateOfBirth.Day || today.Day > today.Day)
                    age = today.Year - dateOfBirth.Year;
                else
                    age = today.Year - dateOfBirth.Year - 1;
            }
            return age;

        }
    }
}
