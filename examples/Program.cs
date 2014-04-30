using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace geoip_api_csharp2.examples
{
    class Program
    {
        
        static int Main(string[] args)
        {

            var options = new Options { };
            if (Parser.Default.ParseArgumentsStrict(args, options))
            {
                try
                {
                    var returnValue = ExecuteTest(args, options);
                    Console.WriteLine("Returned:  {0}", returnValue);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return (int)ExitCode.UnknownError;
                }

                return (int)ExitCode.Success;

            }
            return (int)ExitCode.UnknownError; // Default to not known error

        }

        private static object ExecuteTest(string[] args, Options options)
        {
            var type = Type.GetType(options.TestClassName);
            var method = type.GetMethod("Run");
            var paramValues = new object[] { args.Skip(2).ToArray() };
            
            var returnValue = method.Invoke(null, paramValues);

            return returnValue;
        }
    }

    class Options
    {
        [Option('t', Required = true, HelpText = "Test class name to be executed.")]
        public string TestClassName { get; set; }
        
        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText("GeoIp Example runner", "Copyright 2013", this);
            // ...
            if (this.LastParserState.Errors.Count > 0)
            {
                var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces
                if (!string.IsNullOrEmpty(errors))
                {
                    help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                    help.AddPreOptionsLine(errors);
                }
            }
            // ...
            return help.ToString();
        }
    }

    enum ExitCode {
      Success = 0,
      InvalidTestClassName = 1,
      UnknownError = 10
    }

}
