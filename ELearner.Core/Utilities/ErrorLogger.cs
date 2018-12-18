using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELearner.Core.Utilities {
    public class ErrorLogger {
        private static string pathlocal = @"C:\ElearnerFiles\";

        public void WriteLog(string[] lines, string filename) {
            string guid = Guid.NewGuid().ToString();
            filename += guid;
            filename += ".txt";
            //File.WriteAllLines(pathlocal + filename, lines);
        }
    }
}
