using System;

namespace DataLayer.Models
{
    public class TesterDevice
    {
        int m_DeviceId;
        int m_TesterId;

        public TesterDevice(
            int i_DeviceId,
            int i_TesterId)
        {
            m_DeviceId = i_DeviceId;
            m_TesterId = i_TesterId;
        }

        public int TesterId
        {
            get { return m_TesterId; }
        }

        public int DeviceId
        {
            get { return m_DeviceId; }
        }
    }
}