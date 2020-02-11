using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MarconiDemoBot2020
{
    class StatoPS
    {
        private const string URL = "https://servizi.apss.tn.it/opendata/STATOPS001.xml";

        public IEnumerable<ProntoSoccorso> Lista()
        {
            XDocument doc = XDocument.Load(URL);

            IEnumerable<ProntoSoccorso> list = doc.Root.Elements("PRONTO_SOCCORSO").Select(x =>
            {
                string nome = x.Element("PS").Value;
                string uo = x.Element("UNITA_OPERATIVA").Value;

                XElement attesaEl = x.Element("ATTESA");

                Attesa attesa = new Attesa()
                {
                    Bianco = int.Parse(attesaEl.Element("BIANCO").Value),
                    Verde = int.Parse(attesaEl.Element("VERDE").Value),
                    Giallo = int.Parse(attesaEl.Element("GIALLO").Value),
                    Rosso = int.Parse(attesaEl.Element("ROSSO").Value),
                };

                return new ProntoSoccorso(nome, uo, attesa);
            });

            return list;
        }
    }
}
