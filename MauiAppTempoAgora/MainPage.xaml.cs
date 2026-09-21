using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    // Verifica se o dispositivo possui acesso à internet
                    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayAlert(
                            "Sem conexão",
                            "Verifique sua conexão com a internet.",
                            "OK"
                        );

                        return;
                    }

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon}\n" +
                                         $"Nascer do Sol: {t.sunrise}\n" +
                                         $"Por do Sol: {t.sunset}\n" +
                                         $"Temp Máx: {t.temp_max}\n" +
                                         $"Temp Mín: {t.temp_min}\n" +
                                         $"Descrição: {t.description}\n" +
                                         $"Velocidade do vento: {t.speed}\n" +
                                         $"Visibilidade: {t.visibility}\n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert(
                    "Sem conexão",
                    "Não foi possível acessar a internet. Verifique sua conexão.",
                    "OK"
                );
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}