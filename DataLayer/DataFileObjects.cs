using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class DataFileObjects<T>
    {
        protected delegate T ConversionFunc(string[] i_LineParts);

        DataFile m_DataFile;
        List<T> m_Items;

        public DataFileObjects(string i_FilePath)
        {
            m_DataFile = new DataFile(i_FilePath);
        }

        #region updateItems
        protected async Task<List<T>> getItems(ConversionFunc i_ConvertionFunc)
        {
            await m_DataFile.UpdateFile();

            var linesContent = m_DataFile.FileLinesContent;
            int itemCount = linesContent.Count - 1;

            m_Items = new List<T>(itemCount);
            for (int i = 0; i < itemCount; i++)
            {
                m_Items.Add(i_ConvertionFunc(m_DataFile.FileLinesContent[i+1]));
            };

            return m_Items;
        }
        #endregion
    }
}
