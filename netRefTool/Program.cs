using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace netRefTool
{

    class Program
    {

        static string outFormat = "{0,-16}\t{1,-15}\t{2,-15}\t{3}";

        static void Main(string[] args)
        {
            var stuff = args.ToList();
            bool verbose = stuff.Contains("-v");
            //Check to see if you can type, this tells you if it's piped entry or not.
            if (Console.IsInputRedirected)
            {
                //Piped entry
                string entry = Console.In.ReadToEnd();
                var entries = entry.Split(Environment.NewLine.ToCharArray());
                args = entries;
            }
            stuff = args.ToList();

            if (verbose)
            {
                outFormat = "{0}";
                stuff.Remove("-v");
            }

            stuff.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            foreach (var item in stuff)
            {
                var assembly = LoadAssemblySafe(item);
                Console.Out.WriteLine("{1}References for: {0}", assembly.FullName,Environment.NewLine);
                if (!verbose)
                {
                    Console.Out.WriteLine();
                    Console.Out.WriteLine(outFormat, new string[] { "Assembly Name", "Version", "ProcessorArchitecture", "culture" });
                    Console.Out.WriteLine("".PadRight(80, '-'));

                }
                var referencedAssemblies = FindReferencedAssemblies(assembly);
                foreach (var referenced in referencedAssemblies)
                {
                    Console.Out.WriteLine(outFormat, new object[] { (verbose ? referenced.FullName : referenced.Name), referenced.Version, referenced.ProcessorArchitecture, referenced.CultureName });
                }
            }

        }

        private static Assembly LoadAssemblySafe(string filePath)
        {
            try
            {
                Assembly a = Assembly.LoadFile(filePath);
                return a;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("ERROR");
                Console.Out.WriteLine(ex.ToString());
                Environment.Exit(-1);
                return null;
            }
        }

        static AssemblyName[] FindReferencedAssemblies(Assembly assebmly)
        {
            AssemblyName[] names = assebmly.GetReferencedAssemblies();
            return names;
        }
    }
}
