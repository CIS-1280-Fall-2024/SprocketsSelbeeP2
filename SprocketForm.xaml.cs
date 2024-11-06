/*  Rikki Selbee
 *  SprocketForm.xaml.cs
 *  11.6.24
*/

using SprocketsSelbeeP2.Models;

namespace SprocketsSelbeeP2;

public partial class SprocketForm : ContentPage
{
	public SprocketForm()
	{
		InitializeComponent();
    }

    public event Action<Sprocket> SprocketAdded;

    private async void AddItemButtonClicked(object sender, EventArgs e)
    {
        int itemId = int.Parse(ItemIDEntry.Text);
        int itemQty = int.Parse(ItemQTYEntry.Text);
        int teethQty = int.Parse(TeethQTYEntry.Text);
        string selectedMaterial = MaterialPicker.SelectedItem?.ToString();

        Sprocket newSprocket = selectedMaterial?.ToLower() switch
        {
            "steel" => new SteelSprocket(itemId, itemQty, teethQty),
            "aluminum" => new AluminumSprocket(itemId, itemQty, teethQty),
            "plastic" => new PlasticSprocket(itemId, itemQty, teethQty)
        };

        if (newSprocket != null)
        {
            SprocketAdded.Invoke(newSprocket); // trigger the SprocketAdded Action.
        }

        // Close the form after adding
        await Navigation.PopModalAsync();
    }

    private async void CancelItemButtonClicked(object sender, EventArgs e) // Just kidding.. i dont want to buy this.
    {
        await Navigation.PopModalAsync();
    }
}