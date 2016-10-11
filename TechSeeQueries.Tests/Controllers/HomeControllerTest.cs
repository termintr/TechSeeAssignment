using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechSeeQueries;
using TechSeeQueries.Controllers;

namespace TechSeeQueries.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        string _testersFile = @"C:\Users\Roy\Desktop\MatchingAssignment\testers.csv";
        string _devicesFile = @"C:\Users\Roy\Desktop\MatchingAssignment\devices.csv";
        string _testerDevicesFile = @"C:\Users\Roy\Desktop\MatchingAssignment\tester_device.csv";
        string _bugsFile = @"C:\Users\Roy\Desktop\MatchingAssignment\bugs.csv";

        HomeController getController()
        {
            return new HomeController(
                i_TestersFilePath: _testersFile,
                i_DevicesFilePath: _devicesFile,
                i_BugsFilePath: _bugsFile,
                i_TesterDevicesFilePath: _testerDevicesFile);
        }

        [TestMethod()]
        public void GetCountriesTest()
        {
            Assert.AreNotEqual(0, (getController().GetCountries().Result.Count()));
        }

        [TestMethod()]
        public void GetDevicesTest()
        {
            Assert.AreNotEqual(0, getController().GetDevices().Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_All()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(null, null).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_InvalidCountry()
        {
            Assert.AreEqual(0, getController().MatchTesters(new List<string>() { "NoCountry" }, null).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_SingleCountryAllDevices()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(new List<string>() { "US" }, null).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_MultiCountryAllDevices()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(new List<string>() { "US", "JP",  }, null).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_AllCountryInvalidDevice()
        {
            Assert.AreEqual(0, getController().MatchTesters(null, new List<int>() { 3333 }).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_AllCountrySingleDevices()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(null, new List<int>() { 3 }).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_AllCountryMultiDevices()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(null, new List<int>() { 3, 4 }).Result.Count());
        }

        [TestMethod()]
        public void MatchTestersTest_MultiCountryMultiDevices()
        {
            Assert.AreNotEqual(0, getController().MatchTesters(new List<string>() { "US", "JP", }, new List<int>() { 3, 4 }).Result.Count());
        }
    }
}
