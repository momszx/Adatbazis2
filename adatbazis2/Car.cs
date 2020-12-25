using System;

namespace adatbazis2
{
    public class Car
    {
        private string alvazszam,rendszam,tulajdonos,tipus, marka;
        private int tulajdonosID, autoID, tipusID;

        public string Alvazszam
        {
            get => alvazszam;
            set => alvazszam = value;
        }

        public string Rendszam
        {
            get => rendszam;
            set => rendszam = value;
        }

        public string Tulajdonos
        {
            get => tulajdonos;
            set => tulajdonos = value;
        }

        public string Tipus
        {
            get => tipus;
            set => tipus = value;
        }

        public string Marka
        {
            get => marka;
            set => marka = value;
        }

        public int TulajdonosId
        {
            get => tulajdonosID;
            set => tulajdonosID = value;
        }

        public int AutoId
        {
            get => autoID;
            set => autoID = value;
        }

        public int TipusId
        {
            get => tipusID;
            set => tipusID = value;
        }

        public Car()
        {
            Alvazszam = "UNDEFINED";
            Rendszam="UNDEFINED";
            Tulajdonos="UNDEFINED";
            Tipus="UNDEFINED";
            Marka="UNDEFINED";
            TulajdonosId = 0;
            AutoId = 0;
            TipusId = 0;
        }

        public Car(int autoId,string alvazszam,string rendszam):this()
        {
            AutoId = autoId;
            Alvazszam = alvazszam;
            Rendszam = rendszam;
        }
        public Car(int tulajdonosId,string tulajdonos,int autoId,string alvazszam,string rendszam):this(autoId,alvazszam, rendszam)
        {
            TulajdonosId = tulajdonosId;
            Tulajdonos = tulajdonos;
        }
        public Car(int tulajdonosId,string tulajdonos,int autoId,string alvazszam,string rendszam,int tipusId,string tipus, string marka):this(tulajdonosId,tulajdonos,autoId,alvazszam, rendszam)
        {
            TipusId = tipusId;
            Tipus = tipus;
            Marka = marka;
        }
    }
}