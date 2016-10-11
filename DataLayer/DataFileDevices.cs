using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFileDevices : DataFileObjects<Device>
    {
        public DataFileDevices(string i_FilePath) : base(i_FilePath)
        {
        }

        Device conversion(string[] i_LineParts)
        {
            return new Device(
                i_DeviceId: int.Parse(i_LineParts[0]),
                i_Description: i_LineParts[1]);
        }

        public async Task<List<Device>> GetItems()
        {
            return await getItems(conversion);
        }
    }
}