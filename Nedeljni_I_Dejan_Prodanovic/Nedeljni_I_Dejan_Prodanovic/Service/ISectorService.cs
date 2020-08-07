using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    interface ISectorService
    {
        tblSector AddSector(tblSector sector);
        tblSector GetSectorByName(string sectorName);
        List<tblSector> GetSectors();
    }
}
