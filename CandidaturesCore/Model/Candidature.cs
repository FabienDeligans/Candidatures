using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CandidaturesCore.Model
{
    public class Candidature : Entity
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public string Entreprise { get; set; }
        public string Annonce { get; set; }
        public Etat Etat { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateEtat { get; set; }

        public string Note { get; set; }
    }

    public enum Etat
    {
        Envoyé, 
        Relancé, 

        Entretien_A_Venir, 
        Entretien_Effectué, 

        Refusé,
    }
}
