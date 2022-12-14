using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace AirDucksProject.Models
{
    public class Sensor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Mac { get; set; }

        public Sensor()
        {

        }

        public Sensor(string name, int id, string mac)
        {
            Name = name;
            Id = id;
            Mac = mac;
        }

        public void ValidateName()
        { 
            if(String.IsNullOrEmpty(Name)) throw new ArgumentException("Name cannot be null or empty");
        }
        public void ValidateMac()
        {
            if (string.IsNullOrEmpty(Mac)) throw new ArgumentException("Mac address cannot be null or empty");
            try
            {
                PhysicalAddress py = PhysicalAddress.Parse(Mac);
            }
            catch
            {
                throw new ArgumentException("MAC address is not valid");
            }
        }
    }
}
