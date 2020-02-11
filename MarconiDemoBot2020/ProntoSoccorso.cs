namespace MarconiDemoBot2020
{
    class ProntoSoccorso
    {
        public string Nome { get; set; }

        public string UnitàOperativa { get; set; }

        public Attesa Attesa { get; set; }

        public ProntoSoccorso(string nome, string unitàOperativa, Attesa attesa)
        {
            Nome = nome;
            UnitàOperativa = unitàOperativa;
            Attesa = attesa;
        }
    }
}
