using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Exceptions;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly SpengerBangerContext _dbContext;

        public ShopService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateShop(CreateShopDto newShop)
        {
            // Bedungungen:
            // * Der Shop soll immer und nur Sonntags geschlossen sein
            // * 

            if (newShop.Closed.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ServiceException("Sonntag ist Ruhetag!");
            }

            if (newShop is null)
            {
                throw new ServiceException("Create Shop fehlgeschlagen!");
            }
            _dbContext.Add(newShop.ToShop());
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateShopAsync(ShopDto dto)
        {
            if (dto is null)
            {
                throw new ServiceException("Update Shop fehlgeschlagen!");
            }

            Shop shop = new(dto.CompanySuffix, dto.Name, dto.Location, dto.CatchPhrase, dto.Bs
                , new Address(dto.Street, dto.Zip, dto.City));
            _dbContext.Update(shop);
            await _dbContext.SaveChangesAsync();
        }

        public ShopDto GetShopById(Guid id)
        {
            ShopDto dto = _dbContext.Shops
                .Where(s => s.Guid == id)
                .Select(s =>
                    new ShopDto
                    {
                        Bs = s.Bs,
                        CatchPhrase = s.CatchPhrase,
                        City = s.Address.City,
                        CompanySuffix = s.CompanySuffix,
                        Guid = s.Guid,
                        Location = s.Location,
                        Name = s.Name,
                        Street = s.Address.Street,
                        Zip = s.Address.Zip
                    })
                .FirstOrDefault()
                        ?? new ShopDto();
            return dto;
        }

        public IQueryable<ShopDto> ListAllShops()
        {
            return _dbContext
                .Shops
                .Select(s =>
                    new ShopDto
                    {
                        Bs = s.Bs,
                        CatchPhrase = s.CatchPhrase,
                        City = s.Address.City,
                        CompanySuffix = s.CompanySuffix,
                        Guid = s.Guid,
                        Location = s.Location,
                        Name = s.Name,
                        Street = s.Address.Street,
                        Zip = s.Address.Zip
                    });
        }
    }
}