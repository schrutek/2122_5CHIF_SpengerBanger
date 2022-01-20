using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using Spg.SpangerBanger.TestModelConfiguration.Configurations;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Test.Mock
{
    public class SpengerBangerTestContext : SpengerBangerContext
    {
        public SpengerBangerTestContext()
        { }

        public SpengerBangerTestContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
        }

        public new void Seed()
        {
            List<CatPriceType> catPriceTypes = new List<CatPriceType>()
            {
                new CatPriceType(){Key="A", ShortName="Aktion", Description="Aktionspreis", ValidFrom= new DateTime(2021, 01, 01) },
                new CatPriceType(){Key="N", ShortName="Normal", Description="Noirmalpreis", ValidFrom= new DateTime(2021, 01, 01) },
            };
            CatPriceTypes.AddRange(catPriceTypes);
            SaveChanges();


            List<User> users = new List<User>()
            {
                new User(Gender.FEMALE, "Annelie", "Schlicht", "Levin_Hold@hotmail.com", new Guid("EC83E3C2-106C-4CB7-956D-E2B9F200D8B1"), new Address("Schlehdornstr.", "21839", "Lydiastadt")),
                new User(Gender.MALE, "Tamino", "Wachenbrunner", "Leif_Stang51@yahoo.com", new Guid("27E8B0F0-7AA4-42A7-B128-F09C4474AA76"), new Address("Brahmsweg", "13724", "Münchburg")),
                new User(Gender.FEMALE, "Jana", "Bruckmann", "Nadja.Hentschel@gmail.com", new Guid("AC008ABB-4B15-4353-83F3-30EE5B32E7D9"), new Address("Hütte", "18104", "Ehmanndorf")),
                new User(Gender.FEMALE, "Lias", "Unger", "Chantal.Siebert@gmail.com", new Guid("1735AA1A-C3AF-48B0-9041-30616FEE7B62"), new Address("Marie-Schlei-Str.", "49343", "Daudrichburg")),
                new User(Gender.MALE, "Rafael", "Beushausen", "Mathilda.Krebs@gmail.com", new Guid("91B7CB81-D4B4-4795-9351-B6619C3DA38B"), new Address("Rotdornweg", "1890", "Neu Jasminburg")),
                new User(Gender.MALE, "Emilian", "Gottschalk", "Tammo_Zimmer91@yahoo.com", new Guid("697C412D-20FC-4536-859F-16C658F08296"), new Address("Spitzwegstr.", "43944", "Ost Silasland")),
                new User(Gender.FEMALE, "Karoline", "Huke", "Evelyn52@yahoo.com", new Guid("0EF0DE0E-3DE4-4927-9435-734DC750A7FF"), new Address("Im Kirchfeld", "37690", "Louisascheid")),
                new User(Gender.MALE, "Franz", "Buschbaum", "Steven.Poeschl@hotmail.com", new Guid("B785DCF6-A198-42B8-8D08-DB6CBEA2E15B"), new Address("Loheweg", "34982", "Nord Katrinscheid")),
                new User(Gender.FEMALE, "Stella", "Janke", "Justus_Lichtl@hotmail.com", new Guid("86D35CED-4DFB-4A45-9917-7EE76B75075B"), new Address("Am Ehrenfriedhof", "49765", "West Aaliyahdorf")),
                new User(Gender.FEMALE, "Janine", "Esenwein", "Finn59@yahoo.com", new Guid("892CAB02-BAAE-44E0-AC5B-517295D1DE83"), new Address("Ulrichstr.", "44820", "Gabiusland")),
                new User(Gender.MALE, "Merve", "Keutel", "Zara.Seeger18@gmail.com", new Guid("6492BC3F-5CBE-4AD2-90A1-9A28590DB1C6"), new Address("Leimbacher Hof", "46978", "Süd Mohammeddorf")),
                new User(Gender.FEMALE, "Yvonne", "Uibel", "Omar55@hotmail.com", new Guid("EECEB50A-CAF4-4D49-B9FF-B52C7AE81715"), new Address("Linienstr.", "77264", "Ost Jasminedorf")),
                new User(Gender.FEMALE, "Defne", "Ahrens", "Adam47@yahoo.com", new Guid("7EEC5FB5-B480-4759-8164-98AB7BECED45"), new Address("Karl-Krekeler-Str.", "2549", "Bad Vin")),
                new User(Gender.FEMALE, "Fabienne", "Geisler", "Soeren_Ritschel@hotmail.com", new Guid("E8BB4B1F-FCCA-4A6F-B2BC-65D4477A1B5E"), new Address("Albert-Zarthe-Weg", "94653", "Alt Kjellland")),
                new User(Gender.FEMALE, "Rania", "Wilky", "Lewin_Goldkuehle62@gmail.com", new Guid("7A7F0A60-DE37-4A31-B9E7-24D713553178"), new Address("Bruchhauser Str.", "32556", "Neu Henrik")),
                new User(Gender.MALE, "Logan", "Wagner", "Dominik82@gmail.com", new Guid("5E8F7EB0-0678-46FD-855E-3130BDE55095"), new Address("Wolf-Vostell-Str.", "84779", "Süd Vitoburg")),
                new User(Gender.MALE, "Adriana", "Knippel", "Sandy_Grundmann84@hotmail.com", new Guid("BF0422BE-FC0B-4A60-B12E-FC81F872924E"), new Address("Weißdornweg", "88559", "Coraburg")),
                new User(Gender.FEMALE, "Sophie", "Wenk", "Nicole54@yahoo.com", new Guid("4E4FCA0D-77A9-4F62-9FCA-41F3472CAC0C"), new Address("Karl-Bosch-Str.", "24014", "Alt Malikstadt")),
                new User(Gender.MALE, "Boris", "Keil", "Valentino90@yahoo.com", new Guid("6D4F2916-CB0D-4D9E-87BF-5DAE416BEADF"), new Address("Wiehbachtal", "17446", "Abdulstadt")),
                new User(Gender.FEMALE, "Annemarie", "Frosch", "Juliane25@hotmail.com", new Guid("0E4E1C16-5D05-4AE9-9725-ACF0478D2372"), new Address("Im Dorf", "73486", "Annelieland")),
            };
            Users.AddRange(users);
            SaveChanges();


            Shop shop01 = new Shop("KG", "Huber - Savoia", "Kinderhausen 48c", "Switchable 3rd generation hierarchy", "expedite dot-com web-readiness", new Address("Am Steinberg", "35325", "Piaburg"), new Guid("8930863F-0B71-444D-9ACF-32B58AEC09EE"));
            Category category01 = shop01.AddCategory(new Category("Kategorie 100", shop01));
            shop01.Categories[0].AddProduct(new Product("Intelligent Wooden Chips", "Handmade", "Cotton", "64508375", "5329128197047", 147.9939390150805M, 20, 10, catPriceTypes[0], category01));
            shop01.Categories[0].AddProduct(new Product("Gorgeous Rubber Chips", "Incredible", "Cotton", "70155785", "1376055562923", 913.8675093249736M, 20, 10, catPriceTypes[0], category01));
            shop01.Categories[0].AddProduct(new Product("Rustic Granite Soap", "Handmade", "Metal", "32590388", "3035499593457", 172.62735638424121M, 20, 10, catPriceTypes[0], category01));
            Category category02 = shop01.AddCategory(new Category("Kategorie 101", shop01));
            shop01.Categories[1].AddProduct(new Product("Ergonomic Concrete Cheese", "Tasty", "Concrete", "62684705", "7858105292911", 683.17250477763484M, 20, 10, catPriceTypes[0], category02));
            shop01.Categories[1].AddProduct(new Product("Small Plastic Towels", "Practical", "Metal", "4654346", "2811939492990", 504.93540056745268M, 20, 10, catPriceTypes[0], category02));
            shop01.Categories[1].AddProduct(new Product("Incredible Rubber Gloves", "Generic", "Wooden", "45583360", "3826709007228", 671.3979843824155M, 20, 10, catPriceTypes[0], category02));
            Category category03 = shop01.AddCategory(new Category("Kategorie 102", shop01));
            shop01.Categories[2].AddProduct(new Product("Gorgeous Rubber Gloves", "Generic", "Steel", "20092245", "875309957122", 422.5006509770179M, 20, 10, catPriceTypes[0], category03));
            shop01.Categories[2].AddProduct(new Product("Sleek Concrete Towels", "Small", "Soft", "28445241", "1781356287602", 698.2285168386199M, 20, 10, catPriceTypes[0], category03));
            shop01.Categories[2].AddProduct(new Product("Sleek Granite Car", "Intelligent", "Cotton", "62576161", "8031785888080", 901.91402461003249M, 20, 10, catPriceTypes[0], category03));
            Shop shop02 = new Shop("GmbH & Co. KG", "Krause - Holtfreter", "Friedensstr. 33c", "Secured optimizing strategy", "reinvent scalable technologies", new Address("Schwarzastr.", "25692", "Süd Pepescheid"), new Guid("65E0CEAB-B71D-4CB6-AC0E-F1C91B4D78C5"));
            Category category04 = shop02.AddCategory(new Category("Kategorie 103", shop02));
            shop02.Categories[0].AddProduct(new Product("Handcrafted Plastic Cheese", "Incredible", "Steel", "89676608", "1040512967513", 97.362905236595758M, 20, 10, catPriceTypes[0], category04));
            shop02.Categories[0].AddProduct(new Product("Incredible Fresh Mouse", "Fantastic", "Rubber", "95185231", "4787522388606", 616.31324252873308M, 20, 10, catPriceTypes[0], category04));
            shop02.Categories[0].AddProduct(new Product("Tasty Fresh Tuna", "Gorgeous", "Rubber", "44294243", "2522088488757", 827.52775786329472M, 20, 10, catPriceTypes[0], category04));
            Category category05 = shop02.AddCategory(new Category("Kategorie 104", shop02));
            shop02.Categories[1].AddProduct(new Product("Unbranded Steel Shoes", "Tasty", "Plastic", "38075568", "1793100443776", 690.79504880625535M, 20, 10, catPriceTypes[0], category05));
            shop02.Categories[1].AddProduct(new Product("Practical Frozen Sausages", "Tasty", "Metal", "29339426", "120959023421", 196.46139902363578M, 20, 10, catPriceTypes[0], category05));
            shop02.Categories[1].AddProduct(new Product("Sleek Plastic Chicken", "Gorgeous", "Steel", "85586697", "9760167420684", 119.71450943486506M, 20, 10, catPriceTypes[0], category05));
            Category category06 = shop02.AddCategory(new Category("Kategorie 105", shop02));
            shop02.Categories[2].AddProduct(new Product("Practical Rubber Table", "Practical", "Plastic", "60363268", "4404581646545", 943.30504396152892M, 20, 10, catPriceTypes[0], category06));
            shop02.Categories[2].AddProduct(new Product("Tasty Plastic Towels", "Incredible", "Granite", "23066250", "5302610329041", 879.44758506931699M, 20, 10, catPriceTypes[0], category06));
            shop02.Categories[2].AddProduct(new Product("Handmade Steel Chair", "Sleek", "Granite", "30000735", "447326333823", 849.25505662302304M, 20, 10, catPriceTypes[0], category06));
            Shop shop03 = new Shop("AG", "Koob OHG", "Dornierstr. 67a", "Centralized background definition", "optimize mission-critical systems", new Address("Otto-Wels-Str.", "85216", "Kastnerland"), new Guid("39010EEB-C173-429C-A580-0A988D2C2603"));
            Category category07 = shop03.AddCategory(new Category("Kategorie 106", shop03));
            shop03.Categories[0].AddProduct(new Product("Handcrafted Concrete Salad", "Ergonomic", "Granite", "15021281", "1105998018835", 901.50926296669534M, 20, 10, catPriceTypes[0], category07));
            shop03.Categories[0].AddProduct(new Product("Gorgeous Plastic Mouse", "Small", "Rubber", "96661659", "3322985944702", 770.07891350429434M, 20, 10, catPriceTypes[0], category07));
            shop03.Categories[0].AddProduct(new Product("Fantastic Rubber Mouse", "Fantastic", "Wooden", "97991311", "4498337755308", 608.13466600986901M, 20, 10, catPriceTypes[0], category07));
            Category category08 = shop03.AddCategory(new Category("Kategorie 107", shop03));
            shop03.Categories[1].AddProduct(new Product("Practical Rubber Gloves", "Gorgeous", "Soft", "53495976", "4992711716942", 846.54566576543512M, 20, 10, catPriceTypes[0], category08));
            shop03.Categories[1].AddProduct(new Product("Generic Concrete Pizza", "Refined", "Wooden", "92123649", "2522538926488", 804.73296359401813M, 20, 10, catPriceTypes[0], category08));
            shop03.Categories[1].AddProduct(new Product("Licensed Rubber Salad", "Practical", "Fresh", "13867164", "5779457553784", 470.18018300187736M, 20, 10, catPriceTypes[0], category08));
            Category category09 = shop03.AddCategory(new Category("Kategorie 108", shop03));
            shop03.Categories[2].AddProduct(new Product("Unbranded Metal Tuna", "Awesome", "Granite", "73246923", "863809755881", 741.39815907059176M, 20, 10, catPriceTypes[0], category09));
            shop03.Categories[2].AddProduct(new Product("Intelligent Concrete Soap", "Awesome", "Fresh", "36971374", "7333869711736", 934.41578171421556M, 20, 10, catPriceTypes[0], category09));
            shop03.Categories[2].AddProduct(new Product("Licensed Fresh Bacon", "Fantastic", "Soft", "59203476", "2767620214900", 148.12218498351196M, 20, 10, catPriceTypes[0], category09));
            List<Shop> shops = new List<Shop>() { shop01, shop02, shop03 };
            Shops.AddRange(shops);
            SaveChanges();


            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>()
            {
                new ShoppingCart(new Guid("582C3776-5726-4349-A6FE-D640671878AB"), users[0]),
                new ShoppingCart(new Guid("5835b046-106a-49f2-8f96-14c81015c54f"), users[3]),
                new ShoppingCart(new Guid("57cdf3c7-e7ee-46c3-87d5-3b3938af352e"), users[8]),
                new ShoppingCart(new Guid("7d11dc32-44c7-4c3a-9e5f-f44bebda5b29"), users[11]),
                new ShoppingCart(new Guid("69f86e01-39bb-4049-a4d5-af29ebef81e9"), users[1]),
                new ShoppingCart(new Guid("ac7bffd9-5223-4a73-b71f-f875f6d92835"), users[2]),
                new ShoppingCart(new Guid("15d62b7b-e1e5-44c1-b025-9fdd2c771b41"), users[7]),
                new ShoppingCart(new Guid("33e01151-c53e-4fa0-a643-84669884271b"), users[4]),
                new ShoppingCart(new Guid("a66b09b5-8d76-4bd1-bcbb-756736feae7a"), users[15]),
                new ShoppingCart(new Guid("4ccd1086-5d39-4a3a-883f-a9303e9da2e1"), users[6]),
            };
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();
        }
    }
}
