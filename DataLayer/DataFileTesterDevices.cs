using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFileTesterDevices : DataFileObjects<TesterDevice>
    {
        public DataFileTesterDevices(string i_FilePath) : base(i_FilePath)
        {
        }

        TesterDevice conversion(string[] i_LineParts)
        {
            return new TesterDevice(
                i_TesterId: int.Parse(i_LineParts[0]),
                i_DeviceId: int.Parse(i_LineParts[1]));
        }

        public async Task<List<TesterDevice>> GetItems()
        {
            return await getItems(conversion);
        }
    }
}
