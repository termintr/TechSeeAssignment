using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using TechSeeQueries.Models;
using TechSeeQueries.Providers;

namespace TechSeeQueries.Controllers
{
    public class HomeController : Controller
    {
        DataProvider m_DataProvider;

        public HomeController() : this(
            i_TestersFilePath: HostingEnvironment.MapPath(ConfigurationManager.AppSettings["TestersFilePath"]),
            i_DevicesFilePath: HostingEnvironment.MapPath(ConfigurationManager.AppSettings["DevicesFilePath"]),
            i_BugsFilePath: HostingEnvironment.MapPath(ConfigurationManager.AppSettings["BugsFilePath"]),
            i_TesterDevicesFilePath: HostingEnvironment.MapPath(ConfigurationManager.AppSettings["TesterDevicesFilePath"]))
        {
        }

        public HomeController(
            string i_TestersFilePath,
            string i_DevicesFilePath,
            string i_BugsFilePath,
            string i_TesterDevicesFilePath)
        {
            m_DataProvider = new DataProvider(
                i_TestersFilePath: i_TestersFilePath,
                i_DevicesFilePath: i_DevicesFilePath,
                i_BugsFilePath: i_BugsFilePath,
                i_TesterDevicesFilePath: i_TesterDevicesFilePath);
        }

        public async Task<ActionResult> Index()
        {
            return View(new HomeModel()
            {
                Countries = (await GetCountries()).Select(countryName => new SelectListItem { Text = countryName, Value = countryName }).ToList(),

                Devices = (await GetDevices()).OrderBy(d => d.Description).Select(device => new SelectListItem
                {
                    Value = device.DeviceId.ToString(),
                    Text = device.Description
                }).ToList(),

                Testers = null
            });
        }

        [HttpPost]
        public async Task<ActionResult> Index(HomeModel i_HomeModel)
        {
            var countries = i_HomeModel.Countries.FindAll(country => country.Selected).Select(country => country.Value).ToList();
            var deviceIds = i_HomeModel.Devices.FindAll(device => device.Selected).Select(device => int.Parse(device.Value)).ToList();

            // If all countries/devices selected, pass null (acts as "ALL" requirement in the exercise)
            i_HomeModel.Testers = (await MatchTesters(
                i_Countries: countries.Count == i_HomeModel.Countries.Count ? null : countries,
                i_DeviceIds: deviceIds.Count == i_HomeModel.Devices.Count ? null : deviceIds)).ToList();
            return View(i_HomeModel);
        }


        public async Task<IEnumerable<string>> GetCountries()
        {
            var result = await m_DataProvider.GetTesters();
            return result.Select(tester => tester.Country).Distinct();
        }

        public async Task<IEnumerable<DeviceViewModel>> GetDevices()
        {
            var devices = await m_DataProvider.GetDevices();
            var result = new List<DeviceViewModel>(devices.Count);
            devices.ForEach(device => result.Add(new DeviceViewModel(i_DeviceId: device.DeviceId, i_Description: device.Description)));
            return result.AsEnumerable();
        }
        
        /// <summary>
        /// Find testers matching creteria:
        ///     If i_Countries not null => return only testers who are in any of the given countries
        ///
        ///     Return the tester sorted by experience. Experience is the total number of bugs opened on all given devices
        ///     (if i_DeviceIds is null => use return all devices)
        /// </summary>
        /// <returns>Matched testers, sorted by experience</returns>
        public async Task<IEnumerable<TesterViewModel>> MatchTesters(List<string> i_Countries, List<int> i_DeviceIds)
        {
            var allTesters = await m_DataProvider.GetTesters();
            var testerDevices = await m_DataProvider.GetTesterDevices();
            var bugs = await m_DataProvider.GetBugs();

            // If used, filter testers who reside at one of the given countries
            var testersCountryFiltered = i_Countries == null ? allTesters : allTesters.FindAll(tester => i_Countries.Contains(tester.Country));

            // If used, Filter users who own at least one of the devices

            // Filter those who have at least one device, or matching given device Ids
            var testersCountryAndDeviceFiltered = testersCountryFiltered.FindAll(tester =>
                    testerDevices.Any(testerDevice =>
                        (testerDevice.TesterId == tester.TesterId) &&
                        (i_DeviceIds == null ? true : i_DeviceIds.Contains(testerDevice.DeviceId))));

            // Convert to view-model tester
            var matches = new List<TesterViewModel>();
            testersCountryAndDeviceFiltered.ForEach(tester => matches.Add(new TesterViewModel(
                i_FirstName: tester.FirstName,
                i_LastName: tester.LastName,
                i_Experience: calcTesterExperience(bugs, tester.TesterId, i_DeviceIds))));

            // Order result by decending experience
            return matches.OrderByDescending(match => match.Experience);
        }

        int calcTesterExperience(
            List<DataLayer.Models.Bug> i_Bugs,
            int i_TesterId,
            List<int> i_DeviceIds)
        {
            return i_Bugs.FindAll(
                bug => bug.TesterId == i_TesterId &&
                (i_DeviceIds == null ? true : i_DeviceIds.Contains(bug.DeviceId))
            ).Count();
        }
    }
}