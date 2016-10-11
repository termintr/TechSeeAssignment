using System;

namespace DataLayer.Models
{
    public class Bug
    {
        int m_BugId;
        int m_DeviceId;
        int m_TesterId;

        public Bug(
            int i_BugId,
            int i_DeviceId,
            int i_TesterId)
        {
            m_BugId = i_BugId;
            m_DeviceId = i_DeviceId;
            m_TesterId = i_TesterId;
        }

        public int BugId
        {
            get { return m_BugId; }
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