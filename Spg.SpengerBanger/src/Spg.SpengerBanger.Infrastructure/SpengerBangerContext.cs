using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.ModelConfiguration.Configurations;

namespace Spg.SpengerBanger.Infrastructure
{
    public class SpengerBangerContext : DbContext
    {
        public SpengerBangerContext()
        { }

        public SpengerBangerContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<User> Users => Set<User>();
        public DbSet<CatPriceType> CatPriceTypes => Set<CatPriceType>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=SpengerBanger.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(103647);
            List<CatPriceType> catPriceTypes = new List<CatPriceType>()
            {
                new CatPriceType(){Key="A", ShortName="Aktion", Description="Aktionspreis", ValidFrom= new DateTime(2021, 01, 01) },
                new CatPriceType(){Key="N", ShortName="Normal", Description="Noirmalpreis", ValidFrom= new DateTime(2021, 01, 01) },
            };
            CatPriceTypes.AddRange(catPriceTypes);
            SaveChanges();


            List<User> users = new Faker<User>("de").CustomInstantiator(f =>
            new User(Gender.NA, f.Name.FirstName(), f.Name.LastName(), f.Internet.Email(), Guid.NewGuid(), new Address(f.Address.StreetName(), f.Address.ZipCode(), f.Address.City())))
            .Generate(1000)
            .ToList();
            Users.AddRange(users);
            SaveChanges();


            List<Shop> shops = new Faker<Shop>("de").CustomInstantiator(f =>
            new Shop(f.Company.CompanySuffix(), f.Company.CompanyName(), f.Address.StreetAddress(), f.Company.CatchPhrase(), f.Company.Bs(), new Address(f.Address.StreetName(), f.Address.ZipCode(), f.Address.City()), Guid.NewGuid()))
            .Rules((f, s) =>
            {
                List<Category> categories = new Faker<Category>("de").CustomInstantiator(f =>
                new Category($"Kategorie {f.Random.Int(100, 999)}", s)) //f.Random.ListItem(f.Commerce.Categories)
                .Rules((f, c) =>
                {
                    c.Name = f.Random.ArrayElement(f.Commerce.Categories(10));
                    List<Product> products = new Faker<Product>("de").CustomInstantiator(f =>
                    new Product(f.Commerce.ProductName(), f.Commerce.ProductAdjective(), f.Commerce.ProductMaterial(), f.Commerce.Ean8(), f.Commerce.Ean13(), f.Random.Decimal(10, 1000), 20, f.Random.Int(10, 50), catPriceTypes[0], c))
                    .Rules((f, p) =>
                    {
                        DateTime? lastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
                        p.LastChangeDate = lastChangeDate;
                        if (lastChangeDate != null)
                        {
                            User owner = f.Random.ListItem(users);
                            p.LastChangeUser = owner;
                        }
                    })
                    .Generate(10);
                    c.AddProducts(products);
                    SaveChanges();
                })
                .Generate(5);
                s.AddCategories(categories);
                SaveChanges();
            })
            .Generate(5)
            .ToList();
            Shops.AddRange(shops);
            SaveChanges();


            Guid[] guids = new Guid[]
            {
                new Guid("582C3776-5726-4349-A6FE-D640671878AB"),
                new Guid("5835b046-106a-49f2-8f96-14c81015c54f"),
                new Guid("57cdf3c7-e7ee-46c3-87d5-3b3938af352e"),
                new Guid("7d11dc32-44c7-4c3a-9e5f-f44bebda5b29"),
                new Guid("69f86e01-39bb-4049-a4d5-af29ebef81e9"),
                new Guid("ac7bffd9-5223-4a73-b71f-f875f6d92835"),
                new Guid("15d62b7b-e1e5-44c1-b025-9fdd2c771b41"),
                new Guid("33e01151-c53e-4fa0-a643-84669884271b"),
                new Guid("a66b09b5-8d76-4bd1-bcbb-756736feae7a"),
                new Guid("4ccd1086-5d39-4a3a-883f-a9303e9da2e1"),
            };
            List<ShoppingCart> shoppingCarts = new Faker<ShoppingCart>("de").CustomInstantiator(f =>
            new ShoppingCart(f.Random.ListItem<Guid>(guids), f.Random.ListItem(users)))
            .Rules((f, s) =>
            {
                User owner = f.Random.ListItem(users);
                s.State = States.Active; // f.Random.Enum<States>();
                s.LastChangeDate = f.Date.Between(new DateTime(2021, 01, 01, 00, 00, 00), DateTime.Now);
                s.LastChangeUser = owner;
            })
            .Generate(10)
            .GroupBy(t => t.Guid)
            .Select(g => g.First())
            .ToList();
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();


            for (int i = 0; i <= 20; i++)
            {
                Product product = new Faker().Random.ListItem(Products.ToList());
                ShoppingCart shoppingCart = new Faker().Random.ListItem(ShoppingCarts.ToList());
                ShoppingCartItem shoppingCartItem = new ShoppingCartItem(name: product.Name, tax: product.Tax, nett: product.Nett, product: product, Guid.NewGuid());
                shoppingCart.AddItem(shoppingCartItem);
                SaveChanges();
            }
        }
    }
}
