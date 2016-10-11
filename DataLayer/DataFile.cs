using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class DataFile
    {
        string m_FilePath;
        DateTime? m_LastRead;
        List<string[]> m_FileLinesContent;

        public DataFile(string i_FilePath)
        {
            m_FilePath = i_FilePath;
            m_LastRead = null;
        }

        #region UpdateFile
        /// <summary>
        /// Update file content (lines) if changed since last check
        /// </summary>
        public async Task UpdateFile()
        {
            if (!File.Exists(m_FilePath))
                throw new FileNotFoundException(m_FilePath);

            DateTime lastFileWriteTime = File.GetLastWriteTimeUtc(m_FilePath);

            // If file has not changed, no need to update content
            if (m_LastRead.HasValue && lastFileWriteTime == m_LastRead.Value)
                return;

            // Read file content
            var fileContent = new List<string[]>();
            await Task.Run(() =>
            {
                using (var csvReader = new TextFieldParser(m_FilePath))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    while (!csvReader.EndOfData)
                    {
                        fileContent.Add(csvReader.ReadFields());
                    }
                }
            });

            if (fileContent.Count < 1)
                throw new ApplicationException("File content is invalid. Cannot be empty and must contain at least header row");

            // Save content and skip header row
            m_FileLinesContent = fileContent;

            // Update last read timestamp
            m_LastRead = lastFileWriteTime;
        }
        #endregion

        public List<string[]> FileLinesContent
        {
            get { return m_FileLinesContent; }
        }
    }
}
