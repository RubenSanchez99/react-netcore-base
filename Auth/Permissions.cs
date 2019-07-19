using System;
using System.ComponentModel.DataAnnotations;

namespace ReactNetCoreBase.Auth
{
    public enum Permissions : short
    {
        NotSet = 0, //error condition

        //Here is an example of very detailed control over something
        [Display(GroupName = "Color", Name = "Read", Description = "Can read colors")]
        ColorRead = 0x10,
        [Display(GroupName = "Color", Name = "Create", Description = "Can create a color entry")]
        ColorCreate = 0x11,
        [Display(GroupName = "Color", Name = "Update", Description = "Can update a color entry")]
        ColorUpdate = 0x12,
        [Display(GroupName = "Color", Name = "Delete", Description = "Can delete a color entry")]
        ColorDelete = 0x13,

        [Display(GroupName = "UserAdmin", Name = "Read users", Description = "Can list User")]
        UserRead = 0x20,
        //This is an example of grouping multiple actions under one permission
        [Display(GroupName = "UserAdmin", Name = "Alter user", Description = "Can do anything to the User")]
        UserChange = 0x21,

        [Display(GroupName = "UserAdmin", Name = "Read Roles", Description = "Can list Role")]
        RoleRead = 0x28,
        [Display(GroupName = "UserAdmin", Name = "Change Role", Description = "Can create, update or delete a Role")]
        RoleChange = 0x29,
        
        //This is an example of what to do with permission you don't used anymore.
        //You don't want its number to be reused as it could cause problems 
        //Just mark it as obsolete and the PermissionDisplay code won't show it
        [Obsolete]
        [Display(GroupName = "Old", Name = "Not used", Description = "example of old permission")]
        OldPermissionNotUsed = 0x40,

        [Display(GroupName = "DataAuth", Name = "Not used", Description = "Permissions aren't used in DataAuthWebApp")]
        DataAuthPermission
    }
}