using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LorDonIpCalc
{
    public class IPAddress
    {
        //dichiarazione attributi
        private uint _ipAddress;
        private uint _subnetmask;


        public bool ValidateIPAddress(string ip)
        {
            // Divido la stringa e metto parte per parte nell'array
            if (ip == null)
            {
                return false;
            }
           

            string[] ipArray = ip.Split('.');

            // Controllo che l'array abbia una grandezza uguale a 4 perchè un indirizzo IP valido
            // deve essere composto da 4 parti
            if (ipArray.Length != 4)
            {
                return false;
            }
            else
            {
                // Controllo ogni parte dell'indirizzo IP
                foreach (string parte in ipArray)
                {
                    // Controllo che ogni parte sia un numero compreso tra 0 e 255
                    if (int.TryParse(parte, out int value) != true || value < 0 || value > 255)
                    {
                        return false;
                    }
                }

                // Se tutte le parti sono valide, restituisco true
                _ipAddress = ConvertToBinary(ip);
                return true;
            }
        }

        public bool ValidateSubnetMask(string subnet)
        {
            if (subnet == null)
            {
                return false;
            }
            // Divido la stringa e metto parte per parte nell'array
            string[] subnetArray = subnet.Split('.');

            // Controllo che l'array abbia una grandezza uguale a 4 perchè un indirizzo IP valido
            // deve essere composto da 4 parti
            if (subnetArray.Length != 4)
            {
                return false;
            }
            else
            {
                // Controllo ogni parte dell'indirizzo IP
                foreach (string parte in subnetArray)
                {
                    // Controllo che ogni parte sia un numero compreso tra 0 e 255
                    if (int.TryParse(parte, out int value) != true || value < 0 || value > 255)
                    {
                        return false;
                    }
                }

                // Se tutte le parti sono valide, restituisco true
                _subnetmask = ConvertToBinary(subnet);
                return true;
            }
        }


        public string GetNetMask()
        {
            //trasformo la subnet da binario a decimale e le faccio il return
            string tmp = ConvertToDottedDecimal(_subnetmask);
            return tmp;
        }

        public string GetNetworkAddress()
        {
            //faccio l'and tra ipaddress e la subnet mask 
            uint networkAddress = _ipAddress & _subnetmask;
            //converto in decimale e returno tmp
            string ris = ConvertToDottedDecimal(networkAddress);
            return ris;
        }

        public string GetBroadcastAddress()
        {
            //~ inverte 
            uint subnetMaskInvertita = ~_subnetmask;
            //faccio l'or tra ipaddress e la subnet invertita ottenendo il broadcast address
            uint indirizzoBroadcast = _ipAddress | subnetMaskInvertita;
            return ConvertToDottedDecimal(indirizzoBroadcast);
        }

        public string GetFirstHostAddress()
        {
            uint firstHostAddress = (_ipAddress & _subnetmask) + 1;
            return ConvertToDottedDecimal(firstHostAddress);
        }

        public string GetLastHostAddress()
        {
            //~ inverte la netmask
            uint subnetMaskInvertita = ~_subnetmask;
            uint lastHostAddress = (_ipAddress | subnetMaskInvertita) - 2;
            return ConvertToDottedDecimal(lastHostAddress);
        }

        private uint ConvertToBinary(string dottedDecimal)
        {
            string[] ipInArray = dottedDecimal.Split('.');
            uint ris = 0;
            for (int i = 0; i < 4; i++)
            {
                ris <<= 8;
                ris |= uint.Parse(ipInArray[i]);
            }
            return ris;
        }

        private string ConvertToDottedDecimal(uint binary)
        {
            //shifto il valore e davanti gli metto gli 0 rimanenti per tutti e 4 i punti, una volta finito gli 0 scompaiono
            //0 = 0xff
            //>> shifto a destra i bit di 24 posti e così via
            return $"{(binary >> 24) & 0xFF}.{(binary >> 16) & 0xFF}.{(binary >> 8) & 0xFF}.{binary & 0xFF}";
        }
    }
}
