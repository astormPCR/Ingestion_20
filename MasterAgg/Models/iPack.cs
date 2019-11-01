using System;
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace MasterAgg.Models
{
    public enum AccPackState
    {
        Published,
        Ingest,
        Recon,
        MasterPersist,
        Hub,
        Archived,
        Error,
        Partial
    }
    public class iPack
    {
        [JsonProperty("id")]
        public Guid? IPackGuid { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("datatype")]
        public string Datatype { get; set; }

        [JsonProperty("datasource_uid")]
        public Guid DataSourceGuid { get; set; }

        [JsonProperty("statementdate")]
        public DateTime StatementDate { get; set; }

        [JsonProperty("external")]
        public External External { get; set; }
    }
    [Serializable()]
    public class ExternalSecurity
    {
        [JsonProperty("_name")]
        public string Name { get; set; }

        [JsonProperty("securityGuid")]
        public Guid? security_uid { get; set; }

        [JsonProperty("positions")]
        public List<ExpandoObject> positions { get; set; } //used external 

        [JsonProperty("transactions")]
        public List<ExpandoObject> transactions { get; set; } //used external

        [JsonProperty("identifiervalue")]
        public string IdentifierValue { get; set; }

        [JsonProperty("identifierName")]
        public string IdentifierName { get; set; }

        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("previousPositionGuid")]
        public Guid? PreviousPositionGuid { get; set; }

        [JsonProperty("previousPositionQty")]
        public decimal? PreviousPositionQuantity { get; set; }

        [JsonProperty("previousPositionStartDate")]
        public DateTimeOffset? PreviousPositionStartDate { get; set; }

        [JsonProperty("previousPositionEndDate")]
        public DateTimeOffset? PreviousPositionEndDate { get; set; }

    }

    [Serializable()]
    public class External
    {
        [JsonProperty("security")]
        public List<ExternalSecurity> Security { get; set; }
    }

    [Serializable()]
    public class Security
    {
        [JsonProperty("_name")]
        public string Name { get; set; }
        [JsonProperty("firmId")]
        public Int64 firmId { get; set; }
        [JsonProperty("securityGuid")]
        public Guid? security_uid { get; set; }

        [JsonProperty("identifiervalue")]
        public string IdentifierValue { get; set; }

        [JsonProperty("identifierName")]
        public string IdentifierName { get; set; }

        [JsonProperty("previousPositionGuid")]
        public Guid? PreviousPositionGuid { get; set; }

        [JsonProperty("previousPositionQty")]
        public decimal? PreviousPositionQuantity { get; set; }

        [JsonProperty("previousPositionStartDate")]
        public DateTimeOffset? PreviousPositionStartDate { get; set; }

        [JsonProperty("previousPositionEndDate")]
        public DateTimeOffset? PreviousPositionEndDate { get; set; }

        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("positions")]
        public List<POSITION> positions { get; set; }

        [JsonProperty("transactions")]
        public List<TRANSACTION> transactions { get; set; }
    }

    [Serializable(),  DataContract()]
    public class POSITION
    {
        [DataMember]
        public Guid Position_UID { get; set; }
        [DataMember]
        public Int64 BatchId { get; set; }
        [DataMember]
        public Guid Account_UID { get; set; }
        [DataMember]
        public Guid Security_UID { get; set; }
        [DataMember]
        public DateTimeOffset DateStart { get; set; }
        [DataMember]
        public DateTimeOffset? DateEnd { get; set; }
        [DataMember]
        public Int64 SecurityFirmID { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }
        [DataMember]
        public List<POSITION_IDENTIFIER> Identifier = new List<POSITION_IDENTIFIER>();
        [DataMember]
        public List<POSITION_DETAIL> Position_Detail = new List<POSITION_DETAIL>();
    }
    [Serializable(), DataContract()]
    public class POSITION_IDENTIFIER
    {
        [DataMember]
        public Guid Identifier_UID { get; set; }
        [DataMember]
        public Guid Position_UID { get; set; }
        [DataMember]
        public Int64 FirmID { get; set; }
        [DataMember]
        public String IdentifierType { get; set; }
        [DataMember]
        public String IdentifierName { get; set; }
        [DataMember]
        public String Identifier { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }

    }
    [Serializable(), DataContract()]
    public class POSITION_DETAIL
    {
        [DataMember]
        public Guid Detail_UID { get; set; }
        [DataMember]
        public Guid Position_UID { get; set; }
        [DataMember]
        public String Type { get; set; }
        [DataMember]
        public Decimal Quantity { get; set; }
        [DataMember]
        public String Currency { get; set; }
        [DataMember]
        public Decimal Value { get; set; }
        [DataMember]
        public Decimal? LastPrice { get; set; }
        [DataMember]
        public Decimal? Cost { get; set; }
        [DataMember]
        public Decimal? AccruedInterest { get; set; }
        [DataMember]
        public Decimal? AccruedDividends { get; set; }
        [DataMember]
        public Decimal? Expenses { get; set; }
        [DataMember]
        public Decimal? UnrealizedGains { get; set; }
        [DataMember]
        public Decimal? RealizedGains { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }
        [DataMember]
        public POSITION_BOND Position_Bond { get; set; }
        [DataMember]
        public POSITION_FX Position_FX { get; set; }
        [DataMember]
        public POSITION_OPTION Position_Option { get; set; }
        [DataMember]
        public POSITION_PRICE Position_Price { get; set; }
    }
    [Serializable(), DataContract()]
    public class POSITION_BOND
    {
        [DataMember]
        public Guid Bond_UID { get; set; }
        [DataMember]
        public Guid Detail_UID { get; set; }
        [DataMember]
        public Decimal? NextPayAmount { get; set; }
        [DataMember]
        public Decimal? Duration { get; set; }
        //   [DataMember] public Decimal? YieldToMaturity { get; set; }
        //   [DataMember] public Decimal? YieldToCall { get; set; }
        //   [DataMember] public Decimal? YieldToWorst { get; set; }
        [DataMember]
        public Decimal? YieldToMaturityPurchase { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }
    }
    [Serializable(), DataContract()]
    public class POSITION_FX
    {
        [DataMember]
        public Guid FX_UID { get; set; }
        [DataMember]
        public Guid Detail_UID { get; set; }
        [DataMember]
        public Decimal Currency1Value { get; set; }
        [DataMember]
        public Decimal Currency2Value { get; set; }
        [DataMember]
        public Decimal? Premium { get; set; }
        [DataMember]
        public String PremiumDate { get; set; }
        [DataMember]
        public String PremiumCurrency { get; set; }
        [DataMember]
        public Decimal? SettledQuantity { get; set; }
        [DataMember]
        public Decimal? SettledValue { get; set; }
        [DataMember]
        public Decimal MarketPrice { get; set; }
        [DataMember]
        public String CallPut { get; set; }
        [DataMember]
        public String MaturityDate { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }

    }
    [Serializable(), DataContract()]
    public class POSITION_OPTION
    {
        [DataMember]
        public Guid Option_UID { get; set; }
        [DataMember]
        public Guid Detail_UID { get; set; }
        [DataMember]
        public Decimal StrikePrice { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }
    }
    [Serializable(), DataContract()]
    public class POSITION_PRICE
    {
        [DataMember]
        public Guid Price_UID { get; set; }
        [DataMember]
        public Guid Detail_UID { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public DateTime? LastPriceDate { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public String ChangeType { get; set; }
    }
    [Serializable(), DataContract()]
    public class TRANSACTION
    {
        [DataMember]
        public Int64 BatchId { get; set; }
        [DataMember]
        public Guid Tran_UID { get; set; }
        [DataMember]
        public Guid Account_UID { get; set; }
        [DataMember]
        public Guid Security_UID { get; set; }
        [DataMember]
        public Int64 SecurityFirmID { get; set; }
        [DataMember]
        public String TranID { get; set; }
        [DataMember]
        public Int32 TranCode { get; set; }
        [DataMember(IsRequired = true)]
        public String TradeDate { get; set; }
        [DataMember]
        public String SettleDate { get; set; }
        [DataMember]
        public Decimal Quantity { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public String Currency { get; set; }
        [DataMember]
        public Decimal Value { get; set; }
        [DataMember]
        public Decimal? BaseFXRate { get; set; }
        [DataMember]
        public Boolean? BaseFXDerived { get; set; }
        [DataMember]
        public Decimal? BasePrice { get; set; }
        [DataMember]
        public String BaseCurrency { get; set; }
        [DataMember]
        public Decimal? BaseValue { get; set; }
        [DataMember]
        public Boolean Reversal { get; set; }
        [DataMember]
        public String Broker { get; set; }
        [DataMember]
        public String Comment { get; set; }
        [DataMember]
        public String Quality { get; set; }
        [DataMember]
        public Guid Source_UID { get; set; }
        [DataMember]
        public String Genesis { get; set; }
        [DataMember]
        public string ParserContext { get; set; }
        [DataMember]
        public string ParserMessage { get; set; }
        [DataMember]
        public DateTime ChangeDate { get; set; }
        [DataMember]
        public string ChangeType { get; set; }
        [DataMember]
        public string ReceiveDate { get; set; }
        [DataMember]
        public bool Visible { get; set; }
    }
}
