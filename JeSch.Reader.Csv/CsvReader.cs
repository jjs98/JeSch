using System.Collections.Generic;
using System.IO;

namespace JeSch.Reader
{
    public class CsvReader
    {
        private readonly string _filePath;

        public CsvReader(string filePath)
        {
            _filePath = filePath;
        }

        #region Load

        public string[] LoadCsvAsArray() => LoadCsvAsList().ToArray();

        public List<string> LoadCsvAsList()
        {
            StreamReader streamReader = new StreamReader(_filePath);
            List<string> lines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                string Line = streamReader.ReadLine();
                lines.Add(Line);
            }

            streamReader.Close();

            return lines;
        }


        public object[] LoadCsvAsArrayInArray() => LoadCsvAsArrayInList().ToArray();

        public List<string[]> LoadCsvAsArrayInList()
        {
            StreamReader streamReader = new StreamReader(_filePath);
            List<string[]> lines = new List<string[]>();
            while (!streamReader.EndOfStream)
            {
                var Line = streamReader.ReadLine().Replace("\"", "").Split(',');
                lines.Add(Line);
            }

            streamReader.Close();

            return lines;
        }
        #endregion

        #region Save
        public bool SaveCsvFromList(List<string[]> lines) => SaveCsvFromArray(lines.ToArray());

        public bool SaveCsvFromArray(object[] lines)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(_filePath);
                if (lines[0] is object[])
                {
                    foreach (object[] line in lines)
                    {
                        var newLine = "";
                        foreach (string content in line)
                        {
                            newLine += $"\"{content}\"";
                        }
                        streamWriter.WriteLine(newLine.Replace("\"\"", "\",\""));
                    }
                }
                if(lines[0] is string)
                {
                    foreach (string line in lines)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
                streamWriter.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
