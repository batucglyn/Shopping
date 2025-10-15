using Identity.DTOs;
using Identity.Models;

namespace Identity.Mapper
{
    public static class AppUserMapper
    {



        public static ApplicationUser ToEntity(this RegisterDto registerDto)
        {
            return new ApplicationUser
            {

                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName=registerDto.FirstName+registerDto.LastName
            };
           


        }





    }
}
