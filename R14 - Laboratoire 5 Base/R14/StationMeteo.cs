using System;

namespace R14
{
    class StationMeteo
    {
        private String nom;
        private double lattitude;
        private double longitude;

        private BaseDeDonnees laBaseDeDonnees = new BaseDeDonnees();
        private Barometre leBarometre = new Barometre();
        private Thermometre leThermometre = new Thermometre();
        private Anenometre lAnenometre = new Anenometre();

        public double Lattitude { get => lattitude; set { if (Math.Abs(value) <= 90) lattitude = value; else throw new Exception("La lattitude doit être en -90 et 90."); } }
        public double Longitude { get => longitude; set { if (Math.Abs(value) <= 180) longitude = value; else throw new Exception("La longitude doit être en -180 et 180."); } }

        //---------------------------------------------------------------------
        public StationMeteo(string argNom, double argLattitude, double argLongitude)
        {
            this.nom = argNom;
            this.Lattitude = argLattitude;
            this.Longitude = argLongitude;

            laBaseDeDonnees = new BaseDeDonnees();
            laBaseDeDonnees.ouvrir("BDMeteo", "Admin1");
        }

        //---------------------------------------------------------------------
        public String obtenirBulletinsMeteo(DateTime argDate, int argHeureDebut, int argHeureFin)
        {
            String resultat = "";
            if (argHeureFin < argHeureDebut)
            {
                throw new Exception("L'heure de fin doit être au minimum celle de début.");
            }
            else if (argHeureDebut < 0 || argHeureDebut > 23)
            {
                throw new Exception("L'heure de debut doit être entre 0 et 24.");
            }
            else if (argHeureDebut < 0 || argHeureDebut > 23)
            {
                throw new Exception("L'heure de fin doit être entre 0 et 24.");
            }
            int heureCourante = argHeureDebut;
            while (heureCourante < argHeureFin)
            {
                resultat += "Station " + this.nom + " : " + laBaseDeDonnees.lire(argDate, heureCourante) + "\n";
                heureCourante++;
            }
            return resultat;
        }

        //---------------------------------------------------------------------
        public bool enregistrerDonneesDesCapteurs()
        {
            double temperature = leThermometre.lire();
            double vent = lAnenometre.lire();
            double pression = leBarometre.lire();

            return laBaseDeDonnees.ajouter(temperature, vent, pression);
        }

        //---------------------------------------------------------------------
    }
}
