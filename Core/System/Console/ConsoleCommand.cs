/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-10                    *
* Nowtime:           18:17:41                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Console
{
    using System;
    using System.Text;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Method)]
    public class RegisterConsoleCommandAttribute : Attribute
    {
        private int min_arg_count = 0;
        private int max_arg_count = -1;

        public int MinArgCount
        {
            get => min_arg_count;
            set => min_arg_count = value;
        }

        public int MaxArgCount
        {
            get => max_arg_count;
            set => max_arg_count = value;
        }

        public string Name { get; set; }
        public string Help { get; set; }
        public string Hint { get; set; }

        public RegisterConsoleCommandAttribute(string command_name = null)
        {
            Name = command_name;
        }
    }

    public struct ConsoleCommandInfo
    {
        public Action<ConsoleCommandArg[]> proc;
        public int max_arg_count;
        public int min_arg_count;
        public string help;
        public string hint;
    }

    public struct ConsoleCommandArg
    {
        public string String { get; set; }

        public int Int
        {
            get
            {
                if (int.TryParse(String, out int int_value))
                {
                    return int_value;
                }
                TypeError("int");
                return 0;
            }
        }

        public float Float
        {
            get
            {
                if (float.TryParse(String, out float float_value))
                {
                    return float_value;
                }
                TypeError("float");
                return 0;
            }
        }

        public bool Bool
        {
            get
            {
                if (string.Compare(String, "TRUE", ignoreCase: true) == 0)
                {
                    return true;
                }

                if (string.Compare(String, "FALSE", ignoreCase: true) == 0)
                {
                    return false;
                }

                TypeError("bool");
                return false;
            }
        }

        public override string ToString()
        {
            return String;
        }

        private void TypeError(string expected_type)
        {
            Console.Instance.IssueErrorMessage(
                "Incorrect type for {0}, expected <{1}>",
                String, expected_type
            );
        }
    }


    public static class BuiltinCommands
    {
        [RegisterConsoleCommand(Help = "Clear the command console", MaxArgCount = 0)]
        private static void CommandClear(ConsoleCommandArg[] args)
        {
            Console.Cleanup();
        }

        [RegisterConsoleCommand(Help = "Display help information about a command", MaxArgCount = 1)]
        private static void CommandHelp(ConsoleCommandArg[] args)
        {
            if (args.Length == 0)
            {
                foreach (var command in Console.Instance.Commands)
                {
                    Debug.Log($"{command.Key,-16}: {command.Value.help}");
                }
                return;
            }

            string command_name = args[0].String.ToUpper();

            if (!Console.Instance.Commands.ContainsKey(command_name))
            {
                Console.Instance.IssueErrorMessage("Command {0} could not be found.", command_name);
                return;
            }

            var info = Console.Instance.Commands[command_name];

            if (info.help == null)
            {
                Debug.Log($"{command_name} does not provide any help documentation.");
            }
            else if (info.hint == null)
            {
                Debug.Log(info.help);
            }
            else
            {
                Debug.Log($"{info.help}\nUsage: {info.hint}");
            }
        }

        [RegisterConsoleCommand(Help = "Time the execution of a command", MinArgCount = 1)]
        private static void CommandTime(ConsoleCommandArg[] args)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Console.Instance.RunCommand(JoinArguments(args));

            sw.Stop();
            Debug.Log($"Time: {(double)sw.ElapsedTicks / 10000}ms");
        }

        [RegisterConsoleCommand(Help = "Output message")]
        private static void CommandPrint(ConsoleCommandArg[] args)
        {
            Debug.Log(JoinArguments(args));
        }


        [RegisterConsoleCommand(Help = "List all variables or set a variable value")]
        private static void CommandSet(ConsoleCommandArg[] args)
        {
            if (args.Length == 0)
            {
                foreach (var kv in Console.Instance.Variables)
                {
                    Debug.Log($"{kv.Key,-16}: {kv.Value}");
                }
                return;
            }

            string variable_name = args[0].String;

            if (variable_name[0] == '$')
            {
                Debug.LogWarning($"Warning: Variable name starts with '$', '${variable_name}'.");
            }

            Console.Instance.SetVariable(variable_name, JoinArguments(args, 1));
        }

        [RegisterConsoleCommand(Help = "No operation")]
        private static void CommandNoop(ConsoleCommandArg[] args) { }

        [RegisterConsoleCommand(Help = "Quit running application", MaxArgCount = 0)]
        private static void CommandQuit(ConsoleCommandArg[] args)
        {
            Application.Quit();
        }

        private static string JoinArguments(ConsoleCommandArg[] args, int start = 0)
        {
            var sb = new StringBuilder();
            int arg_length = args.Length;

            for (int i = start; i < arg_length; i++)
            {
                sb.Append(args[i].String);

                if (i < arg_length - 1)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }
    }
}