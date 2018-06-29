namespace RevisoSamples.DTO
{
    public class Layout
    {
        public int layoutNumber { get; set; }
        public bool isDefault { get; set; }
        public string self { get; set; }
    }

    public class PaymentTerms
    {
        public int daysOfCredit { get; set; }
        public string name { get; set; }
        public int paymentTermsNumber { get; set; }
        public string paymentTermsType { get; set; }
        public string self { get; set; }
    }

    public class Pdf
    {
        public string download { get; set; }
    }

    public class Recipient
    {
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public string zip { get; set; }
    }

    public class Voucher
    {
        public bool booked { get; set; }
        public string voucherType { get; set; }
        public int voucherId { get; set; }
        public string self { get; set; }
    }

    public class Invoice
    {
        public int bookedInvoiceNumber { get; set; }
        public string currency { get; set; }
        public Customer customer { get; set; }
        public string date { get; set; }
        public string dueDate { get; set; }
        public double grossAmount { get; set; }
        public Layout layout { get; set; }
        public double netAmount { get; set; }
        public double netAmountInBaseCurrency { get; set; }
        public PaymentTerms paymentTerms { get; set; }
        public Pdf pdf { get; set; }
        public Recipient recipient { get; set; }
        public double remainder { get; set; }
        public double remainderInBaseCurrency { get; set; }
        public double roundingAmount { get; set; }
        public double vatAmount { get; set; }
        public string displayInvoiceNumber { get; set; }
        public Voucher voucher { get; set; }
        public string self { get; set; }
    }
}