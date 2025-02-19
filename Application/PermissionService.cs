using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions.ForRepositories;
using Core.Enums;
using DataAccess.Repository;

namespace Application
{
    public class PermissionService
    {
        private readonly IUserRepository _userRepository;
        public PermissionService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<HashSet<Permission>> GetPermissionsAsync(Guid id) 
        {
            return await _userRepository.GetUserPermissions(id);
        }
    }
}
