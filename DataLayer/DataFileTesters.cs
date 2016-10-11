using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFileTesters : DataFileObjects<Tester>
    {
        public DataFileTesters(string i_FilePath) : base(i_FilePath)
        {
        }

        Tester conversion(string[] i_LineParts)
        {
            return new Tester(
                i_TesterId: int.Parse(i_LineParts[0]),
                i_FirstName: i_LineParts[1],
                i_LastName: i_LineParts[2],
                i_Country: i_LineParts[3],
                i_LastLogin: DateTime.Parse(i_LineParts[4]));
        }

        public async Task<List<Tester>> GetItems()
        {
            return await getItems(conversion);
        }
    }
}
