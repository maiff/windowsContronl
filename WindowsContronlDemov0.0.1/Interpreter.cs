using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsContronlDemov0._0._1
{
    class Interpreter
    {
        private Dictionary<string, Delegate> dico = new Dictionary<string, Delegate>();

        public Interpreter()
        {
            dico["click"] = new Action<int, int>(Mouse.mockClick);
            dico["wheel"] = new Action<int>(Mouse.mockWheel);
        }

        public void interpreter(string text)
        {
            using (StringReader sr = new StringReader(text))
            {
                string line;
                int lineIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    
                    var val2 = System.Text.RegularExpressions.Regex.Split(line, @"\s{1,}");
                    if (checkScript(val2) == 0)
                    {
                        exec(val2);
                    }
                    Console.WriteLine("行{0}:{1}", ++lineIndex, line);
                    //System.Console.WriteLine(val2[0]);
                }
            }
        }

        private int checkScript(string [] scriptline)
        {
            switch (scriptline[0])
            {
                case "click":
                    return checkLength(scriptline, 3);
                case "wheel":
                    return checkLength(scriptline, 2);
                default:
                    return -1;
            }
        }

        private int checkLength(string[] scriptline, int len) { 
            if (scriptline.Length != len) return -1;
            return 0;
        }

        private void exec(string[] scriptline)
        {
            switch (scriptline[0])
            {
                case "click":
                    dico["click"].DynamicInvoke(Convert.ToInt32(scriptline[1]), Convert.ToInt32(scriptline[2]));
                    break;
                case "wheel":
                    dico["wheel"].DynamicInvoke(Convert.ToInt32(scriptline[1]));
                    break;
            }

        }


    }
}
