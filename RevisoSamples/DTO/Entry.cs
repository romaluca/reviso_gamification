namespace RevisoSamples.DTO
{
    public class Account
    {
        public int accountNumber { get; set; }
        public string self { get; set; }
    }

    public class Entry
    {
        public Account account { get; set; }
        public double amount { get; set; }
        public double amountInBaseCurrency { get; set; }
        public string currency { get; set; }
        public string date { get; set; }
        public int entryNumber { get; set; }
        public string entryType { get; set; }
        public int voucherNumber { get; set; }
        public string self { get; set; }
    }
}