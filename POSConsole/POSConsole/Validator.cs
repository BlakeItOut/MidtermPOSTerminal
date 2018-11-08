using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSConsole
{
    public class Validator
    {
        public static string promptUser(string message, Func<string, bool> condition)
        {
            Console.Write(message);
            string textValue = Console.ReadLine();
            if (condition(textValue))
            {
                return textValue;
            }
            else
            {
                Console.WriteLine("This is not a valid input.");
                return promptUser(message, condition);
            }
        }

        public static string promptUser(string message, Func<string, bool> condition, string response, int counter)
        {
            Console.Write(message);
            string textValue = response;
            if (condition(textValue))
            {
                return textValue;
            }
            else if (counter == 10)
            {
                return null;
            }
            else
            {
                Console.WriteLine("This is not a valid input.");
                return promptUser(message, condition, response, ++counter);
            }
        }

        public static bool promptYN(string message)
        {
            return promptUser(message, (response => response.ToLower()[0] == 'y' || response.ToLower()[0] == 'n')).ToLower()[0] == 'y';
        }

        public static bool promptYN(string message, string textValue)
        {
            return promptUser(message, (response => response.ToLower()[0] == 'y' || response.ToLower()[0] == 'n'), textValue, 0).ToLower()[0] == 'y';
        }
    }
}
