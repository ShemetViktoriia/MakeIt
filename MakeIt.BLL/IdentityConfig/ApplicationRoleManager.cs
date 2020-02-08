using MakeIt.EF;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace MakeIt.BLL.IdentityConfig
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> store) : base(store) { }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            return base.CreateAsync(role);
        }

        public override Task<IdentityResult> UpdateAsync(Role role)
        {
            return base.UpdateAsync(role);
        }
    }
}
