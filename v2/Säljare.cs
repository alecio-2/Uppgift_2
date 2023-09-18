using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift2C { 

 // Klass som representerar säljare samt lagrar information om säljare. 
 // Säljaren har egenskaper som namn, personnummer, distrikt och sålda artiklar.
 // get och set används för att hämta information. 
    public class Säljare
        {
            public string Namn { get; set; }
            public string Personnummer { get; set; }
            public string Distrikt { get; set; }
            public int SåldaArtiklar { get; set; }
        }
    }
