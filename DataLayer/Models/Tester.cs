using System;

namespace DataLayer.Models
{
    public class Tester
    {
        int m_TesterId;
        string m_FirstName;
        string m_LastName;
        string m_Country;
        DateTime m_LastLogin;

        public Tester(
            int i_TesterId,
            string i_FirstName,
            string i_LastName,
            string i_Country,
            DateTime i_LastLogin)
        {
            m_TesterId = i_TesterId;
            m_FirstName = i_FirstName;
            m_LastName = i_LastName;
            m_Country = i_Country;
            m_LastLogin = i_LastLogin;
        }

        public int TesterId
        {
            get { return m_TesterId; }
        }

        public string FirstName
        {
            get { return m_FirstName; }
        }

        public string LastName
        {
            get { return m_LastName; }
        }

        public string Country
        {
            get { return m_Country; }
        }

        public DateTime LastLogin
        {
            get { return m_LastLogin; }
        }
    }
}