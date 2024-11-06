/*  Rikki Selbee
 *  SprocketOrder.cs
 *  11.6.24
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprocketsSelbeeP2.Models
{
    public class SprocketOrder
    {
        // == PROPERTIES
        public string CustomerName { get; set; }
        public Address Address { get; set; }
        public ObservableCollection<Sprocket> Items { get; set; }
        public decimal TotalPrice { get; set; }
    
        // == METHODS
        // -- constructors
        public SprocketOrder() : this (new Address(), "Doe", new ObservableCollection<Sprocket>(), 0)
        {
        }
 
        public SprocketOrder(Address address, string name, ObservableCollection<Sprocket> items, decimal total)
        {
            Address = address;
            CustomerName = name;
            Items = items;
            TotalPrice = total;
            Items.CollectionChanged += OnItemsChanged; // bind f(x) to the ObservableCollection.CollectionChanged event.
        }

        //-- public
        public void Calculate()
        {
            TotalPrice = Items.Sum(s => s.Price); // sum the items Price
        }

        public override string ToString()
        {
            var itemsSummary = string.Join("\n", Items.Select((item, index) => $"Order #{index + 1}: {item}")); // Create a string by joining with a new line each item from the Items list.
            string addressSummary = Address != null ? $"Ship to: {Address}" : "Local Pickup"; // Build the adress.
            return $"{CustomerName}: {Items.Count} items, Total Price: ${TotalPrice:F2}\n{addressSummary}\n\n{itemsSummary}"; // build the final string.
        }

        // -- private
        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e) // event handler bound to observable.
        {
            Calculate();
        }
    }
}
