using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class PharmacistViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя аптекаря")]
        public string FullName { get; set; }
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}