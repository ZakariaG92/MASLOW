using MASLOW.Entities.Privileges;
using MASLOW.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities
{
    public interface IActionnable
    {
        public IEnumerable<string> Actions { get; }

        public IEnumerable<GroupPrivilege> GroupPrivileges { get; set; }
        public IEnumerable<UserPrivilege> UserPrivileges { get; set; }

        public sealed bool CheckPrivileges(string action, User user)
        {
            //TODO : Check if is admin

            //Check user privileges
            var userPrivilges = UserPrivileges.Where(privilege => 
                                                        (privilege.Action == action || privilege.Action == null) 
                                                        && privilege.IsUserConserned(user))
                .ToList();

            bool? userAutorization = null;
            foreach (var privilege in userPrivilges)
            {
                if (privilege.GetActiveStatus())
                {
                    switch (privilege.Mode)
                    {
                        case PrivilegeMode.ALLOW:
                            userAutorization = userAutorization != false ? true : false;
                            break;
                        case PrivilegeMode.DENY:
                            userAutorization = false;
                            break;
                    }
                }
            }

            if(userAutorization != null)
            {
                return userAutorization.Value;
            }

            //Check group privileges
            var userGroupPriliveges = GroupPrivileges.Where(privilege =>
                                                    (privilege.Action == action || privilege.Action == null)
                                                    && privilege.IsUserConserned(user))
            .ToList();

            bool? groupAutorization = null;
            foreach(var privilege in userGroupPriliveges)
            {
                if (privilege.GetActiveStatus())
                {
                    switch (privilege.Mode)
                    {
                        case PrivilegeMode.ALLOW:
                            groupAutorization = groupAutorization != false ? true : false;
                            break;
                        case PrivilegeMode.DENY:
                            groupAutorization = false;
                            break;
                    }
                }
            }

            return groupAutorization != null ? groupAutorization.Value : false;
        }

        public sealed bool DoActionWithPrivileges(string action, User user)
        {
            return CheckPrivileges(action, user) ? DoAction(action, user) : false;
        }

        internal bool DoAction(string action, User user);
    }
}
