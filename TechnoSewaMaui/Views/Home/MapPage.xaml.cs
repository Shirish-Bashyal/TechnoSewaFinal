using Microsoft.Maui.Controls.Maps;

namespace TechnoSewaMaui.Views.Home;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();

        string url;
#if ANDROID
        url = "file:///android_asset/osm.html";
#else
        // For other platforms, consider using LoadEmbeddedHtml()
        url = "about:blank"; // or some default map URL
#endif

        if (!string.IsNullOrEmpty(url))
        {
            OsmWebView.Source = url;
        }
    }

    private async void OsmWebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        try
        {
            if (e.Url?.StartsWith("dotnet://position/") == true)
            {
                e.Cancel = true;

                var coords = e.Url.Replace("dotnet://position/", "").Split(',');
                if (
                    coords.Length == 2
                    && double.TryParse(coords[0], out var latitude)
                    && double.TryParse(coords[1], out var longitude)
                )
                {
                    await Shell.Current.GoToAsync(
                        "..",
                        true,
                        new Dictionary<string, object>
                        {
                            { "SelectedLocation", new Location(latitude, longitude) }
                        }
                    );
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"{ex.Message}", "OK");
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
