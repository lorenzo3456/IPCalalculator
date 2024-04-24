using Microsoft.Maui.Controls;
namespace LorDonIpCalc
{
    public partial class MainPage : ContentPage
    {
        IPAddress ip = new IPAddress();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            bool validazioneIP = ip.ValidateIPAddress(IndirizzoIp.Text);
            bool validazioneSubnet = ip.ValidateSubnetMask(Netmask.Text);
            IndirizzoIp.IsReadOnly = true;
            Netmask.IsReadOnly = true;
            if (validazioneIP && validazioneSubnet)
            { 
                IndirizzoIp.BackgroundColor = Colors.Green;
                Netmask.BackgroundColor = Colors.Green;
                validita.Text = "DATI VALIDI!";
                
                StampaAVideo();
                
            }
            else
            {
                if (validazioneIP == false && validazioneSubnet == false)
                {
                    IndirizzoIp.BackgroundColor = Colors.Red;
                    Netmask.BackgroundColor = Colors.Red;
                    validita.Text = "IP E SUBNET NON VALIDI";
                    NetworkAddress.Text = "";
                    BroadCast.Text = "";
                    firstHost.Text = "";
                    lastHost.Text = "";
                }
                else if (validazioneIP == true && validazioneSubnet == false)
                {
                    IndirizzoIp.BackgroundColor = Colors.Green;
                    Netmask.BackgroundColor = Colors.Red;
                    validita.Text = "SUBNET NON VALIDA";
                    NetworkAddress.Text = "";
                    BroadCast.Text = "";
                    firstHost.Text = "";
                    lastHost.Text = "";
                }else if (validazioneIP == false && validazioneSubnet == true)
                {
                    IndirizzoIp.BackgroundColor = Colors.Red;
                    Netmask.BackgroundColor = Colors.Green;
                    validita.Text = "IP NON VALIDO";
                    NetworkAddress.Text = "";
                    BroadCast.Text = "";
                    firstHost.Text = "";
                    lastHost.Text = "";
                }
                


            }

           

            
            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        public void StampaAVideo()
        {
            ip.ValidateSubnetMask(Netmask.Text);
            NetworkAddress.Text = ip.GetNetworkAddress();
            BroadCast.Text = ip.GetBroadcastAddress();
            firstHost.Text = ip.GetFirstHostAddress();
            lastHost.Text = ip.GetLastHostAddress();

        }

        public void AbilitaModifica(object sender, EventArgs e)
        {

            IndirizzoIp.BackgroundColor = default;
            Netmask.BackgroundColor = default;
            IndirizzoIp.IsReadOnly = false;
            Netmask.IsReadOnly = false;
            validita.Text = "IN ATTESA DEI DATI";
            NetworkAddress.Text = "";
            BroadCast.Text = "";
            firstHost.Text = "";
            lastHost.Text = "";
            IndirizzoIp.Text = "";
            Netmask.Text = "";
        }

    }
}

