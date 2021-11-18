using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Infrastructure
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
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();
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
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(c => c.UserNavigation)
                .WithMany(e => e.ShoppingCarts);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(103647);



            List<User> users = new Faker<User>().Rules((f, u) =>
            {
                string firstName = String.Empty;
                Gender gender = f.Random.Enum<Gender>(new Gender[] { Gender.NA });
                if (gender == Gender.FEMALE)
                {
                    firstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
                }
                else
                {
                    firstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                }
                u.FirstName = firstName;
                u.LastName = f.Name.LastName();
                u.EMail = f.Internet.Email();
                u.Gender = gender;
            })
            .Generate(1000)
            .ToList();
            Users.AddRange(users);
            SaveChanges();



            List<CatPriceType> catPriceTypes = new List<CatPriceType>()
            {
                new CatPriceType(){Key="A", ShortName="Aktion", Description="Aktionspreis", ValidFrom= new DateTime(2021, 01, 01) },
                new CatPriceType(){Key="N", ShortName="Normal", Description="Noirmalpreis", ValidFrom= new DateTime(2021, 01, 01) },
            };
            CatPriceTypes.AddRange(catPriceTypes);
            SaveChanges();



            List<Shop> shops = new Faker<Shop>().Rules((f, s) => 
            {
                s.Name = f.Company.CompanyName();
                s.Location = f.Address.StreetAddress();
                s.CompanySuffix = f.Company.CompanySuffix();
                s.CatchPhrase = f.Company.CatchPhrase();
                s.Bs = f.Company.Bs();
            })
            .Generate(5)
            .ToList();
            Shops.AddRange(shops);
            SaveChanges();



            List<Category> categories = new Faker<Category>().Rules((f, s) =>
            {

                s.Name = f.Random.ArrayElement(f.Commerce.Categories(10));

                s.ShopNavigation = f.Random.ListItem(shops);
            })
            .Generate(50)
            .ToList();
            Categories.AddRange(categories);
            SaveChanges();

            

            List<Product> products = new Faker<Product>().Rules((f, s) =>
            {
                s.CategoryNavigation= f.Random.ListItem(categories);
                s.Ean13 = f.Commerce.Ean13();
                s.Ean8 = f.Commerce.Ean8();
                s.Name = f.Commerce.ProductName();
                s.ProductAdjective = f.Commerce.ProductAdjective();
                s.ProductMaterial = f.Commerce.ProductMaterial();

                s.CategoryNavigation = f.Random.ListItem(categories);
                s.CatPriceTypeNavigation = catPriceTypes[0];
            })
            .Generate(500)
            .ToList();
            Products.AddRange(products);
            SaveChanges();



            List<ShoppingCart> shoppingCarts = new Faker<ShoppingCart>().Rules((f, s) => 
            {
                User owner = f.Random.ListItem(users);
                s.LastChangeDate = f.Date.Between(new DateTime(2021, 01, 01, 00, 00, 00), DateTime.Now);
                s.UserNavigation = owner;
                s.LastChangeUser = owner;
            })
            .Generate(10)
            .ToList();
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();


            List<ShoppingCartItem> shoppingCartItems = new Faker<ShoppingCartItem>().Rules((f, s) =>
            {
                Product product = f.Random.ListItem(products);

                s.Nett = product.Nett;
                s.Tax = product.Tax;
                s.ProductNavigation = product;
                s.Name = product.Name;

                s.ShoppingCartNavigation = f.Random.ListItem(shoppingCarts);
            })
            .Generate(40)
            .ToList();
            ShoppingCartItems.AddRange(shoppingCartItems);
            SaveChanges();
        }
    }
}
