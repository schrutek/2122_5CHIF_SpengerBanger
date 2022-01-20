namespace Spg.SpengerBanger.Domain.Model
{
    public class Product : EntityBase
    {
        protected Product() { }
        public Product(string name, string productAdjective, string productMaterial, string ean8, string ean13, decimal nett, int tax, int stock, CatPriceType catPriceType, Category categoryNavigation)
        {
            Name = name;
            ProductAdjective = productAdjective;
            ProductMaterial = productMaterial;
            Ean8 = ean8;
            Ean13 = ean13;
            Nett = nett;
            Tax = tax;
            Stock = stock;
            CatPriceTypeId = catPriceType.Id;
            CatPriceTypeNavigation = catPriceType;
            CategoryId = categoryNavigation.Id;
            CategoryNavigation = categoryNavigation;
        }

        public string Name { get; set; }
        public string ProductAdjective { get; set; }
        public string ProductMaterial { get; set; }
        public string Ean8 { get; set; }
        public string Ean13 { get; set; }
        public decimal Nett { get; set; }
        public int Tax { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public virtual Category CategoryNavigation { get; private set; } = null!;

        public int CatPriceTypeId { get; set; }
        public virtual CatPriceType CatPriceTypeNavigation { get; private set; } = null!;
    }
}
