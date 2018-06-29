using System;

namespace RevisoSamples.DTO
{
    public class Customer
    {
        public string address { get; set; }
        public double balance { get; set; }
        public string city { get; set; }
        public string contacts { get; set; }
        public string corporateIdentificationNumber { get; set; }
        public string country { get; set; }
        public Countrycode countryCode { get; set; }
        public string currency { get; set; }
        public Customergroup customerGroup { get; set; }
        public string email { get; set; }
        public string italianCustomerType { get; set; }
        public DateTime lastUpdated { get; set; }
        public Paymentterms paymentTerms { get; set; }
        public bool splitPayment { get; set; }
        public Templates templates { get; set; }
        public string vatNumber { get; set; }
        public Vatzone vatZone { get; set; }
        public string zip { get; set; }
        public int customerNumber { get; set; }
        public string name { get; set; }
        public Metadata metaData { get; set; }
        public string self { get; set; }
    }

    public class Countrycode
    {
        public string code { get; set; }
        public string self { get; set; }
    }

    public class Customergroup
    {
        public int customerGroupNumber { get; set; }
        public string self { get; set; }
    }

    public class Paymentterms
    {
        public int paymentTermsNumber { get; set; }
        public string self { get; set; }
    }

    public class Templates
    {
        public string invoice { get; set; }
        public string self { get; set; }
    }

    public class Vatzone
    {
        public int vatZoneNumber { get; set; }
        public string self { get; set; }
    }

    public class Metadata
    {
        public Delete delete { get; set; }
        public Replace replace { get; set; }
    }

    public class Delete
    {
        public string description { get; set; }
        public string href { get; set; }
        public string httpMethod { get; set; }
    }

    public class Replace
    {
        public string description { get; set; }
        public string href { get; set; }
        public string httpMethod { get; set; }
    }
}
