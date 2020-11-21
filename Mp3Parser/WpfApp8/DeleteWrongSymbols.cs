using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8
{
    public static class DeleteWrongSymbols
    {
        static public string DeleteSymbols(string name)
        {
            string newname = "";
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '?' || name[i] == ':' || name[i] == '/' || name[i] == '<' || name[i] == '>' || name[i] == '*' || name[i] == '-')
                    newname += " ";
                else
                    newname += name[i];
            }
            return newname;
        }
    }
}
