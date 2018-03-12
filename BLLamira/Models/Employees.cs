using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesLazyLoad.Models
{
    public class Employees
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid TeamLeadersID { get; set; }
        public virtual TeamLeaders TeamLeaders { get; set; }
    }

    public class Teams
    {
        public Guid ID { get; set; }
        public string TeamName { get; set; }
        public virtual ICollection<TeamLeaders> TeamLeaders { get; set; }

    }
    public class TeamLeaders
    {
        public Guid ID { get; set; }
        public string TeamLeaderName { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public Guid TeamsID { get; set; }
        public virtual Teams Teams { get; set; }
    }

}