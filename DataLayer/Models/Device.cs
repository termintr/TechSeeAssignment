namespace DataLayer.Models
{
    public class Device
    {
        int m_DeviceId;
        string m_Description;

        public Device(
            int i_DeviceId,
            string i_Description)
        {
            m_DeviceId = i_DeviceId;
            m_Description = i_Description;
        }

        public int DeviceId
        {
            get { return m_DeviceId; }
        }

        public string Description
        {
            get { return m_Description; }
        }
    }
}