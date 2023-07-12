# EntityFrameworkCore’ [one to one], [one to many] and [many to many] relationship

This article is based on the [https://sliu9412.notion.site/Create-ASP-NET-API-App-29abdc4cd5184e91a387a102d6f86a23?pvs=4](https://www.notion.so/Create-ASP-NET-API-App-29abdc4cd5184e91a387a102d6f86a23?pvs=21). To make the testing easier, I have already make the dbservice as in-meory database.

## Pre-work

1. duplicate the lab3’s project:  https://github.com/sliu9412/Lab3_Asp.net_CRUD_User_API
2. Modify the database service to in-memory database.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled.png)

## One To One Relationship

**Assume one user only has one address.**

1. Prepare the AddressEntity class to store the infomation, its id should become the foreign key of the userEntity

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%201.png)

1. Add a new property in the UserEntity class, and its data type should be the AddressEntity. 

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%202.png)

1. Like the UserEntity class, the Address class also needs to be a property inside the ORM class

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%203.png)

1. When create a new user, the addtional address field should alse be filled, it’s already an instance of the AddressEntity class.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%204.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%205.png)

1. The point is retrieving data, if using the previous code directly, the address attribute of returned json will be null. 

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%206.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%207.png)

1. To address this problem, using .Include() method as the screenshot below.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%208.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%209.png)

## One To Many Relationship

**Assume one user has mutiple addresses.**

1. The majority steps of one to many are almost same as the one to one relationship, the point is changing the one to one’s address’ data type from AddressEntity to ICollection<AddressEntity>. (This example code also changes the property name from address to addresses)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2010.png)

1. Modify the property name at the controller class

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2011.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2012.png)

1. In the Swagger api testing class, the default format of the address has vonvert from a key-value pair to an array. Test the new user with only one address.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2013.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2014.png)

1. Test the situation with multiple addresses. It works as expection

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2015.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2016.png)

## Many To Many Relationship

**Assume one user has mutiple addresses, and the addresses can be used by other users.**

1. Many to Many Relationship is much more complicated than the previous relationships above. Like the ralation table, there must be a class to be the bridge for the many to many field. Prepare it as the screenshot below.
   
    This Entity class’s propertis consist of the UserEntity and AddressEntity class’s instance.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2017.png)

1. Replace the address property of UserEntity class and user property of AddressEntity class to the bridge class respectively.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2018.png)

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2019.png)

1. In the Mapping class, there’s OnModelCreating method to bind the Many to Many relationship.
- one user has many userAddressEntity, its foreign key is userId
- one address has many userAddressEntity, its foreign key is addressId

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2020.png)

1. Set the properties mapping

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2021.png)

1. To test the many to many relationship, This project has prepared 5 apis.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2022.png)

code

```csharp
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
```

1. The Post api will create two users and two addresses, every user has two addresses.
- Addresses

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2023.png)

- Users

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2024.png)

- Bridge class’s instance, to store the instance of user and address. Like the records in database, there should be 4 records.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2025.png)

1. Because the user and address’s instances are already in the bridge entity class’s instance, save these four instance is enough. *Do not make the user or address as response, it will throw error message.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2026.png)

1. Test the result from user, address and bridge class’s apis.
- user: the same id has two addresses

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2027.png)

- address: two addresses as expection

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2028.png)

- brigeclass, four records as expection

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2029.png)

1. However, if there’s an api, it needs to display all the users, and every user will display their addresses inside a list, it can not use Include and ThenInClude method, then use ToList() directly.
   
    If return the query, it will throw errow message. 

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2030.png)

1. To address the problem, it needs to prepare the one to many class for user, because this api is used to tranverse all the users, and every user has many address.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2031.png)

1. Prepare an empty list to store the user instance (the new one, not the entity class), then tranverse all the user, every user will also has multiple addresses, put them into the new user instance’s address list.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2032.png)

1. The json is as the screenshot shown.

![Untitled](EntityFrameworkCore%E2%80%99%20%5Bone%20to%20one%5D,%20%5Bone%20to%20many%5D%20a%206c5c896bf6a44aab90d4a6ff5fc71135/Untitled%2033.png)