using Nedeljni_I_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Dejan_Prodanovic.Service
{
    class PositionService:IPositionService
    {
        public tblPosition AddPosition(tblPosition position)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {

                    tblPosition newPositon = new tblPosition();
                    newPositon.PositionName = position.PositionName;
                    newPositon.PositionDescription = position.PositionDescription;

                    context.tblPositions.Add(newPositon);
                    context.SaveChanges();

                    return newPositon;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblPosition GetPositionByName(string positionName)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {


                    tblPosition user = (from x in context.tblPositions
                                      where x.PositionName.Equals(positionName)

                                      select x).First();

                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblPosition> GetPositions()
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {
                    List<tblPosition> list = new List<tblPosition>();
                    list = (from x in context.tblPositions select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        public void DeletePosition(int positionId)
        {
            try
            {
                using (MyCompanyDBEntities context = new MyCompanyDBEntities())
                {
                    tblPosition positionToDelete = (from u in context.tblPositions
                                                where u.PositionID == positionId
                                                select u).First();


                    context.tblPositions.Remove(positionToDelete);

                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
