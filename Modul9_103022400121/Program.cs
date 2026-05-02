using System.Diagnostics.SymbolStore;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

internal class program
{
    public class BankTransferConfig
    {
        public Config config;
        public Transfer transfer;
        public Confirmation Confirmation;
        private const string filepath = @"jsconfig1.json";

        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch {
                SetDefault();
                WriteConfigFile();
            }
        }

        public void ReadConfigFile() {
            string hasilbaca = File.ReadAllText(filepath);
            config = JsonSerializer.Deserialize<Config>(hasilbaca);
        }

        public void WriteConfigFile() {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

            string tulisan = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filepath, tulisan);
        }

        public void SetDefault() {
            config = new Config();
        }

    }


    public class Config
    {
        public string lang;
        public Transfer transfer;
        public List<string> methods;
        public Confirmation confirmation;
        public Config() {
            this.lang = "en";
            this.transfer = new Transfer();
            methods.Add("RTO (real-time)”, “SKN”, “RTGS”, “BI FAST");
            this.confirmation = new Confirmation();
        }
        public Config(string lang, Transfer transfer, List<string> methods, Confirmation confirmation) {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = methods;
            this.confirmation = confirmation;
        }
    }

    public class Transfer
    {
        public double thresshold;
        public double low_fee;
        public double high_fee;
        public Transfer() {
            this.thresshold = 25000000;
            this.low_fee = 6500;
            this.high_fee = 15000;
        }
        public Transfer(double thresshold, double low_fee, double high_fee) {
            this.thresshold = thresshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }

    public class Confirmation
    {
        public string en;
        public string id;
        public Confirmation() {
            this.en = "yes";
            this.id = "ya";
        }
        public Confirmation(string en, string id) {
            this.en = en;
            this.id = id;
        }
    }


    public static void main(string[] args)
    {
        BankTransferConfig bank = new BankTransferConfig();
        double jumlahTransfer;
        Transfer transfer =new Transfer();
        double biayaTransfer;
        double totalBiaya;

        if (bank.config.lang == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer : ");
        } else if (bank.config.lang == "id")
        {
            Console.WriteLine(" Masukkan jumlah uang yang akan ditransfer: ");
        }
        jumlahTransfer = double.Parse(Console.ReadLine());
        if ( jumlahTransfer <= transfer.thresshold)
        {
            biayaTransfer = transfer.low_fee;
        }
        else if (jumlahTransfer > transfer.thresshold)
        {
            biayaTransfer = transfer.high_fee;
        }
        totalBiaya = jumlahTransfer + biayaTransfer;
        if (bank.config.lang == "en")
        {
            Console.WriteLine("transfer fee : " + totalBiaya);
        }
        else if (bank.config.lang == "id")
        {
            Console.WriteLine(" biaya transfer: " + totalBiaya );
        }
    }
}
