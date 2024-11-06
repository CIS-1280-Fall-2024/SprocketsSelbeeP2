using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprocketsSelbeeP2.Models
{
    public class AluminumSprocket : Sprocket
    {
        public AluminumSprocket(int itemID, int itemQty, int teethQty) : base(itemID, itemQty, teethQty)
        {
            Calculate();
        }

        protected override void Calculate()
        {
            const decimal pricePerTooth = 0.04m;
            Price = TeethQty * ItemQty * pricePerTooth;
        }

    }
}
