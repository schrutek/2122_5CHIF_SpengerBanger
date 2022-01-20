using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    public class Shop : EntityBase
    {
        protected Shop() { }
        public Shop(string companySuffix, string name, string location, string catchPhrase, string bs, Address address, Guid guid)
        {
            CompanySuffix = companySuffix;
            Name = name;
            Location = location;
            CatchPhrase = catchPhrase;
            Bs = bs;
            Address = address;
            Guid = guid;
        }

        public Shop(string companySuffix, string name, string location, string catchPhrase, string bs, Address address)
        {
            CompanySuffix = companySuffix;
            Name = name;
            Location = location;
            CatchPhrase = catchPhrase;
            Bs = bs;
            Address = address;
        }

        public string CompanySuffix { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        public Address Address { get; set; }
        public Guid Guid { get; set; }


        protected List<Category> _categories = new();
        public virtual IReadOnlyList<Category> Categories => _categories;


        public Category AddCategory(Category newCategory)
        {
            if (newCategory is not null)
            {
                newCategory.ShopId = Id;
                _categories.Add(newCategory);
            }
            return newCategory;
        }
        public void AddCategories(List<Category> categories)
        {
            _categories.AddRange(categories);
        }
    }
}
