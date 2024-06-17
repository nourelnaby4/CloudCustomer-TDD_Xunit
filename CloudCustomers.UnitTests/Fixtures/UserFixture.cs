namespace CloudCustomers.UnitTests.Fixtures;

public static class UserFixture
{
    public static List<User> GetUserTests() => new()
        {
            new User()
            {
                Id = 1,
                Name = "Ahmed",
                Email = "ahmed@gmail.com",
                Address = new Address
                {
                    Street = "123 kamel Soliman",
                    City = "Giza",
                    ZipCode = "11485"
                }
            },
            new User()
            {
                Id = 2,
                Name = "saed",
                Email = "saed@gmail.com",
                Address = new Address
                {
                    Street = "123 Ahmed Soliman",
                    City = "Cairo",
                    ZipCode = "11485"
                }
            },
            new User()
            {
                Id = 3,
                Name = "mohamed",
                Email = "mohamed@gmail.com",
                Address = new Address
                {
                    Street = "65 Ahmed Hosam",
                    City = "Alex",
                    ZipCode = "11485"
                }
            }
        };
}
