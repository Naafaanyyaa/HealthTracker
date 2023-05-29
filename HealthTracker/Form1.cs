
using HealthTracker.Interfaces;
using Autofac;
using HealthTracker.Models;
using Microsoft.Extensions.Configuration;
using static System.Windows.Forms.Design.AxImporter;
using System.Runtime;
using static System.Windows.Forms.AxHost;
using System.Xml;

namespace HealthTracker
{
    public partial class Form1 : Form
    {
        private readonly IRequestToServerService _requestToServerService;
        private string userName;
        private string animalId;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IMessageService _messageService;
        public Form1(IContainer container)
        {
            InitializeComponent();
            _requestToServerService = container.Resolve<IRequestToServerService>();
            _rabbitMqService = container.Resolve<IRabbitMqService>();
            _messageService = container.Resolve<IMessageService>();
            GetProfileSettings("data.bin");
            GetIoTSettings();
        }

        private async void SendData(object sender, EventArgs e)
        {
            try
            {
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorizeObj.Token);

                var resonse = _messageService.GenerateRecommendations(animalId, userName, _requestToServerService.GenerateData());

                _rabbitMqService.SendMessage(resonse);
                label3.BackColor = Color.GreenYellow;
                label3.Text = "Sended";
                label3.Visible = true;
            }
            catch (Exception exception)
            {
                label3.BackColor = Color.Red;
                label3.Text = "Not sended";
                label3.Visible = true;
                Console.WriteLine(exception);
            }
        }

        private void SaveUsernameAndPassword(object sender, EventArgs e)
        {
            using (FileStream stream = new FileStream("data.bin", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    string info = $"{textBox1.Text.Trim()};{textBox2.Text.Trim()}";
                    writer.Write(info);
                }
            }
        }

        private void GetProfileSettings(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        textBox1.Text += userName = line.Split(';')[0].Remove(0, 1);
                        textBox2.Text += animalId = line.Split(';')[1];
                    }
                    reader.Close();
                }
            }
        }

        private void GetIoTSettings()
        {
            XmlDocument config = new XmlDocument();
            config.Load("../../../App.config");

            textBox3.Text = config.SelectSingleNode("//Bpm/Lower")?.InnerText;
            textBox4.Text = config.SelectSingleNode("//Bpm/Upper")?.InnerText;
            textBox5.Text = config.SelectSingleNode("//Temperature/Lower")?.InnerText;
            textBox6.Text = config.SelectSingleNode("//Temperature/Upper")?.InnerText;
            textBox7.Text = config.SelectSingleNode("//Pressure/Lower")?.InnerText;
            textBox8.Text = config.SelectSingleNode("//Pressure/Upper")?.InnerText;
            textBox9.Text = config.SelectSingleNode("//BloodOxygen/Lower")?.InnerText;
            textBox10.Text = config.SelectSingleNode("//BloodOxygen/Upper")?.InnerText;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("../../../App.config");

            config.SelectSingleNode("//Bpm/Lower").InnerText = textBox3.Text.Trim();
            config.SelectSingleNode("//Bpm/Upper").InnerText = textBox4.Text.Trim();
            config.SelectSingleNode("//Temperature/Lower").InnerText = textBox5.Text.Trim();
            config.SelectSingleNode("//Temperature/Upper").InnerText = textBox6.Text.Trim();
            config.SelectSingleNode("//Pressure/Lower").InnerText = textBox7.Text.Trim();
            config.SelectSingleNode("//Pressure/Upper").InnerText = textBox8.Text.Trim();
            config.SelectSingleNode("//BloodOxygen/Lower").InnerText = textBox9.Text.Trim();
            config.SelectSingleNode("//BloodOxygen/Upper").InnerText = textBox10.Text.Trim();

            config.Save("../../../App.config");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("../../../App.config");
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

                    config.SelectSingleNode("//Language").InnerText = "en";
                    config.Save("../../../App.config");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");

                    config.SelectSingleNode("//Language").InnerText = "uk";
                    config.Save("../../../App.config");
                    break;

            }
            this.Controls.Clear();
            InitializeComponent();
            GetProfileSettings("data.bin");
            GetIoTSettings();
        }
    }
}