using System;
using System.Threading;

namespace GoFish.Utils
{
    public class Terminal
    {
        private const string InvalidInputMessage = "Input was not valid";
        private const string InputNotAnInteger = "Input was not an integer";
        private const string InputNotADouble = "Input was not a double";
        private const string QuestionSeperator = " : ";

        private static readonly int DefaultDelay = 35;
        private static readonly int LineEndDelay = DefaultDelay * 5;
        private static readonly int Variation = 10;

        public static void TypeLine(string message)
        {
            Type(message);
            Thread.Sleep(LineEndDelay);
            Console.Write("\n");
        }

        public static void Type(string message)
        {
            Random random = new();
            foreach (char character in message)
            {
                Console.Write(character);
                Thread.Sleep(DefaultDelay + random.Next(DefaultDelay, DefaultDelay + Variation));
            }
        }

        public static string AskString(string question, string invalidMessage = InvalidInputMessage, Func<string, bool> validate = null)
        {
            Type(question + QuestionSeperator);
            return ReadString(invalidMessage, validate);
        }

        public static int AskInt(string question, string notAnIntegerMessage = InputNotAnInteger, string invalidMessage = InvalidInputMessage, Func<int, bool> validate = null)
        {
            Type(question + QuestionSeperator);
            return ReadInt(notAnIntegerMessage, invalidMessage, validate);
        }

        public static double AskDouble(string question, string notADoubleMessage = InputNotADouble, string invalidMessage = InvalidInputMessage, Func<double, bool> validate = null)
        {
            Type(question + QuestionSeperator);
            return ReadDouble(notADoubleMessage, invalidMessage, validate);
        }

        /// <summary>
        /// Allows you to read a string, and validate all in one go.
        /// This way, you don't need to write an extra loop to handle retries for bad data.
        /// </summary>
        /// <param name="invalidMessage">The message to write when the user doesn't provide valid input</param>
        /// <param name="validate">Optional validation that can be provided to ensure that data is valid</param>
        /// <returns></returns>
        public static string ReadString(string invalidMessage = InvalidInputMessage, Func<string, bool> validate = null)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var isValid = validate == null || validate(input);

                if (isValid)
                {
                    return input;
                }
                else
                {
                    TypeLine(invalidMessage);
                }
            }
        }

        /// <summary>
        /// Abstract away the difficulty of getting and parsing an integer value.
        /// </summary>
        /// <param name="invalidInputMessage">A message you can provide to give a custom error in the case of bad input</param>
        /// <param name="validate">A secondary validation that can be passed in to ensure that your input was in an acceptable range</param>
        /// <returns>The parsed int input</returns>
        public static int ReadInt(string notAnIntegerMessage = InputNotAnInteger, string invalidMessage = InvalidInputMessage, Func<int, bool> validate = null)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var parsed = int.TryParse(input, out var intResult);
                var isValid = parsed && (validate == null || validate(intResult));

                if (isValid)
                {
                    return intResult;
                }
                else
                {
                    if (!parsed)
                    {
                        TypeLine(notAnIntegerMessage);
                    }
                    else
                    {
                        TypeLine(invalidMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Abstract away the difficulty of getting and parsing an double value.
        /// </summary>
        /// <param name="invalidInputMessage">A message you can provide to give a custom error in the case of bad input</param>
        /// <param name="validate">A secondary validation that can be passed in to ensure that your input was in an acceptable range</param>
        /// <returns>The parsed double input</returns>
        public static double ReadDouble(string notADoubleMessage = InputNotADouble, string invalidMessage = null, Func<double, bool> validate = null)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var parsed = double.TryParse(input, out var doubleResult);
                var isValid = parsed && (validate == null || validate(doubleResult));

                if (isValid)
                {
                    return doubleResult;
                }
                else
                {
                    if (!parsed)
                    {
                        TypeLine(notADoubleMessage);
                    }
                    else
                    {
                        TypeLine(invalidMessage);
                    }
                }
            }
        }
    }
}
