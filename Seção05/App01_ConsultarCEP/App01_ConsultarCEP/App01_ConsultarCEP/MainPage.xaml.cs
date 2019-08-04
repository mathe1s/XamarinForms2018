using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Serviço.Modelo;
using App01_ConsultarCEP.Serviço;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Botao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereço end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {    
                        Resultado.Text = string.Format("Endereço: {2} de {3} {0},{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado", "Ok");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "Ok");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter oito caracteres", "Ok");
                valido = false;
            }
            int NovoCEP = 0;
            if (!int.TryParse(cep,out NovoCEP))
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter apenas valores numéricos", "Ok");
                valido = false;
            }
            return valido;
        }
    }
}
