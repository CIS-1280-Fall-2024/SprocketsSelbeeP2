using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprocketsSelbeeP2.Models
{
    public class SteelSprocket : Sprocket
    {
        public SteelSprocket(int itemID, int itemQty, int teethQty) : base(itemID, itemQty, teethQty)
        {
            // Call the Calculate method if needed to initialize other properties
            Calculate();
        }

        protected override void Calculate()
        {
            const decimal pricePerTooth = 0.05m;
            Price = TeethQty * ItemQty * pricePerTooth;
        }
    }
}
