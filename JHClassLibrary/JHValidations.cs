﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace JHClassLibrary
{
    public class JHValidations : ValidationAttribute
    {
        public static string JHCapitalize(string input)
        {
            if (string.IsNullOrEmpty(input) == false)
            {
                input.ToLower();
                input.Trim();
                input = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
            }
            return input;
        }

        public static bool JHPostalCodeValidation(ref string input)
        {
            Regex postCodePattern = new Regex(@"^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(input))
                return true;

            else if (postCodePattern.IsMatch(input))
            {
                
                input = input.Trim();
                input = input.Replace(" ", "");
                input = input.Insert(3, " ").ToUpper();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool JHZipCodeValidation(ref string input)
        {
            Regex zipCodePattern = new Regex(@"^[0-9]{5}(?:-[0-9]{4})?$", RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            //need to remove any other things except numbers
            else if ((Regex.Replace(input, "[^0-9]*$", "")).Length != 5 && (Regex.Replace(input.ToString(), "[^0-9]*$", "")).Length != 9)
            {
                var test = Regex.Replace(input, "[^0-9]*$", "");
                return false;
            }
            else
            {
                if (zipCodePattern.IsMatch(Regex.Replace(input, "[^0-9]*$", "")))
                {
                    // make sure it is correct
                    input = Regex.Replace(input, "[^0-9]*$", "");
                    if (input.Length == 5)
                    {
                        input = input.Trim();
                    }
                    else if (input.Length == 9)
                    {
                        input = input.Trim();
                        input = input.Insert(5, "-");
                    }
                    else if (input.Length==10)
                    {
                        return true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool JHPhoneValidate(ref string input)
        {
            Regex phonePattern = new Regex(@"^[0-9]*$");
            if (!string.IsNullOrEmpty(input))
            {
                if (Regex.Replace(input, "[^0-9]", "").Length != 10)
                    return false;
                if (phonePattern.IsMatch(Regex.Replace(input, "[^0-9]", "")))
                {
                    input=Regex.Replace(input, "[^0-9]", "");
                    input = input.Insert(3, "-");
                    input = input.Insert(7, "-");
                    return true;
                }
                else
                { return false; }
            }
            return true;
        }

    }

}
