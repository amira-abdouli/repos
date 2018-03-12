using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RolesLazyLoad.Models
{
    public class Roles
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

    }

    public class RoleGroups
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoleGroupsJoinRoles> RoleGroupsJoinRoles { get; set; }


    }
    public class RoleGroupsJoinRoles
    {
        [Key]
        [Column(Order = 1)]  
        public Guid RolesID { get; set; }
        public virtual Roles Roles { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid RoleGroupsID { get; set; }
        public virtual RoleGroups RoleGroups { get; set; }

    }

    public class RoleGroupsJoinUsers
    {
        [Key]
        [Column(Order = 1)]
        public Guid RoleGroupsID { get; set; }
        public virtual RoleGroups RoleGroups { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }


    }

}