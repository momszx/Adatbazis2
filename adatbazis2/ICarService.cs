

using System.Collections.Generic;

namespace adatbazis2
{
    interface ICarService
    {
        string Login(string username, string password);
        bool Logout(string ID);
        bool Add(string alvazszam,string rendszam,string tulajdonos,string tipus,string marka,string ID);
        bool Delete( int tulajID,int carID,int tipisID, string ID);
        bool Modify(int tulajID,int carID,int tipisID, string alvazszam, string rendszam, string tulajdonos, string tipus, string marka,string ID);
        bool GenerateData(int num,string ID);
        List<Car> List(string ID);
    }
}
