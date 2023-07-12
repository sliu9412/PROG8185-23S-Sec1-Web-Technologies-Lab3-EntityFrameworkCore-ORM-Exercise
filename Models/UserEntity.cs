namespace lab3.Models
{
    public class UserEntity
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public int status { get; set; }
        public ICollection<UserAddressEntity>? userAddressEntity { get; set; }
    }

    public class AddressEntity
    {
        public int id { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public ICollection<UserAddressEntity>? userAddressEntity { get; set; }
    }

    public class UserAddressEntity
    {
        public string? userId { get; set; }
        public UserEntity? user { get; set; }
        public int addressId { get; set; }
        public AddressEntity? address { get; set; }
    }
}