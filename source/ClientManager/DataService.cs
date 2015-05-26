using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BubbleTea.DataAccess;

namespace ClientManager
{
    public class DataService
    {
        private static IDataRepository _repository;
        public static IDataRepository Repository
        {
            get
            {
                return _repository ?? (_repository = new DataRepository());
            }
        }
    }
}
