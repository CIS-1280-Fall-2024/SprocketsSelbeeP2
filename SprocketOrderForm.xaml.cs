/*  Rikki Selbee
 *  Sprocket Order Form.xaml.cs
 *  11.6.24 - A truly dark day.
*/ 

using System.Collections.ObjectModel;
using System.Diagnostics;
using SprocketsSelbeeP2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SprocketsSelbeeP2;

public partial class SprocketOrderForm : ContentPage
{
    // === Init
    private SprocketOrder currentOrder; // Property to hold the order

	public SprocketOrderForm() // new instantiations of the form will get a new order object attached.
	{
		InitializeComponent();

        // Create the current order object
        currentOrder = new SprocketOrder();
        currentOrder.Address = new Address();
        currentOrder.CustomerName = "TBD";

        // Bind the item list to the view.
        OrderListView.ItemsSource = currentOrder.Items;
    }

    // === Event Handler Functions
    private void OnSprocketAdded(Sprocket newSprocket) // this gets called from the sprocket object event.
    {
        currentOrder.Items.Add(newSprocket);  // add the new item from the event to the list
        UpdateView(); 
    }

    public void CheckBoxDeliveryChanged(object sender, EventArgs e) // this toggles the address field visibility
    {
        if (GridAddressFields.IsVisible == true)
        {
            GridAddressFields.IsVisible = false;
        }
        else
        {
            GridAddressFields.IsVisible = true;
        }
    }

    private async void AddItemToOrderButtonClicked(object sender, EventArgs e) 
    {
        var sprocketForm = new SprocketForm();  // Create a new sprocket form
        sprocketForm.SprocketAdded += OnSprocketAdded; // attach to the SprocketAdded Event on the form
        await Navigation.PushModalAsync(sprocketForm); // shove the view with the object
    }

    private void RemoveItemFromOrderButtonClicked(object sender, EventArgs e)
    {
        // Get the selected item
        var selectedItem = OrderListView.SelectedItem as Sprocket;

        // if it exists.. remove from list and rerender
        if (selectedItem != null)
        {
            currentOrder.Items.Remove(selectedItem);
            UpdateView();
        }
    }

    private void PrintOrderButtonClicked(object sender, EventArgs e)
    {
        // get the customer name from the view
        currentOrder.CustomerName = CustomerNameEntry.Text;

        // Check if delivery is selected - if yes, get the address. 
        if (CheckBoxDelivery.IsChecked)
        {
            currentOrder.Address = new Address
            {
                Street = StreetEntry.Text,
                City = CityEntry.Text,
                State = StateEntry.Text,
                Zip = ZipEntry.Text
            };
        }
        else
        {
            currentOrder.Address = null; // Set to null for local pickup
        }
        
        OrderSummaryEditor.Text = currentOrder.ToString();


        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);  // i dont know how cross platform this is. but, running on windows would not let me save..(permissions issue)
        string fileName = $"{currentOrder.CustomerName}_OrderSummary_{DateTime.Now:yyyyMMddHHmmss}.txt"; // unique filename with timestamp
        string filePath = Path.Combine(documentsPath, fileName); // concat for full patch

        using (StreamWriter writer = new StreamWriter(filePath)) // I had a lot of challenge with the file picker versions.
        {
            writer.WriteLine(currentOrder.ToString());
        }

        // Notify the user that the file has been saved
        DisplayAlert("Success", $"Order summary saved to {filePath}", "OK");
    }

    private void UpdateView() 
    {
        // show the total
        OrderTotal.Text = $"Total: ${currentOrder.TotalPrice:F2}";

        // Refresh the ListView
        OrderListView.ItemsSource = null;
        OrderListView.ItemsSource = currentOrder.Items;
    }
}