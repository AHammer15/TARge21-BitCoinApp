﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitCoinApp_Thor_Hammer
{
    public partial class btnGetRates : Form
    {
        public btnGetRates()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(CurrencyCombo.SelectedItem.ToString() == "EUR")
            {
                resultLabel.Visible = true;
                ResultTextBox.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(AmountOfCoinBox.Text) * bitcoin.bpi.EUR.rate_float;
                ResultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.EUR.code}";
            }
            if (CurrencyCombo.SelectedItem.ToString() == "USD")
            {
                resultLabel.Visible = true;
                ResultTextBox.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(AmountOfCoinBox.Text) * bitcoin.bpi.USD.rate_float;
                ResultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.USD.code}";
            }
        }

        public static BitCoinRates GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            BitCoinRates bitcoin;
            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRates>(response);
            }
            return bitcoin;
        }
    }
}
