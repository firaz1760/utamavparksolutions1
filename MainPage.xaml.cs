using Microsoft.Maui.Controls;

namespace UTAMavParkSolutions;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        StartSplashScreen();

        // Attach the ValueChanged event for the DurationSlider
        DurationSlider.ValueChanged += OnDurationSliderValueChanged;

        // Attach the DateSelected event for the DatePicker
        CalendarDatePicker.DateSelected += OnDateSelected;
    }

    private async void StartSplashScreen()
    {
        // Show the splash screen for 3 seconds
        await Task.Delay(3000);

        // Hide the SplashScreen and show LoginContent
        SplashScreen.IsVisible = false;
        LoginContent.IsVisible = true;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var phoneNumber = PhoneNumberEntry?.Text;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            // Hide LoginContent and show MapContent
            LoginContent.IsVisible = false;
            MapContent.IsVisible = true;

            await DisplayAlert("Welcome", $"Logged in with: {phoneNumber}", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Please enter a valid phone number.", "OK");
        }
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        // Hide LoginContent and show RegisterContent
        LoginContent.IsVisible = false;
        RegisterContent.IsVisible = true;
    }

    private void OnLogInLinkClicked(object sender, EventArgs e)
    {
        // Hide RegisterContent and show LoginContent
        RegisterContent.IsVisible = false;
        LoginContent.IsVisible = true;
    }

    private async void OnRegisterConfirmClicked(object sender, EventArgs e)
    {
        var fullName = FullNameEntry?.Text;
        var mobileNumber = RegisterMobileEntry?.Text;
        var password = RegisterPasswordEntry?.Text;

        if (!string.IsNullOrWhiteSpace(fullName) &&
            !string.IsNullOrWhiteSpace(mobileNumber) &&
            !string.IsNullOrWhiteSpace(password))
        {
            // Simulate a successful registration
            await DisplayAlert("Success", "Registration completed successfully!", "OK");

            // Hide RegisterContent and show LoginContent
            RegisterContent.IsVisible = false;
            LoginContent.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", "Please fill in all the required fields.", "OK");
        }
    }

    private async void OnSearchCompleted(object sender, EventArgs e)
    {
        var searchText = SearchEntry?.Text;

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            if (searchText.Equals("West Campus Garage", StringComparison.OrdinalIgnoreCase))
            {
                // Simulate navigation to parking detail screen
                MapContent.IsVisible = false;
                DetailsContent.IsVisible = true;

                await DisplayAlert("Found", $"Navigating to {searchText}.", "OK");
            }
            else
            {
                await DisplayAlert("Not Found", $"No results found for: {searchText}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Please enter a search term.", "OK");
        }
    }

    private void OnBookParkingClicked(object sender, EventArgs e)
    {
        // Hide DetailsContent and show ParkingSpotContent
        DetailsContent.IsVisible = false;
        ParkingSpotContent.IsVisible = true;
    }

    private void OnBackToDetailsClicked(object sender, EventArgs e)
    {
        // Navigate back to DetailsContent from ParkingSpotContent
        ParkingSpotContent.IsVisible = false;
        DetailsContent.IsVisible = true;
    }

    private void OnBackToMapClicked(object sender, EventArgs e)
    {
        // Navigate back to MapContent from DetailsContent
        DetailsContent.IsVisible = false;
        MapContent.IsVisible = true;
    }

    private void OnSpotSelected(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button != null)
        {
            // Deselect all buttons first
            var parentGrid = button.Parent as Grid;
            if (parentGrid != null)
            {
                foreach (var child in parentGrid.Children)
                {
                    if (child is Button spotButton)
                    {
                        spotButton.BackgroundColor = Colors.White;
                        spotButton.TextColor = Colors.Black;
                    }
                }
            }

            // Highlight the selected spot
            button.BackgroundColor = Color.FromArgb("#4A90E2");
            button.TextColor = Colors.White;
        }
    }

    private void OnContinueClicked(object sender, EventArgs e)
    {
        // Hide ParkingSpotContent and show ParkingDetailsContent
        ParkingSpotContent.IsVisible = false;
        ParkingDetailsContent.IsVisible = true;

        DisplayAlert("Success", "Proceeding to Parking Details.", "OK");
    }

    private async void OnConfirmPayClicked(object sender, EventArgs e)
    {
        // Hide ParkingDetailsContent and show PaymentContent
        ParkingDetailsContent.IsVisible = false;
        PaymentContent.IsVisible = true;

        await DisplayAlert("Proceeding to Payment", "Redirecting to payment screen.", "OK");
    }

    private async void OnPaymentCompleted(object sender, EventArgs e)
    {
        // Hide PaymentContent and show ParkingTicketContent
        PaymentContent.IsVisible = false;
        ParkingTicketContent.IsVisible = true;

        await DisplayAlert("Payment Successful", "Here is your parking ticket.", "OK");
    }

    private async void OnExitButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to the map screen
        ParkingTicketContent.IsVisible = false;
        MapContent.IsVisible = true;

        await DisplayAlert("Goodbye", "You have exited the parking ticket screen.", "OK");
    }

    private void OnDurationSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Update the DurationLabel based on the slider value
        int hours = (int)e.NewValue;
        DurationLabel.Text = $"{hours} hrs";

        // Optionally, update the total amount dynamically
        double totalCost = hours * 4.0; // Example: $4 per hour
        TotalAmountLabel.Text = $"${totalCost:0.00}";
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        // Display an alert with the selected date
        var selectedDate = e.NewDate.ToString("MMMM dd, yyyy"); // Format the date as "Month Day, Year"
        DisplayAlert("Date Selected", $"You selected: {selectedDate}", "OK");
    }

    private void OnCalendarClicked(object sender, EventArgs e)
    {
        // Ensure DatePicker is visible when the calendar button is clicked
        CalendarDatePicker.IsVisible = true;
    }
}
