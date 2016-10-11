using DataLayer;
using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechSeeQueries.Providers
{
    internal class DataProvider
    {
        DataFileTesters m_DataTesters;
        DataFileDevices m_DataDevices;
        DataFileBugs m_DataBugs;
        DataFileTesterDevices m_DataTesterDevices;

        public DataProvider(
            string i_TestersFilePath,
            string i_DevicesFilePath,
            string i_BugsFilePath,
            string i_TesterDevicesFilePath)
        {
            m_DataTesters = new DataFileTesters(i_TestersFilePath);
            m_DataDevices = new DataFileDevices(i_DevicesFilePath);
            m_DataBugs = new DataFileBugs(i_BugsFilePath);
            m_DataTesterDevices = new DataFileTesterDevices(i_TesterDevicesFilePath);
        }

        public async Task<List<Tester>> GetTesters()
        {
            return await m_DataTesters.GetItems();
        }

        public async Task<List<Device>> GetDevices()
        {
            return await m_DataDevices.GetItems();
        }

        public async Task<List<Bug>> GetBugs()
        {
            return await m_DataBugs.GetItems();
        }

        public async Task<List<TesterDevice>> GetTesterDevices()
        {
            return await m_DataTesterDevices.GetItems();
        }
    }
}
