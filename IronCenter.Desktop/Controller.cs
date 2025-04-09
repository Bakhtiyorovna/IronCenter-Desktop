using IronCenter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Desktop
{
    public class Controller
    {
        private readonly IProductService _productService;
        public Controller(IProductService productService)
        {
            this._productService = productService;
        }
    }
}
