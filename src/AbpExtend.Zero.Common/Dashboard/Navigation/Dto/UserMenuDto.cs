using System.Collections.Generic;
using Abp.Application.Navigation;
using Abp.AutoMapper;

namespace Boss.Scm.Dashboard.Navigation.Dto
{
    [AutoMapFrom(typeof(UserMenu))]
    public class UserMenuDto
    {
        public UserMenuDto()
        {
            Items = new List<UserMenuItemDto>();
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<UserMenuItemDto> Items { get; set; }
    }
}
