/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-10                    *
* Nowtime:           18:15:24                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Console
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    public class ConsoleImpl
    {
        internal int InfoCount { get; private set; }
        internal int WarnCount { get; private set; }
        internal int ErrorCount { get; private set; }

        private bool _InfoSelected;
        internal bool InfoSelected
        {
            get => _InfoSelected;
            set
            {
                _InfoSelected = value;
                Recalculate();
                if (_ConsoleDisplay != null)
                {
                    _ConsoleDisplay.Refresh();
                }
            }
        }

        private bool mWarnSelected;
        internal bool WarnSelected
        {
            get => mWarnSelected;
            set
            {
                mWarnSelected = value;
                Recalculate();
                if (_ConsoleDisplay != null)
                {
                    _ConsoleDisplay.Refresh();
                }
            }
        }

        private bool mErrorSelected;
        internal bool ErrorSelected
        {
            get => mErrorSelected;
            set
            {
                mErrorSelected = value;
                Recalculate();
                if (_ConsoleDisplay != null)
                {
                    _ConsoleDisplay.Refresh();
                }
            }
        }

        internal bool Enable { get; set; }
        private IConsoleDisplay _ConsoleDisplay = null;
        private readonly List<ConsoleLogEntry> _Logs = null;
        internal List<ConsoleLogEntry> ViewLogs { get; private set; }

        public readonly Dictionary<string, ConsoleCommandInfo> Commands = new Dictionary<string, ConsoleCommandInfo>();
        public readonly Dictionary<string, ConsoleCommandArg> Variables = new Dictionary<string, ConsoleCommandArg>();
        private readonly List<ConsoleCommandArg> _Arguments = new List<ConsoleCommandArg>(); // Cache for performance

        internal string IssuedErrorMessage { get; private set; }

        internal ConsoleImpl()
        {
            Enable = true;
            _Logs = new List<ConsoleLogEntry>(128);
            ViewLogs = new List<ConsoleLogEntry>(128);

            var rejected_commands = new Dictionary<string, ConsoleCommandInfo>();
            var method_flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var method in type.GetMethods(method_flags))
                {
                    var attribute = Attribute.GetCustomAttribute(
                        method, typeof(RegisterConsoleCommandAttribute)) as RegisterConsoleCommandAttribute;

                    if (attribute == null)
                    {
                        if (method.Name.StartsWith("FRONTCOMMAND", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Front-end Command methods don't implement RegisterCommand, use default attribute
                            attribute = new RegisterConsoleCommandAttribute();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var methods_params = method.GetParameters();

                    string command_name = InferFrontCommandName(method.Name);
                    Action<ConsoleCommandArg[]> proc;

                    if (attribute.Name == null)
                    {
                        // Use the method's name as the command's name
                        command_name = InferCommandName(command_name ?? method.Name);
                    }
                    else
                    {
                        command_name = attribute.Name;
                    }

                    if (methods_params.Length != 1 || methods_params[0].ParameterType != typeof(ConsoleCommandArg[]))
                    {
                        // Method does not match expected Action signature,
                        // this could be a command that has a FrontCommand method to handle its arguments.
                        rejected_commands.Add(command_name.ToUpper(), CommandFromParamInfo(methods_params, attribute.Help));
                        continue;
                    }

                    // Convert MethodInfo to Action.
                    // This is essentially allows us to store a reference to the method,
                    // which makes calling the method significantly more performant than using MethodInfo.Invoke().
                    proc = (Action<ConsoleCommandArg[]>)Delegate.CreateDelegate(typeof(Action<ConsoleCommandArg[]>), method);
                    AddCommand(command_name, proc, attribute.MinArgCount, attribute.MaxArgCount, attribute.Help, attribute.Hint);
                }
            }
            HandleRejectedCommands(rejected_commands);



            // 注册日志监听
            Application.logMessageReceivedThreaded -= ReceivedLog;
            Application.logMessageReceivedThreaded += ReceivedLog;
            AppDomain.CurrentDomain.UnhandledException += LogUnhandledException;
        }

        private void ReceivedLog(string logString, string stackTrace, LogType logType)
        {
            if (!Enable)
                return;
            if (logType == LogType.Exception)
            {
                logType = LogType.Error;
            }
            var log = new ConsoleLogEntry(logString, stackTrace, logType);
            _Logs.Add(log);

            AddViewLogs(log);

            if (_ConsoleDisplay != null)
            {
                _ConsoleDisplay.Refresh();
            }
        }

        private void LogUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.LogException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// 重新计算数据
        /// </summary>
        private void Recalculate()
        {
            InfoCount = 0;
            WarnCount = 0;
            ErrorCount = 0;

            ViewLogs.Clear();

            foreach (var item in _Logs)
            {
                AddViewLogs(item.Reset());
            }
        }

        private void AddViewLogs(ConsoleLogEntry log)
        {
            bool add = false;
            switch (log.LogType)
            {
                case LogType.Log:
                    InfoCount++;
                    add = _InfoSelected;
                    break;
                case LogType.Warning:
                    WarnCount++;
                    add = mWarnSelected;
                    break;
                case LogType.Error:
                    ErrorCount++;
                    add = mErrorSelected;
                    break;
            }

            if (add)
            {
                ViewLogs.Add(log);
            }
        }

        private string InferCommandName(string method_name)
        {
            string command_name;
            int index = method_name.IndexOf("COMMAND", StringComparison.CurrentCultureIgnoreCase);

            if (index >= 0)
            {
                // Method is prefixed, suffixed with, or contains "COMMAND".
                command_name = method_name.Remove(index, 7);
            }
            else
            {
                command_name = method_name;
            }

            return command_name;
        }

        private string InferFrontCommandName(string method_name)
        {
            int index = method_name.IndexOf("FRONT", StringComparison.CurrentCultureIgnoreCase);
            return index >= 0 ? method_name.Remove(index, 5) : null;
        }

        private void HandleRejectedCommands(Dictionary<string, ConsoleCommandInfo> rejected_commands)
        {
            foreach (var command in rejected_commands)
            {
                if (Commands.ContainsKey(command.Key))
                {
                    Commands[command.Key] = new ConsoleCommandInfo()
                    {
                        proc = Commands[command.Key].proc,
                        min_arg_count = command.Value.min_arg_count,
                        max_arg_count = command.Value.max_arg_count,
                        help = command.Value.help
                    };
                }
                else
                {
                    IssueErrorMessage("{0} is missing a front command.", command);
                }
            }
        }

        private ConsoleCommandInfo CommandFromParamInfo(ParameterInfo[] parameters, string help)
        {
            int optional_args = 0;

            foreach (var param in parameters)
            {
                if (param.IsOptional)
                {
                    optional_args += 1;
                }
            }

            return new ConsoleCommandInfo()
            {
                proc = null,
                min_arg_count = parameters.Length - optional_args,
                max_arg_count = parameters.Length,
                help = help
            };
        }

        private ConsoleCommandArg EatArgument(ref string s)
        {
            var arg = new ConsoleCommandArg();
            int space_index = s.IndexOf(' ');

            if (space_index >= 0)
            {
                arg.String = s.Substring(0, space_index);
                s = s.Substring(space_index + 1); // Remaining
            }
            else
            {
                arg.String = s;
                s = "";
            }

            return arg;
        }

        internal void SetDisplay(IConsoleDisplay display)
        {
            _ConsoleDisplay = display;
        }

        internal void Cleanup()
        {
            _Logs.Clear();
            Recalculate();
            if (_ConsoleDisplay != null)
            {
                _ConsoleDisplay.Refresh();
            }
        }

        internal void RunCommand(string line)
        {
            string remaining = line;
            IssuedErrorMessage = null;
            _Arguments.Clear();

            while (remaining != "")
            {
                var argument = EatArgument(ref remaining);

                if (argument.String != "")
                {
                    if (argument.String[0] == '$')
                    {
                        string variable_name = argument.String.Substring(1).ToUpper();

                        if (Variables.ContainsKey(variable_name))
                        {
                            // Replace variable argument if it's defined
                            argument = Variables[variable_name];
                        }
                    }
                    _Arguments.Add(argument);
                }
            }

            if (_Arguments.Count == 0)
            {
                // Nothing to run
                return;
            }

            string command_name = _Arguments[0].String.ToUpper();
            _Arguments.RemoveAt(0); // Remove command name from arguments

            if (!Commands.ContainsKey(command_name))
            {
                IssueErrorMessage("Command {0} could not be found", command_name);
                return;
            }

            RunCommand(command_name, _Arguments.ToArray());
        }

        internal void RunCommand(string command_name, ConsoleCommandArg[] arguments)
        {
            var command = Commands[command_name];
            int arg_count = arguments.Length;
            string error_message = null;
            int required_arg = 0;

            if (arg_count < command.min_arg_count)
            {
                if (command.min_arg_count == command.max_arg_count)
                {
                    error_message = "exactly";
                }
                else
                {
                    error_message = "at least";
                }
                required_arg = command.min_arg_count;
            }
            else if (command.max_arg_count > -1 && arg_count > command.max_arg_count)
            {
                // Do not check max allowed number of arguments if it is -1
                if (command.min_arg_count == command.max_arg_count)
                {
                    error_message = "exactly";
                }
                else
                {
                    error_message = "at most";
                }
                required_arg = command.max_arg_count;
            }

            if (error_message != null)
            {
                string plural_fix = required_arg == 1 ? "" : "s";

                IssueErrorMessage(
                    "{0} requires {1} {2} argument{3}",
                    command_name,
                    error_message,
                    required_arg,
                    plural_fix
                );

                if (command.hint != null)
                {
                    IssuedErrorMessage += string.Format("\n    -> Usage: {0}", command.hint);
                }

                return;
            }

            command.proc(arguments);
        }

        internal void AddCommand(string name, ConsoleCommandInfo info)
        {
            name = name.ToUpper();

            if (Commands.ContainsKey(name))
            {
                IssueErrorMessage("Command {0} is already defined.", name);
                return;
            }

            Commands.Add(name, info);
        }

        internal void AddCommand(string name, Action<ConsoleCommandArg[]> proc, int min_args = 0, int max_args = -1, string help = "", string hint = null)
        {
            var info = new ConsoleCommandInfo()
            {
                proc = proc,
                min_arg_count = min_args,
                max_arg_count = max_args,
                help = help,
                hint = hint
            };

            AddCommand(name, info);
        }

        internal void SetVariable(string name, string value)
        {
            SetVariable(name, new ConsoleCommandArg() { String = value });
        }

        internal void SetVariable(string name, ConsoleCommandArg value)
        {
            name = name.ToUpper();

            if (Variables.ContainsKey(name))
            {
                Variables[name] = value;
            }
            else
            {
                Variables.Add(name, value);
            }
        }

        internal ConsoleCommandArg GetVariable(string name)
        {
            name = name.ToUpper();

            if (Variables.ContainsKey(name))
            {
                return Variables[name];
            }

            IssueErrorMessage("No variable named {0}", name);
            return new ConsoleCommandArg();
        }

        internal void IssueErrorMessage(string format, params object[] message)
        {
            IssuedErrorMessage = string.Format(format, message);
        }
    }
}