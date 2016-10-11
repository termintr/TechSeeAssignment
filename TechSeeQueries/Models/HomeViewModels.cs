using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TechSeeQueries.Models
{
    public class DeviceViewModel
    {
        int m_DeviceId;
        string m_Description;

        public DeviceViewModel(
            int i_DeviceId,
            string i_Description)
        {
            m_DeviceId = i_DeviceId;
            m_Description = i_Description;
        }

        [Required]
        [Display(Name = "Device Id")]
        public int DeviceId
        {
            get { return m_DeviceId; }
        }

        [Required]
        [Display(Name = "Description")]
        public string Description
        {
            get { return m_Description; }
        }
    }

    public class TesterViewModel
    {
        string m_FirstName;
        string m_LastName;
        int m_Experience;

        public TesterViewModel(
            string i_FirstName,
            string i_LastName,
            int i_Experience)
        {
            m_FirstName = i_FirstName;
            m_LastName = i_LastName;
            m_Experience = i_Experience;
        }

        [Required]
        [Display(Name = "Name")]
        public string Name
        {
            get { return string.Format("{0} {1}", m_FirstName, m_LastName); }
        }

        [Required]
        [Display(Name = "Experience")]
        public int Experience
        {
            get { return m_Experience; }
        }
    }
    
    public class HomeModel
    {
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Devices { get; set; }
        public List<TesterViewModel> Testers { get; set; }
    }
}