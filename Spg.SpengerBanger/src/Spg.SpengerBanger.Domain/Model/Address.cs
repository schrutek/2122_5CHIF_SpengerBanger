using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    /// <summary>
    /// Record (Seit .Net5 an Board)
    /// </summary>
    /// <param name="Street"></param>
    /// <param name="Zip"></param>
    /// <param name="City"></param>
    public record Address(string Street, string Zip, string City);

    /// <summary>
    /// Klassisch mittels Class
    /// </summary>
    //public class Address
    //{
    //    public string Street { get; set; }
    //    public string Zip { get; set; }
    //    public string City { get; set; }
    //    public Address(string street, string zip, string city)
    //    {
    //        Street = street;
    //        Zip = zip;
    //        City = city;
    //    }
    //}
}
