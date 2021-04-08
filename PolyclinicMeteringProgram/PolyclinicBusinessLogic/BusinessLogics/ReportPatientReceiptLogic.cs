using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    class ReportPatientReceiptLogic
    {
        private readonly ISweetStorage _sweetStorage;
        private readonly IPastryStorage _pastryStorage;
        private readonly IOrderStorage _orderStorage;
        public ReportPatientReceiptLogic(IReceiptStorage pastryStorage, ISweetStorage sweetStorage,
        IOrderStorage orderStorage)
        {
            _pastryStorage = pastryStorage;
            _sweetStorage = sweetStorage;
            _orderStorage = orderStorage;
        }
    }
}
