using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataLayer.Tests
{
    [TestClass()]
    public class DataFileTests
    {
        string _testersFile = @"C:\Users\Roy\Desktop\MatchingAssignment\testers.csv";
        string _devicesFile = @"C:\Users\Roy\Desktop\MatchingAssignment\devices.csv";
        string _testerDevicesFile = @"C:\Users\Roy\Desktop\MatchingAssignment\tester_device.csv";
        string _bugsFile = @"C:\Users\Roy\Desktop\MatchingAssignment\bugs.csv";
        
        [TestMethod()]
        public void ReadDevicesTest()
        {
            try
            {
                Assert.AreNotEqual(0, new DataFileDevices(_devicesFile).GetItems().Result.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void ReadTestersTest()
        {
            try
            {
                Assert.AreNotEqual(0, new DataFileTesters(_testersFile).GetItems().Result.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void ReadTesterDevicesTest()
        {
            try
            {
                Assert.AreNotEqual(0, new DataFileTesterDevices(_testerDevicesFile).GetItems().Result.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void ReadBugsTest()
        {
            try
            {
                Assert.AreNotEqual(0, new DataFileBugs(_bugsFile).GetItems().Result.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}