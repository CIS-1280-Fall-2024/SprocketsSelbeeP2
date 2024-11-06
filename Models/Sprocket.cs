/*  Rikki Selbee
 *  Sprocket Order Form.xaml.cs
 *  11.6.24 - A truly dark day.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprocketsSelbeeP2.Models
{
    public abstract class Sprocket
    {
        // == CONSTRUCTORS
        public Sprocket() : this(0,0,0)
        {
        }
        public Sprocket(int itemID, int itemQty, int teethQty )
        {
            this.ItemID = itemID;
            this.ItemQty = itemQty;
            this.TeethQty = teethQty;
        }

        // == PROPERTIES
        public int ItemQty { get; set; }
        public int TeethQty { get; set; }
        public decimal Price { get; protected set; }
        public int ItemID { get; }

        public string Material => this switch
        {
            SteelSprocket => "Steel",
            AluminumSprocket => "Aluminum",
            PlasticSprocket => "Plastic",
            _ => "Unknown"
        };

        // == METHODS
        protected abstract void Calculate();

        public override string ToString()
        {
            return $"Item ID: {ItemID}, Quantity: {ItemQty}, Teeth: {TeethQty}, Price: ${Price:F2}";
        }
    }
}
