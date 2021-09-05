using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ColourMemory.Models
{
    public class Card
    {
        public string Image;
        public Card()
        {
           Image = "/img/CardBackSide_ColourMemory.PNG";
        }
        public int Id { get; set; }
        public bool Up { get; set; }
        public string CardColor { get; set; }

        public bool Gone { get; set; }
        
       

    }
}
