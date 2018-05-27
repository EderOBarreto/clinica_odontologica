using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public static class Conversor
    {
        public static string ConvertToPDF(byte[] file)
        {
            string spathfile = System.IO.Path.GetTempFileName();
            //move from soures to destination the extension until now is .temp
            System.IO.File.Move(spathfile, System.IO.Path.ChangeExtension(spathfile, ".pdf"));
            //make it real pdf file  extension .pdf
            spathfile = System.IO.Path.ChangeExtension(spathfile, ".pdf");
            System.IO.File.WriteAllBytes(spathfile, file);

            return spathfile;
        }

    }
}
