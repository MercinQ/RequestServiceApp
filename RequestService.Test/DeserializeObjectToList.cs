using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestServiceApp.Models;

namespace RequestService.Test
{
    [TestClass]
    public class DeserializeObjectToList
    {
      private readonly  List<Request> _listExpected = new List<Request> {
             new Request { ClientId = "1", RequestId = 1, Name = "Bułka", Quantity = 1, Price = 10.00 },
             new Request { ClientId = "1", RequestId = 2, Name = "Chleb", Quantity = 2, Price = 15.00 },
             new Request { ClientId = "1", RequestId = 2, Name = "Chleb", Quantity = 5, Price = 15.00 },
             new Request { ClientId = "2", RequestId = 1, Name = "Chleb", Quantity = 1, Price = 10.00 },
             };

        public bool ArelistEqueal(List<Request> ListActual, List<Request> ListExpected)
        { 
            for (int i = 0; i < ListExpected.Count; i++)
            {
                if(ListExpected[i].ClientId != ListActual[i].ClientId |
                ListExpected[i].RequestId != ListActual[i].RequestId |
                ListExpected[i].Name != ListActual[i].Name |
                ListExpected[i].Quantity != ListActual[i].Quantity |
                ListExpected[i].Price != ListActual[i].Price) { return false;}
            }
            return true;
        }

        [TestMethod]
        public void CsvToObjectList_DeserializeObjectsToList_Deserialized()
        {
            string pathToActualList = "../../FilesForTest/orders.csv";
            List<Request> ListActual = Request.CsvToObjectList(pathToActualList);
            
            Assert.IsTrue(ArelistEqueal(ListActual, _listExpected));
        }

        [TestMethod]
        public void JsonToObjectList_DeserializeObjectsToList_Deserialized()
        {
            string pathToActualList = "../../FilesForTest/orders.json";
            List<Request> ListActual = Request.JsonToObjectList(pathToActualList);

            Assert.IsTrue(ArelistEqueal(ListActual, _listExpected));
        }

        [TestMethod]
        public void XmlToObjectList_DeserializeObjectsToList_Deserialized()
        {
            string pathToActualList = "../../FilesForTest/orders.xml";
            List<Request> ListActual = Request.XmlToObjectList(pathToActualList);

            Assert.IsTrue(ArelistEqueal(ListActual, _listExpected));
        }    

        [TestMethod]
        public void ListFilter_DeleteBadRecordsFromList_Filtred()
        {
            List<Request> ListAfterFiltringExpected = new List<Request> {
             new Request { ClientId = "1", RequestId = 2, Name = "Chleb", Quantity = 5, Price = 15.00 },
             new Request { ClientId = "2", RequestId = 1, Name = "Chleb", Quantity = 1, Price = 10.00 },
             };


            List<Request> ListToFiltring = new List<Request> {
             new Request { ClientId = "abcde fghsfsd", RequestId = 1, Name = "Bułka", Quantity = 1, Price = 10.00 },
             new Request { ClientId = "1", RequestId = 2, Name = "dasssssssssssssssssssssssssasdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsd", Quantity = 2, Price = 15.00 },
             new Request { ClientId = "1", RequestId = 2, Name = "Chleb", Quantity = 5, Price = 15.00 },
             new Request { ClientId = "2", RequestId = 1, Name = "Chleb", Quantity = 1, Price = 10.00 },
             };

            List<Request> ListAfterFiltringActual = Request.ListFilter(ListToFiltring);
            Assert.IsTrue(ArelistEqueal(ListAfterFiltringActual, ListAfterFiltringExpected));
        }

    }
}
