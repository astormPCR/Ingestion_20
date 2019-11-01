using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Dynamic;
using NUnit.Framework;
using Telerik.JustMock;
using Ingestion_20.Models;

namespace IngestionUnitTest
{
    [TestFixture]
    public class AccPackUnitTest
    {

        [Test]
        public async Task IsInputCreated()
        {
            
            var foo = Mock.Create(() => new AccPack());
            foo.StatementDate = new DateTime(2019, 01, 04);
            foo.firmId = 490;
            foo.AccPackUid = Guid.Parse("5f1d3776-e76d-433a-bb78-f5668869bda9");
            foo.AccountUID = Guid.Parse("c8e85643-f5f4-45a3-a184-d9d15ef2f199");
            foo.External = new List<ExternalSecurity>();
            foo.External.Add(new ExternalSecurity() { currency = "USD", IdentifierName = "USD", IdentifierValue = "CASH" });
            foo.External[0].transactions = new List<ExpandoObject>();
            dynamic exp = new ExpandoObject();
            AddProperty(exp, "Tran", "FAA0411A");
            AddProperty(exp, "Qty", "75.00");
            AddProperty(exp, "Prc", "1.00");
            AddProperty(exp, "Type", "Buy");
            AddProperty(exp, "Class", "Private Equity");
            foo.External[0].transactions.Add(exp);
            dynamic exp2 = new ExpandoObject();
            AddProperty(exp2, "Tran", "FAA0411B");
            AddProperty(exp2, "Qty", "-6166.20.00");
            AddProperty(exp2, "Prc", "4.00");
            AddProperty(exp2, "Type", "Sell");
            AddProperty(exp2, "Class", "Equity");
            foo.External[0].transactions.Add(exp2);
            await foo.Process();
        }
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}


  //"id": "5f1d3776-e76d-433a-bb78-f5668869bda9",
  //"custodianUid": "00000000-0000-0000-0000-000000000000",
  //"datasourceUid": "e4b30a37-d0bf-49d4-bee9-a0cc19959e6d",
  //"token": "@!!!@1367b51b5546387e545721934bd4f4c684ec78dd746a47c5feffac56f7a59e77",
  //"statementDate": "2019-01-04T00:00:00Z",
  //"accountUid": "c8e85643-f5f4-45a3-a184-d9d15ef2f199",
  //"firmId": 490,
  //"state": 3,
  //"exceedsSizeLimit": false,
  //"external": [
  //  {
  //    "identifiervalue": "USD",
  //    "identifierName": "CASH",
  //    "currency": "USD",
  //    "firmId": null,
  //    "batchid": null,
  //    "securityGuid": null,
  //    "previousPositionGuid": "7f3eccf4-6f67-4960-acd5-3ac90036785b",
  //    "previousPositionQty": 993113.39,
  //    "previousPositionStartDate": "2019-01-01T00:00:00+00:00",
  //    "previousPositionEndDate": null,
  //    "transactions": [
  //      "FAA0411A|01/04/2019|Withdraw|-75.00|0.00|1|Private Equity",
  //      "FAA0411B|01/04/2019|Sell|-6166.20|0.00|4|Equity"
  //    ],
  //    "taxlots": [],
  //    "positions": [
  //      "1/4/2019 12:00:00 AM|FAA0411||@!!!@1367b51b5546387e545721934bd4f4c684ec78dd746a47c5feffac56f7a59e77|Cash|DDA|36830|0|||||||||0.0000|0.0000||0|0|||0.00000|||986872.19000||||||||0.00|0.00|||986872.19|986872.19|USD|||||0.0000||||0.000000000||||"
  //    ],
  //    "customs": []
  //  }
  //],
  //"internal": null,
  //"batchid": 2019042913164916500