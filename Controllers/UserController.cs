using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab3.Data;
using lab3.Models;
using Microsoft.EntityFrameworkCore;

public class A
{
    public int id { get; set; }
    public string? street { get; set; }
    public string? city { get; set; }
    public string? country { get; set; }
}

public class U
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public int status { get; set; }
    public List<A> address { get; set; } = new List<A>();
}


namespace lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext DbContext;

        public UserController(DatabaseContext DbContext)
        {
            this.DbContext = DbContext;
        }

        [HttpGet("users_addresses")]
        public IActionResult GetUsersAddrs()
        {
            var users = DbContext.UserEntityMapping.Include(i => i.userAddressEntity).ThenInclude(i => i.address).ToList();
            // return Ok(users);

            var userList = new List<U>();

            foreach (UserEntity user in users)
            {
                var u = new U()
                {
                    id = user.id,
                    name = user.name,
                    email = user.email,
                    status = user.status
                };

                foreach (UserAddressEntity userAddress in user.userAddressEntity)
                {
                    u.address.Add(new A()
                    {
                        id = userAddress.address.id,
                        street = userAddress.address.street,
                        city = userAddress.address.city,
                        country = userAddress.address.country
                    });
                }

                userList.Add(u);
            }
            return Ok(userList);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(DbContext.UserAddressMapping.ToArray());
        }

        [HttpGet("adress")]
        public IActionResult GetAddr()
        {
            return Ok(DbContext.AddressMapping.ToArray());
        }

        [HttpGet("useraddress")]
        public IActionResult GetUserAddr()
        {
            return Ok(DbContext.UserAddressMapping.ToArray());
        }

        // Add new users and new addresses
        [HttpPost]
        public IActionResult AddNewUsersAndAddresses()
        {
            // Addresses
            var newAddr1 = new AddressEntity()
            {
                street = "1",
                city = "1",
                country = "1"
            };
            var newAddr2 = new AddressEntity()
            {
                street = "2",
                city = "2",
                country = "2"
            };
            // Users
            var newUser1 = new UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                name = "user1",
                email = "email1",
                status = 0,
            };
            var newUser2 = new UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                name = "user2",
                email = "email1",
                status = 0,
            };
            // Relations
            var newUserAddresses1 = new UserAddressEntity()
            {
                user = newUser1,
                address = newAddr1
            };
            var newUserAddresses2 = new UserAddressEntity()
            {
                user = newUser1,
                address = newAddr2
            };
            var newUserAddresses3 = new UserAddressEntity()
            {
                user = newUser2,
                address = newAddr1
            };
            var newUserAddresses4 = new UserAddressEntity()
            {
                user = newUser2,
                address = newAddr2
            };

            DbContext.AddRange(newUserAddresses1, newUserAddresses2, newUserAddresses3, newUserAddresses4);
            DbContext.SaveChanges();
            return Ok("done");
        }
    }
}