using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTS.ViewModels
{
    public class ProductViewModel
    {
        public List<Category> categoriesList { get; set; }
        public Product product { get; set; }
        public List<Person> personList { get; set; }
    }
}