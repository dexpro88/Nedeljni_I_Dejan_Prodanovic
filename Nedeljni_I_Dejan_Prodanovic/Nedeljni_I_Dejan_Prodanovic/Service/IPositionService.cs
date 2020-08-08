using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    interface IPositionService
    {
        tblPosition AddPosition(tblPosition position);
        tblPosition GetPositionByName(string postionName);
        List<tblPosition> GetPositions();
        void DeletePosition(int positionId);
    }
}
