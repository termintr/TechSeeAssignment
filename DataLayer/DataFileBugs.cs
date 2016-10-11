using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFileBugs : DataFileObjects<Bug>
    {
        public DataFileBugs(string i_FilePath) : base(i_FilePath)
        {
        }

        Bug conversion(string[] i_LineParts)
        {
            return new Bug(
                i_BugId: int.Parse(i_LineParts[0]),
                i_DeviceId: int.Parse(i_LineParts[1]),
                i_TesterId: int.Parse(i_LineParts[2]));
        }

        public async Task<List<Bug>> GetItems()
        {
            return await getItems(conversion);
        }
    }
}
