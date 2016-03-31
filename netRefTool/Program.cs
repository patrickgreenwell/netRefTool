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

            //Check to see if you can type, this tells you if it's piped entry or not.
            if (Console.IsInputRedirected)
            {
                //Piped entry
                var entry = Console.ReadLine();
                var entries = entry.Split(' ');
                args = entries;
            }
            var stuff = args.ToList();
            bool verbose = stuff.Contains("-v");
            if (verbose)
            {
                outFormat = "{0}";
                stuff.Remove("-v");
            }
            foreach (var item in stuff)
            {
                if (!verbose)
                {
                    Console.Out.WriteLine("{0}", item.Substring(item.LastIndexOf(@"\") + 1));
                    Console.Out.WriteLine();
                    Console.Out.WriteLine(outFormat, new string[] { "Assembly Name", "Version", "ProcessorArchitecture", "culture" });
                    Console.Out.Write("".PadRight(80, '-'));

                } var referencedAssemblies = FindReferencedAssemblies(item);
                foreach (var referenced in referencedAssemblies)
                {
                    Console.Out.WriteLine(outFormat, new object[] { (verbose ? referenced.FullName : referenced.Name), referenced.Version, referenced.ProcessorArchitecture, referenced.CultureName });
                }
            }

        }

        static AssemblyName[] FindReferencedAssemblies(string filePath)
        {
            try
            {
                Assembly a = Assembly.LoadFile(filePath);
                return FindReferencedAssemblies(a);
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
