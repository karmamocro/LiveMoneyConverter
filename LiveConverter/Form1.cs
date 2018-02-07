using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;

namespace LiveConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            WebClient wbClient = new WebClient();

            string url = "http://api.fixer.io/latest?base=" + ddCurrency1.selectedValue + "&symbols=" + ddCurrency2.selectedValue;
            rtbLogger.AppendText("\n" + url + "\n");

            var json = wbClient.DownloadString(url);
            rtbLogger.AppendText(json.ToString());

            JObject currency = JObject.Parse(json);
            IList<JToken> results = currency["rates"].Children().ToList();
            IList<string> searchResults = new List<string>();

            foreach (JToken result in results)
            {
                rtbLogger.AppendText("\n" + result.Last.ToString() + "\n");

                searchResults.Add(result.Last.ToString());
            }
            double num1 = Convert.ToDouble(searchResults[0]);
            double resultX = Convert.ToDouble(tbValueToExchange.Text) * num1;
            lblCRate.Text = num1.ToString();
            lblValue.Text = resultX.ToString();
        }
    }
}
