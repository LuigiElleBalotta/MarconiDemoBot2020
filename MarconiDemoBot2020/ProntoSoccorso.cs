namespace MarconiDemoBot2020
{
    class ProntoSoccorso
    {
        public string Nome { get; set; }

        public string Codice { get; set; }

        public Attesa Attesa { get; set; }

        public ProntoSoccorso(string nome, string codice, Attesa attesa)
        {
            Nome = nome;
            Codice = codice;
            Attesa = attesa;
        }
    }
}
