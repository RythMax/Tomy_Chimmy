using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomy_Chimy.Web.Models;

namespace Tomy_Chimy.Web.ViewsModels
{
    public class InvoiceView
    {
        
            public Models.Invoice Invoice { get; set; }

            public InvoiceDetail InvoiceDetail { get; set; }

            public PayingMethod PayingMethod { get; set; }

            public Client Client { get; set; }

            public List<InvoiceDetail> Artículos { get; set; }
        
    }
}
